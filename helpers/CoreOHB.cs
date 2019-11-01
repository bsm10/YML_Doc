using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using FluentFTP;

using Microsoft.Toolkit.Uwp.Notifications;

using static CoreOHB.Helpers.Files;
using static CoreOHB.Helpers.NetWork;
using static CoreOHB.Helpers.ToastNotifications;

using Windows.Networking.Connectivity;
using Windows.UI.Notifications;
using CoreOHB.Helpers;

namespace CoreOHB //.OldCore
{
    public static class Core
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
        private static IEnumerable<XElement> ExcludesGoods;//Collection, который содержит исключения которые не надо импортировать
        public static IEnumerable<XElement> ListShopXML;//Collection, который содержит список магазинов
        private static XDocument shopXML;//XML, который содержит все магазины
                                         //private static XDocument excludesShopXML;//XML, который содержит исключения которые не надо импортировать

        private static TreeView tw;

        private static StorageFile xmlFile; //сюда записывается итоговый файл магазина (на диске локально)
        static IProgress<string> progress = null;


        private static async Task UploadShopAsync()
        {
            try
            {
                FtpClient client = new FtpClient
                {
                    Host = "ftp://ftp.s51.freehost.com.ua/",
                    Credentials = new NetworkCredential("granitmar1_ohbed", "Va3NeMHzyY")
                };
                await client.ConnectAsync();
                string newFileName = Path.GetFileNameWithoutExtension(FileOHB_Shop)
                                     + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + Path.GetExtension(FileOHB_Shop);
                if (await client.FileExistsAsync(FileOHB_Shop))
                {
                    await client.RenameAsync(FileOHB_Shop, newFileName);
                }
                await client.UploadFileAsync(xmlFile.Path, FileOHB_Shop);
                if ((bool)localSettings.Values["showtoast"] == true)
                {
                    ShowToast("One Home Beauty", DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " - Updated onehomebeauty.xml");
                }

                await LogAsync("uploaded!");
            }
            catch (Exception ex)
            {
                await LogAsync(ex.Message);
            }
            finally
            {

            }

        }
        //public static async Task UploadShopAsync(string fileName)
        //{
        //    try
        //    {
        //        string file = Path.GetFileName(fileName);
        //        await LogAsync($"Uploading {file} to ftp..."); 
        //        using (WebClient client = new WebClient())
        //        {
        //            client.Credentials = new NetworkCredential("granitmar1_ohbed", "Va3NeMHzyY");
        //            byte[] responseArray = await client.UploadFileTaskAsync("ftp://ftp.s51.freehost.com.ua/" + file,
        //                                                                    "STOR",
        //                                                                    Path.Combine(Files.FolderOHB_Local, file));
        //            await LogAsync(Encoding.Default.GetString(responseArray) == "" ? "Ok!" :
        //                             Encoding.Default.GetString(responseArray));
        //        }
        //        //Report($"Ok!");
        //    }
        //    catch (Exception ex)
        //    {
        //        await LogAsync(ex.Message);
        //    }
        //}

        private static async Task SaveXml()
        {
            try
            {
                // получаем локальную папку
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;

                using (StreamWriter sw = File.CreateText(localFolder.Path + file))
                {
                    shopTree.Save(sw);
                }

                xmlFile = await localFolder.GetFileAsync(file);

                FileInfo fi = new FileInfo(xmlFile.Path);
                await LogAsync(xmlFile.Name + " - " + fi.Length.FileSizeToString());
            }
            catch (Exception e)
            {
                await LogAsync(e.Message);
            }
        }

        private static  Task<XDocument> LoadShopAsync(string url_shop)
        {
            return Task.Run(async () =>
            {
                try
                {
                    //***************************************************************
                    //загружаем магазин
                    Uri uri = new Uri(url_shop);
                    XDocument xYMLCatalog;

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    
                    Encoding encoding;
                    if (url_shop.Contains("Platok"))
                    {
                        encoding = Encoding.GetEncoding("windows-1251");
                        IFtpClient f = new FtpClient();
                        f.Host = uri.Host;
                        xYMLCatalog = GetXDocFromStream(encoding, f.OpenRead(uri.LocalPath));
                    }
                    else
                    {
                        encoding = Encoding.GetEncoding("utf-8");
                        using (var httpclient = new HttpClient())
                        {
                            var response = await httpclient.GetAsync(url_shop);
                            xYMLCatalog = XDocument.Load(await response.Content.ReadAsStreamAsync());
                        }

                        //WebRequest request = WebRequest.Create(url_shop);
                        //using (WebResponse response = await request.GetResponseAsync())
                        //{
                        //    xYMLCatalog = GetXDocFromStream(encoding, response.GetResponseStream());
                        //}
                    }

                    //список категорий
                    IEnumerable<XElement> xCategories = xYMLCatalog.Element(yml_catalog).Element(shop).Element(categories).Elements(category);

                    //добавляем Категории и Товары в общее дерево

                    //загружаем список исключений - товары, которые исключаются из общего файла
                    ExcludesGoods = await LoadShopExcludesAsync();
                    IEnumerable<XElement> allGoods = xYMLCatalog.Element(yml_catalog).Element(shop).Element(offers).Elements();
                    IEnumerable<XElement> ohbGoods = allGoods.Except(ExcludesGoods, new GoodsComparer());

                    shopTree.Element(shop).Element(categories).Add(xYMLCatalog.Element(yml_catalog).Element(shop).Element(categories).Elements());
                    shopTree.Element(shop).Element(offers).Add(ohbGoods);

                    //список товаров
                    IEnumerable<XElement> xOffers = xYMLCatalog.Element(yml_catalog).Element(shop).Element(offers).Elements();

                    XAttribute xCatalogAttribute = xYMLCatalog.Element(yml_catalog).Attribute(date);
                    DateTime lastUpdate = DateTime.Parse(xCatalogAttribute.Value);//дата последнего обновления
                    return xYMLCatalog;
                }
                catch (XmlException xmlEx)
                {
                    await LogAsync(xmlEx.Message);
                    return null;
                }
                catch (Exception ex)
                {
                    await LogAsync(ex.Message);
                    return null;
                }
            }
            );
        }

        private static XDocument GetXDocFromStream(Encoding encoding, Stream stream)
        {
            XDocument xYMLCatalog;
            using (StreamReader reader = new StreamReader(stream, encoding))
            {
                xYMLCatalog = XDocument.Load(reader);
            }

            return xYMLCatalog;
        }

        // Custom comparer for the Product class
        class GoodsComparer : IEqualityComparer<XElement>
        {
            // Products are equal if their names and product categories are equal.
            public bool Equals(XElement x, XElement y)
            {
                return x.Attribute("id").Value == y.Attribute("id").Value && x.Element("categoryId").Value == y.Element("categoryId").Value;
            }

            // If Equals() returns true for a pair of objects 
            // then GetHashCode() must return the same value for these objects.

            public int GetHashCode(XElement product)
            {
                //Check whether the object is null
                if (object.ReferenceEquals(product, null)) return 0;

                //Get hash code for the Name field if it is not null.
                int hashProductName = product.Value == null ? 0 : product.Value.GetHashCode();

                //Get hash code for the Code field.
                int hashProductCode = product.Value.GetHashCode();

                //Calculate the hash code for the product.
                return hashProductName ^ hashProductCode;
            }

        }
        private static async Task<IEnumerable<XElement>> LoadShopExcludesAsync()
        {
            try
            {
                //***************************************************************
                //загружаем магазин
                XDocument xDoc;
                using (var httpclient = new HttpClient())
                {
                    var response = await httpclient.GetAsync(FolderOHB_Remote + "excludes.xml");
                    xDoc = XDocument.Load(await response.Content.ReadAsStreamAsync());
                }
                return xDoc.Element("Excludes").Elements();
            }
            catch (XmlException xmlEx)
            {
                await LogAsync(xmlEx.Message);
            }
            catch (Exception ex)
            {
                await LogAsync(ex.Message);
            }
            return null;
        }

        private static async Task GetShopsAsync()
        {
            try
            {
                //загружаем список магазинов
                int countshops = ListShopXML.Count();
                string time_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                //строим структуру основного файла
                shopTree =
                    new XElement(yml_catalog, new XAttribute(date, time_update),
                        new XElement(shop,
                            new XElement(categories),
                            new XElement(offers)));

                //добавляем в корень категории и товары
                //foreach (XElement addresxml in ListShopXML)
                //{
                //    progress?.Report("Loading " + addresxml.Value + "...");
                //    await LoadShopAsync(addresxml.Value);
                //    progress?.Report("Done!");
                //}
                shopXML = new XDocument();
                int count = ListShopXML.Count();
                Task<XDocument>[] tasks = new Task<XDocument>[count];
                int i = 0;
                foreach (XElement addresxml in ListShopXML) 
                {
                    progress?.Report("Start loading " + addresxml.Value + "...");
                    tasks[i] = LoadShopAsync(addresxml.Value);
                    i++;
                }
                var processingTasks = tasks.Select(AwaitAndProcessAsync).ToList();
                await Task.WhenAll(processingTasks);




            }
            catch (Exception e)
            {
                await LogAsync(e.Message);
            }
        }
        static async Task AwaitAndProcessAsync(Task<XDocument> task)
        {
            var result = await task;
            
            if (result != null)
            {
                //shopXML.Add(shopTree);
                //await LogAsync("Task " + task.Result.Element(shop).Value + " added");
                var xshop = result.Element(yml_catalog).Element(shop);
                progress?.Report($"{xshop.Element(url).Value} добавлено {xshop.Element(offers).Elements().Count()} товаров");
            }

        }

        public static void BuildOHBShopAsync()
        {
            try
            {
                //string time_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                //xOHBShop = new XElement(yml_catalog, new XAttribute(date, time_update),
                //               new XElement(shop,
                //                   new XElement(url, "http://onehomebeauty.com.ua")));
                //xOHBShop.Element(shop).Add(xOHBCategories, new XElement(offers));
                //XDocument xDoc = new XDocument(new XComment("YML file of OneHomeBeauty shop"), xOHBShop);
            }
            catch (Exception e)
            {
                //Report($"BuildOHBShop -  {e.Message}");
            }
        }

        public static async Task GetInfoShopAsync(string shopUrl)
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
            catch (XmlException e)
            {
                await LogAsync(e.Message);
                await LogAsync("Пробую обновить магазин...");
                await UpdateOneHomeBeautyAsync();
            }
            catch (Exception e)
            {
                await LogAsync(e.Message);
            }

        }

        public static async Task UpdateOneHomeBeautyAsync(IProgress<string> _progress = null)
        {
            progress = _progress;
            // Проверяем есть ли подключение к интернету
            if (InternetAvailable())
            {
                ListShopXML = await LoadListShopsAsync();
                await GetShopsAsync();
                await SaveXml();
                await UploadShopAsync();
            }
            else
            {
                await LogAsync("Нет подключения к интернету, обновление не состоялось");
            }
        }
    }
    namespace Helpers
    {
        public static class ToastNotifications
        {
            public static void ShowToast(string title, string content, string image = "", string logo = "")
            {
                // In a real app, these would be initialized with actual data
                //string title = "Andrew sent you a picture";
                //string content = "Check this out, Happy Canyon in Utah!";
                //string image = "https://picsum.photos/360/202?image=883";
                //string logo = "ms-appdata:///local/Andrew.jpg";

                // Construct the visuals of the toast
                ToastVisual visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },
                        new AdaptiveText()
                        {
                            Text = content
                        },
                        new AdaptiveImage()
                        {
                            Source = image
                        }
                    },
                        AppLogoOverride = new ToastGenericAppLogo()
                        {
                            Source = logo,
                            HintCrop = ToastGenericAppLogoCrop.Circle
                        }
                    }
                };
                // Now we can construct the final toast content
                ToastContent toastContent = new ToastContent()
                {
                    Visual = visual,
                };

                // And create the toast notification
                var toast = new ToastNotification(toastContent.GetXml());
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
        }
        public static class Files
        {
            private readonly static string logFileName = "update.log";
            public static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            public static StorageFile LogFile { get; set; }
            public static async Task LogAsync(string text)
            {
                try
                {
                    // получаем локальную папку
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    string filePath = "update.log";
                    // получаем файл
                    StorageFile logFile = await localFolder.GetFileAsync(filePath);
                    FileInfo fi = new FileInfo(logFile.Path);
                    if (fi.Length < 1000)
                    {
                        await FileIO.AppendTextAsync(logFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm - ") + text + "\r\n");
                    }
                    else
                    {
                        await FileIO.WriteTextAsync(logFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm - ") + text + "\r\n");
                    }
                }
                catch (Exception e)
                {
                    await LogAsync(e.Message);
                }
                finally
                {

                }
            }
            public static async Task<string> GetLogFileTextAsync()
            {
                try
                {
                    // получаем файл
                    LogFile = await localFolder.GetFileAsync(logFileName);
                    // читаем файл
                    string text = await FileIO.ReadTextAsync(LogFile);
                    //txtStatus.Text += text + "\r\n";
                    return text + "\r\n";

                }
                catch (FileNotFoundException)
                {
                    return "Файл лога не найден";
                }
            }
            public static async Task CreateLogFileAsync()
            {
                try
                {
                    StorageFile file = await localFolder.GetFileAsync(logFileName);
                }
                catch (FileNotFoundException)
                {
                    //txtStatus.Text += "File not found, - creating..." + "\r\n";
                    LogFile = await localFolder.CreateFileAsync(logFileName, CreationCollisionOption.ReplaceExisting);
                    //txtStatus.Text += "File was creation - " + LogFile.Path + "\r\n";
                }
                catch (Exception e)
                {
                    string s = e.Message + "\r\n" + e.HelpLink + "\r\n";
                    ShowToast("Error", s);
                }

            }

            public static string FolderOHB_Local { get; } = ApplicationData.Current.LocalFolder.Path + @"\";
            public static string FolderOHB_Remote { get; } = @"http://onebeauty.com.ua/files/";
            public static string FileOHB_Logfile { get; set; }
            public static string FileOHB_Shop { get; } = "onehomebeauty.xml"; 
            public static string FileOHB_ListShops { get; } = "shops-yml.xml";

            public static async void SaveXmlAsync(string file, XElement xmlObject)
            {
                await Task.Run(async () =>
                {
                    try
                    {
                        using (StreamWriter sw = File.CreateText(file))
                        {
                            xmlObject.Save(sw);
                        }
                    }
                    catch (Exception e)
                    {
                        await LogAsync($"SaveXmlAsync - {e.Message}");
                    }
                });
            }
            public static void SaveXml(string file, XElement xEl, out FileInfo fileInfo)
            {
                if (string.IsNullOrEmpty(file))
                {
                    throw new ArgumentException("message", nameof(file));
                }

                using (StreamWriter sw = File.CreateText(file))
                {
                    xEl.Save(sw);
                    fileInfo = new FileInfo(file);
                }
            }
        }
        public static class NetWork
        {
            /// <summary>
            /// Проверяет есть ли подключение к интернету. Возвращает Истину если Да
            /// </summary>
            /// <returns></returns>
            public static bool InternetAvailable()
            {
                var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
                return (connectionProfile != null &&
                        connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            }

            public static async Task<IEnumerable<XElement>> LoadListShopsAsync()
            {
                XDocument result;
                using (var httpclient = new HttpClient())
                {
                    var response = await httpclient.GetAsync(FolderOHB_Remote + FileOHB_ListShops);
                    result = XDocument.Load(await response.Content.ReadAsStreamAsync());
                }

                return result.Element("shops-yml").Descendants();
            }
        }
    }
}
