using System;
using static CoreOHB.Helpers.Files;
using static CoreOHB.Helpers.ToastNotifications;
using static CoreOHB.Helpers.NetWork;
using static CoreOHB.Core;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace YML_Doc
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Progress<string> progress;

        //DispatcherTimer dispatcherTimer;
        //DispatcherTimer dispatcherTimer2;
        DateTime startTime, endTime;

        string taskName = "Update_OneHomeBeauty";
        string taskEntryPoint = "RuntimeComponentOHB.YMLShopUpdate";
        BackgroundTaskRegistration bkTask;

        public ViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            DataContext = viewModel;
            progress = new Progress<string>(s => txtStatus.Text = txtStatus.Text + s + "\r\n");
            startTime = DateTime.Now;
            endTime = startTime.Add(timePiker.Time);
            RestoreSettimgs();
        }


        private async void BtnUpdate_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                await UpdateOneHomeBeautyAsync();
            }
            catch (Exception exc)
            {
                ShowToast("BtnUpdate_Tapped", exc.Message);
            }
        }


        private async void BtnGetInfo_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                if(InternetAvailable())await GetInfoShopAsync(FolderOHB_Remote + FileOHB_Shop, progress);
            }
            catch (Exception exc)
            {
                ShowToast("BtnGetInfo_Tapped", exc.Message);
            }
        }

        private void TimePiker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            if (timePiker.Time >= TimeSpan.Parse("00:15:00"))
            {
                //dispatcherTimer.Stop();
                //dispatcherTimer.Interval = timePiker.Time;
                //dispatcherTimer.Start();
                startTime = DateTime.Now;
                endTime = startTime.Add(timePiker.Time);
            }

            localSettings.Values["timeupdate"] = timePiker.Time;
        }


        private void BtnSetBackgroundTask_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                SetBackgroundTask(timePiker.Time.TotalMinutes);
            }
            catch (Exception exc)
            {
                ShowToast("BtnSetBackgroundTask_Tapped", exc.Message);
            }

           
        }

        //
        // Register a background task with the specified taskEntryPoint, name, trigger,
        // and condition (optional).
        //
        // taskEntryPoint: Task entry point for the background task.
        // taskName: A name for the background task.
        // trigger: The trigger for the background task.
        // condition: Optional parameter. A conditional event that must be true for the task to fire.
        //
        public BackgroundTaskRegistration RegisterBackgroundTask(string taskEntryPoint,
                                                                        string taskName,
                                                                        IBackgroundTrigger trigger,
                                                                        IBackgroundCondition condition)
        {
            CheckExistTask(taskName)?.Unregister(true);
            // Register the background task.
            var builder = new BackgroundTaskBuilder();
            builder.Name = taskName;
            builder.TaskEntryPoint = taskEntryPoint;
            builder.SetTrigger(trigger);
            // Begin adding conditions.
            SystemCondition internetCondition = new SystemCondition(SystemConditionType.InternetAvailable);
            
            builder.AddCondition(internetCondition);

            BackgroundTaskRegistration task = builder.Register();

            return task;
        }

        private static BackgroundTaskRegistration CheckExistTask(string taskName)
        {
            // Check for existing registrations of this background task.
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == taskName)
                {
                    // The task is already registered.
                    return (BackgroundTaskRegistration)cur.Value;
                }
            }
            return null;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private async void BtnClear_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            txtStatus.Text = "";
            await LogFile.DeleteAsync();
        }

        private void SetBackgroundTask(double minutes)
        {
            // A time trigger that repeats at 15-minute intervals.
            IBackgroundTrigger trigger = new TimeTrigger((uint)minutes, false);
            BackgroundTaskRegistration bk = RegisterBackgroundTask(taskEntryPoint, taskName, trigger, null);
            if (bk != null)
            {
                txtStatus.Text += "Background Task " + taskName + " registered!\r\n";
                txtStatus.Text += "time trigger set at " + minutes.ToString() + "minutes\r\n";
            }
        }

        private async void BtnLog_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            txtStatus.Text = await GetLogFileTextAsync();
           // GetLogAsync();
        }

        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private void ToggleToast_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void ToggleTask_LostFocus(object sender, RoutedEventArgs e)
        {
            //localSettings.Values["enable_task"] = toggleToast.IsOn;
        }

        private void ToggleTask_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch t = (ToggleSwitch)e.OriginalSource;
            BackgroundTaskRegistration bk = CheckExistTask(taskName);
            if (t.IsOn & bk==null)
            {
                SetBackgroundTask(timePiker.Time.TotalMinutes);
            }
            else if(!t.IsOn & bk!=null)
            {
                bk.Unregister(true);
            }
        }

        private void ToggleToast_Toggled(object sender, RoutedEventArgs e)
        {
            localSettings.Values["showtoast"] = toggleToast.IsOn;
        }

        private void RestoreSettimgs()
        {
            object tog = localSettings.Values["showtoast"];
            if (tog == null)
            {
                toggleToast.IsOn = false;
            }
            else
            {
                toggleToast.IsOn = (bool)tog;
            }
            object time = localSettings.Values["timeupdate"];
            if (time != null)
            {
                timePiker.Time = (TimeSpan)time;
            }
            else
            {
                timePiker.Time = new TimeSpan(4, 0, 0);
            }
            bkTask = CheckExistTask(taskName);
            if (bkTask != null)
            {
                toggleTask.IsOn = true;
            }
            else toggleTask.IsOn = false;
        }

    }
    public class OHBShops : DependencyObject
    {
        public int ItemCount
        {
            get { return (int)GetValue(ItemCountProperty); }
            set { SetValue(ItemCountProperty, value); }
        }

        public static readonly DependencyProperty ItemCountProperty =
             DependencyProperty.Register("ItemCount", typeof(int),
             typeof(yml_catalog), new PropertyMetadata(0));
    }
}
