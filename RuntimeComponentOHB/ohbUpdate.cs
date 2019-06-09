using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.ApplicationModel.Background;
using Windows.Storage;



namespace RuntimeComponentOHB
{
    public sealed class YMLShopUpdate : IBackgroundTask
    {
        private static readonly string file = @"onehomebeauty.xml";

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

        BackgroundTaskDeferral _deferral;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            try
            {
                
                await UpdateOneHomeBeautyAsync();
            }
            finally
            {
                _deferral.Complete();
            }
        }
        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            // Indicate that the background task is canceled.
            //_cancelRequested = true;

            //Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested...");
        }
        private static async Task UpdateOneHomeBeautyAsync()
        {
            await GetShopsAsync();
            await SaveXml();
            await UploadShopAsync();
        }

        private static StorageFile xmlFile;

        private static async Task UploadShopAsync()
        {
            try
            {
                //progress?.Report("Upload on ftp...");
                FtpClient client = new FtpClient();
                client.Host = "elchukof.ftp.tools";
                client.Credentials = new NetworkCredential("elchukof_tks", "gAdP6K51mMh9");
                //client.Connect();
                //client.UploadFile(xmlFile.Path, "/www/files/onehomebeauty.xml");
                await client.ConnectAsync();
                await client.UploadFileAsync(xmlFile.Path, "/www/files/onehomebeauty.xml");
                await LogAsync("Upload - ok!");
                //progress?.Report("File Uploaded! " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                await LogAsync(ex.Message);
            }
            finally
            {
                
            }

        }

        private static async Task SaveXml()
        {
            try
            {
                // получаем локальную папку
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                // создаем файл
                xmlFile = await localFolder.CreateFileAsync(file,
                                                    CreationCollisionOption.ReplaceExisting);
                // запись в файл
                await FileIO.WriteTextAsync(xmlFile, shopTree.ToString());
                FileInfo fi = new FileInfo(xmlFile.Path);
                await LogAsync(xmlFile.Name + " - " + fi.Length.FileSizeToString());
            }
            catch(Exception e)
            {
                await LogAsync(e.Message);
            }
        }

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
                await LogAsync(xmlEx.Message);
                //MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                await LogAsync(ex.Message);
                //MessageBox.Show(ex.Message);
            }
            //return null;
        }

        private static async Task GetShopsAsync()
        {
            try
            {
                //загружаем список магазинов
                XDocument xdoc = XDocument.Load("shops-yml.xml");
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
                    await LoadShopAsync(addresxml.Value);
                }
                shopXML = new XDocument();
                shopXML.Add(shopTree);
            }
            catch(Exception e)
            {
                await LogAsync(e.Message);
            }
        }

        private static async Task LogAsync(string text)
        {
            try
            {
                // получаем локальную папку
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                string filePath = "update.log";
                // получаем файл
                StorageFile logFile = await localFolder.GetFileAsync(filePath);
                await FileIO.AppendTextAsync(logFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm - ") + text + "\r\n");
            }
            finally
            {
                
            }
        }

    }
}

