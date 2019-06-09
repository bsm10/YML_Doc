using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using YML_Doc.ViewModel;
using YMLUpload;

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


        public MainPage()
        {
            InitializeComponent();
            ViewModelCatalog viewModel = new ViewModelCatalog();
            progress = new Progress<string>(s => txtStatus.Text = txtStatus.Text + s + "\r\n");
            //  DispatcherTimer setup
            //dispatcherTimer = new DispatcherTimer();
            //dispatcherTimer.Tick += dispatcherTimer_TickAsync;
            timePiker.Time = new TimeSpan(4, 0, 0);
            //dispatcherTimer.Interval = timePiker.Time;
            //dispatcherTimer.Interval = new TimeSpan(0, (int)numericUpDown1.Value, 0);
            //dispatcherTimer.Start();

            //dispatcherTimer2 = new DispatcherTimer();
            //dispatcherTimer2.Tick += dispatcherTimer_Tick2;
            //dispatcherTimer2.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer2.Start();
            startTime = DateTime.Now;
            endTime = startTime.Add(timePiker.Time);

        }

        TimeSpan ts;
        private void dispatcherTimer_Tick2(object sender, object e)
        {
            if (endTime > DateTime.Now)
            {
                ts = endTime - DateTime.Now;
                txt1.Text = "До обновления осталось - " + ts.ToString(@"hh\:mm\:ss");
            }
            else
            {
                txt1.Text = "До обновления осталось - 0";
            }

        }

        //private async void dispatcherTimer_TickAsync(object sender, object e)
        //{
        //    await YML_prog.UpdateOneHomeBeauty(progress);
        //    startTime = DateTime.Now;
        //    endTime = startTime.Add(timePiker.Time);
        //}

        private async void BtnUpdate_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            await YML_prog.UpdateOneHomeBeauty(progress);
        }


        private async void BtnGetInfo_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            //await YMLUpload.YML_prog.LoadShopAsync("http://tks.pl.ua/files/onehomebeauty.xml", progress);
            await GetInfo();

        }

        private async System.Threading.Tasks.Task GetInfo()
        {
            bkTask = CheckExistTask(taskName);
            if (bkTask != null)
            {
                txtStatus.Text += "Background Task " + taskName + " is worked...\r\n";
            }
            else txtStatus.Text += "Background Task " + taskName + " is not worked!\r\n";

            await YML_prog.GetInfoShopAsync("http://tks.pl.ua/files/onehomebeauty.xml", progress);
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
        }


        private void BtnSetBackgroundTask_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            SetBackgroundTask(timePiker.Time.TotalMinutes);
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
            //var requestStatus = await BackgroundExecutionManager.RequestAccessAsync();
            //if (requestStatus != BackgroundAccessStatus.AlwaysAllowed)
            //{
            //    // Depending on the value of requestStatus, provide an appropriate response
            //    // such as notifying the user which functionality won't work as expected
            //}
            CheckExistTask(taskName)?.Unregister(true);
            //cur.Value.Unregister(true);
            // Register the background task.
            var builder = new BackgroundTaskBuilder();
            builder.Name = taskName;
            builder.TaskEntryPoint = taskEntryPoint;
            builder.SetTrigger(trigger);
            // Begin adding conditions.
            //SystemCondition userCondition = new SystemCondition(SystemConditionType.UserPresent);
            SystemCondition internetCondition = new SystemCondition(SystemConditionType.InternetAvailable);
            
            //builder.AddCondition(userCondition);
            builder.AddCondition(internetCondition);

            //if (condition != null)
            //{
            //    builder.AddCondition(condition);
            //}

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

        private void BtnUnregBackgroundTask_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            BackgroundTaskRegistration bk = CheckExistTask(taskName);
            if(bk!=null)
            {
                bk.Unregister(true);
                txtStatus.Text += "Background Task " + taskName + " unregistered! \r\n";
            }
            else
            {
                txtStatus.Text += "Background Task " + taskName + " is null! \r\n";
            }
            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await CreateLogFileAsync();
            await GetInfo();
        }

        private void BtnClear_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            txtStatus.Text = "";
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

        private void BtnLog_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            GetLogAsync();
        }

        StorageFile logFile;
        string logFileName = "update.log";
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        private async void GetLogAsync()
        {
            try
            {

                // получаем файл
                logFile = await localFolder.GetFileAsync(logFileName);
                // читаем файл
                string text = await FileIO.ReadTextAsync(logFile);
                txtStatus.Text += text + "\r\n";

            }
            catch(FileNotFoundException)
            {
                return;
            }
        }
        private async Task CreateLogFileAsync()
        {
            try
            {
                StorageFile file = await localFolder.GetFileAsync(logFileName);
            }
            catch(FileNotFoundException)
            {
                txtStatus.Text += "File not found, - creating..." + "\r\n";
                logFile = await localFolder.CreateFileAsync(logFileName, CreationCollisionOption.ReplaceExisting);
                txtStatus.Text += "File was creation - " + logFile.Path + "\r\n";
            }
            catch(Exception e)
            {
                txtStatus.Text += e.Message + "\r\n";
                txtStatus.Text += e.HelpLink + "\r\n";
            }

        }
        private async Task LogAsync(string text)
        {
            try
            {
                // получаем файл
                StorageFile logFile = await localFolder.GetFileAsync(logFileName);
                await FileIO.AppendTextAsync(logFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm - ") + text + "\r\n");
            }
            finally
            {

            }
        }

    }
}
