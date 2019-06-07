using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Storage;

namespace YMLUpload
{
    public class YML_prog 
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


        public static XElement shopTree;//Корневой узел, который содержит все магазины
        public static XDocument shopXML;//XML, который содержит все магазины

        public static async Task UpdateOneHomeBeauty(IProgress<string> progress)
        {
            await GetShopsAsync(progress, true);
            SaveXml();
            UploadShopAsync(progress);
        }

        private static StorageFile xmlFile;

        public static async void UploadShopAsync(IProgress<string> progress=null)
        {
            try
            {
                progress?.Report("Upload on ftp...");
                FtpClient client = new FtpClient();
                client.Host = "elchukof.ftp.tools";
                client.Credentials = new NetworkCredential("elchukof_tks", "gAdP6K51mMh9");
                await client.ConnectAsync();
                await client.UploadFileAsync(xmlFile.Path, "/www/files/onehomebeauty.xml");
                progress?.Report("File Uploaded! " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                progress.Report(ex.ToString());
            }

        }

        private static async void SaveXml()
        {
            // получаем локальную папку
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            // создаем файл
            xmlFile = await localFolder.CreateFileAsync(file,
                                                CreationCollisionOption.ReplaceExisting);
            // запись в файл
            await FileIO.WriteTextAsync(xmlFile, YMLUpload.YML_prog.shopTree.ToString());

            //await new Windows.UI.Popups.MessageDialog("Файл создан и сохранен").ShowAsync();
        }

        public static async Task LoadShopAsync(string url_shop, IProgress<string> _progress)
        {
            try
            {
                //***************************************************************
                //загружаем магазин
                Uri uri = new Uri(url_shop);
                XDocument xYMLCatalog;
                _progress?.Report("Загрузка товаров с " + uri.Host);
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
                _progress?.Report("категорий - " + xCategories.Count() + "; товаров - " + xOffers.Count() + "; обновлено: " + xYMLCatalog.Element(yml_catalog).Attribute(date).Value);
                XAttribute xCatalogAttribute = xYMLCatalog.Element(yml_catalog).Attribute(date);
                DateTime lastUpdate = DateTime.Parse(xCatalogAttribute.Value);//дата последнего обновления
            }
            catch (XmlException xmlEx)
            {
                //MessageBox.Show(xmlEx.Message);
                _progress.Report(xmlEx.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                _progress.Report(ex.Message);
            }
            //return null;
        }

        public static async Task GetShopsAsync(IProgress<string> progress=null, bool upload = false)
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
                    await LoadShopAsync(addresxml.Value, progress);
                }
                progress?.Report("Всего категорий - " + shopTree.Element(shop).Element(categories).Elements().Count()
                               + "; товаров - " + shopTree.Element(shop).Element(offers).Elements().Count());

                shopXML = new XDocument();
                shopXML.Add(shopTree);
            }
            catch(Exception ex)
            {
                progress?.Report(ex.ToString());
            }
        }

        public static async Task GetInfoShopAsync(string shopUrl, IProgress<string> progress)
        {
            try
            {
                progress.Report("Читаю " + shopUrl);
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
            catch(Exception ex)
            {
                progress.Report(ex.ToString());
            }

        }

    }

}

