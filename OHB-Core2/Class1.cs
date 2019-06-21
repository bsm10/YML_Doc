using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

//using static CoreOHB.Helpers;
//using static CoreOHB.Helpers.Files;

using Windows.Storage;
using Windows.UI.Notifications;

namespace CoreOHB
{
    public static class CoreOHB
    {
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private static readonly string file = @"\DataFiles\onehomebeauty.xml";

        static readonly XName yml_catalog = "yml_catalog";
        static readonly XName shop = "shop";
        static readonly XName offers = "offers";
        static readonly XName categoryId = "categoryId";
        static readonly XName category = "category";
        static readonly XName categories = "categories";
        static readonly XName groups = "groups";
        static readonly XName root = "root";
        static readonly XName id = "id";
        static readonly XName parentId = "parentId";
        static readonly XName date = "date";
        static readonly XName url = "url";
        static readonly XName available = "available";


        private static XElement shopTree;//Корневой узел, который содержит все магазины
        private static XDocument shopXML;//XML, который содержит все магазины

        private static StorageFile xmlFile;

        //private static async Task UploadShopAsync()
        //{
        //    try
        //    {
        //        FtpClient client = new FtpClient
        //        {
        //            Host = "elchukof.ftp.tools",
        //            Credentials = new NetworkCredential("elchukof_tks", "gAdP6K51mMh9")
        //        };
        //        //client.Connect();
        //        //client.UploadFile(xmlFile.Path, "/www/files/onehomebeauty.xml");
        //        await client.ConnectAsync();
        //        await client.UploadFileAsync(xmlFile.Path, "/www/files/onehomebeauty.xml");
        //        if ((bool)localSettings.Values["showtoast"] == true)
        //        {
        //            ToastNotifications.ShowToast("One Home Beauty", DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " - Updated onehomebeauty.xml");
        //        }

        //        await LogAsync("uploaded!");
        //    }
        //    catch (Exception ex)
        //    {
        //        await LogAsync(ex.Message);
        //    }
        //    finally
        //    {

        //    }

        //}

        //private static async Task SaveXml()
        //{
        //    try
        //    {
        //        // получаем локальную папку
        //        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        //        // создаем файл
        //        xmlFile = await localFolder.CreateFileAsync(file, CreationCollisionOption.ReplaceExisting);
        //        // запись в файл
        //        await FileIO.WriteTextAsync(xmlFile, shopTree.ToString());
        //        FileInfo fi = new FileInfo(xmlFile.Path);
        //        await LogAsync(xmlFile.Name + " - " + fi.Length.FileSizeToString());
        //    }
        //    catch (Exception e)
        //    {
        //        await LogAsync(e.Message);
        //    }
        //}

        private static async Task LoadShopAsync(string url_shop)
        {
            try
            {
                //***************************************************************
                //загружаем магазин
                Uri uri = new Uri(url_shop);
                XDocument xYMLCatalog;
                using (var httpclient = new HttpClient())
                {
                    var response = await httpclient.GetAsync(url_shop);
                    xYMLCatalog = XDocument.Load(await response.Content.ReadAsStreamAsync());
                }
                //xYMLCatalog = XDocument.Load(url_shop);
                //список категорий
                IEnumerable<XElement> xCategories = xYMLCatalog.Element(yml_catalog).Element(shop).Element(categories).Elements(category);

                //добавляем Категории и Товары в общее дерево
                shopTree.Element(shop).Element(categories).Add(xYMLCatalog.Element(yml_catalog).Element(shop).Element(categories).Elements());
                shopTree.Element(shop).Element(offers).Add(xYMLCatalog.Element(yml_catalog).Element(shop).Element(offers).Elements());
                //список товаров
                IEnumerable<XElement> xOffers = xYMLCatalog.Element(yml_catalog).Element(shop).Element(offers).Elements();
                XAttribute xCatalogAttribute = xYMLCatalog.Element(yml_catalog).Attribute(date);
                DateTime lastUpdate = DateTime.Parse(xCatalogAttribute.Value);//дата последнего обновления
            }
            catch (XmlException xmlEx)
            {
                //await LogAsync(xmlEx.Message);
                //MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                //await LogAsync(ex.Message);
                //MessageBox.Show(ex.Message);
            }
            //return null;
        }

        private static async Task GetShopsAsync(IProgress<string> progress)
        {
            try
            {
                //загружаем список магазинов
                XDocument xdoc = XDocument.Load(@"DataFiles/shops-yml.xml");
                int countshops = xdoc.Element("shops-yml").Elements().Count();
                string time_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                //строим структуру основного файла
                shopTree =
                    new XElement(yml_catalog, new XAttribute(date, time_update),
                        new XElement(shop,
                            new XElement(categories),
                            new XElement(offers)));
                //добавляем в корень категории и товары
                foreach (XElement addresxml in xdoc.Element("shops-yml").Descendants())
                {
                    progress?.Report("Loading " + addresxml.Value + "...");
                    await LoadShopAsync(addresxml.Value);
                    progress?.Report("Done!");
                }
                shopXML = new XDocument();
                shopXML.Add(shopTree);
            }
            catch (Exception e)
            {
                //await LogAsync(e.Message);
            }
        }

        public static async Task GetInfoShopAsync(string shopUrl, IProgress<string> progress)
        {
            try
            {
                progress.Report("Читаю " + shopUrl + "...");
                XDocument xCatalog;
                using (var httpclient = new HttpClient())
                {
                    var response = await httpclient.GetAsync(shopUrl);
                    xCatalog = XDocument.Load(await response.Content.ReadAsStreamAsync());
                }

                //string time_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                progress.Report("Всего категорий - " + xCatalog.Element(yml_catalog).Element(shop).Element(categories).Elements().Count()
                               + "; товаров - " + xCatalog.Element(yml_catalog).Element(shop).Element(offers).Elements().Count());

                progress.Report("Дата обновления - " + xCatalog.Element(yml_catalog).Attribute(date).Value);
            }
            catch (Exception e)
            {
                //await LogAsync(e.Message);
            }

        }

        public static async Task UpdateOneHomeBeautyAsync(IProgress<string> progress = null)
        {
            await GetShopsAsync(progress);
            //await SaveXml();
            //await UploadShopAsync();
        }
    }
    public static class Helpers
    {
        //public static class ToastNotifications
        //{
        //    public static void ShowToast(string title, string content, string image = "", string logo = "")
        //    {
        //        // In a real app, these would be initialized with actual data
        //        //string title = "Andrew sent you a picture";
        //        //string content = "Check this out, Happy Canyon in Utah!";
        //        //string image = "https://picsum.photos/360/202?image=883";
        //        //string logo = "ms-appdata:///local/Andrew.jpg";

        //        // Construct the visuals of the toast
        //        ToastVisual visual = new ToastVisual()
        //        {
        //            BindingGeneric = new ToastBindingGeneric()
        //            {
        //                Children =
        //            {
        //                new AdaptiveText()
        //                {
        //                    Text = title
        //                },
        //                new AdaptiveText()
        //                {
        //                    Text = content
        //                },
        //                new AdaptiveImage()
        //                {
        //                    Source = image
        //                }
        //            },
        //                AppLogoOverride = new ToastGenericAppLogo()
        //                {
        //                    Source = logo,
        //                    HintCrop = ToastGenericAppLogoCrop.Circle
        //                }
        //            }
        //        };
        //        // Now we can construct the final toast content
        //        ToastContent toastContent = new ToastContent()
        //        {
        //            Visual = visual,
        //        };

        //        // And create the toast notification
        //        var toast = new ToastNotification(toastContent.GetXml());
        //        ToastNotificationManager.CreateToastNotifier().Show(toast);
        //    }
        //}
        //public static class Files
        //{
        //    private readonly static string logFileName = "update.log";
        //    public static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        //    public static StorageFile LogFile { get; set; }

        //    public static async Task LogAsync(string text)
        //    {
        //        try
        //        {
        //            // получаем локальную папку
        //            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        //            string filePath = "update.log";
        //            // получаем файл
        //            StorageFile logFile = await localFolder.GetFileAsync(filePath);
        //            FileInfo fi = new FileInfo(logFile.Path);
        //            if (fi.Length < 1000)
        //            {
        //                await FileIO.AppendTextAsync(logFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm - ") + text + "\r\n");
        //            }
        //            else
        //            {
        //                await FileIO.WriteTextAsync(logFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm - ") + text + "\r\n");
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            await LogAsync(e.Message);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    public static async Task<string> GetLogFileTextAsync()
        //    {
        //        try
        //        {
        //            // получаем файл
        //            LogFile = await localFolder.GetFileAsync(logFileName);
        //            // читаем файл
        //            string text = await FileIO.ReadTextAsync(LogFile);
        //            //txtStatus.Text += text + "\r\n";
        //            return text + "\r\n";

        //        }
        //        catch (FileNotFoundException)
        //        {
        //            return "Файл лога не найден";
        //        }
        //    }
        //    public static async Task CreateLogFileAsync()
        //    {
        //        try
        //        {
        //            StorageFile file = await localFolder.GetFileAsync(logFileName);
        //        }
        //        catch (FileNotFoundException)
        //        {
        //            //txtStatus.Text += "File not found, - creating..." + "\r\n";
        //            LogFile = await localFolder.CreateFileAsync(logFileName, CreationCollisionOption.ReplaceExisting);
        //            //txtStatus.Text += "File was creation - " + LogFile.Path + "\r\n";
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.Message + "\r\n" + e.HelpLink + "\r\n";
        //            ToastNotifications.ShowToast("Error", s);
        //        }

        //    }
        //}
    }
}
