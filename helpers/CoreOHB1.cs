//using CoreOHB.Helpers;
//using FluentFTP;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;
//using Windows.Storage;
//using Windows.UI.Xaml.Controls;

//namespace CoreOHB
//{
//    public static class OHB_Core
//    {
//        #region Constants Names
//        public static readonly XName yml_catalog = "yml_catalog";
//        public static readonly XName shop = "shop";
//        public static readonly XName offers = "offers";
//        public static readonly XName categoryId = "categoryId";
//        public static readonly XName category = "category";
//        public static readonly XName categories = "categories";
//        public static readonly XName groups = "groups";
//        public static readonly XName root = "root";
//        public static readonly XName id = "id";
//        public static readonly XName parentId = "parentId";
//        public static readonly XName date = "date";
//        public static readonly XName url = "url";
//        public static readonly XName available = "available";
//        public static readonly XName name = "name";
//        public static readonly XName description = "description";
//        public static readonly XName price = "price";
//        public static readonly XName selling_type = "selling_type";
//        public static readonly XName vendorCode = "vendorCode";
//        #endregion


//        /// <summary>
//        /// главный файл магазина OHB - строится из секций OHBStructure и OHBOffers
//        /// </summary>
//        public static XElement xOHBShop { get; set; }

//        /// <summary>
//        /// Главный XML элемент магазина OHB - структура магазина в виде XML дерева
//        /// </summary>
//        public static XElement xOHBShopTreeXML { get; set; }

//        //public static TreeShopNode tnsOHBShop { get; set; }  // Главный элемент treenode магазина OHB



//        static IProgress<string> _progress;

//        public static TreeView treeViewStructure;
//        public static TreeView treeViewShops;

//        /// <summary>
//        /// Структура магазина - список каталогов и подкаталогов - секция "categories"
//        /// </summary>
//        public static XElement xOHBCategories { get; set; }
//        /// <summary>
//        /// товары магазина - секция "offers"
//        /// </summary>
//        public static XElement xOHBOffers { get; set; } = new XElement("offers");

//        /// <summary>
//        /// Общее количество товаров со всех выгрузок. 
//        /// </summary>
//        public static XElement shopTreeOffers = new XElement(offers);

//        /// <summary>
//        /// XElement "Relations" в котором хранятся привязки - пара categoryId-relatedId 
//        /// <para>categoryId - id категории выгрузки, к которой относится товар</para>
//        /// <para>relatedId - id категории магазина OHB, к которой будет привязан товар</para>
//        /// </summary>
//        public static XElement xOHBRelations { get; set; } = new XElement("Relations");

//        public static async Task UploadFileAsync(string fileName)
//        {
//            try
//            {
//                string file = Path.GetFileName(fileName);
//                Report($"Uploading {file} to ftp...");
//                //create an FTP client
//                FtpClient client = new FtpClient
//                {
//                    Host = "ftp://ftp.s51.freehost.com.ua/",
//                    Credentials = new NetworkCredential("granitmar1_ohbed", "Va3NeMHzyY")
//                };
//                await client.ConnectAsync();
//                await client.UploadFileAsync(file, Files.FolderOHB_Remote + Files.FileOHB_Shop);

//                //using (WebClient client = new WebClient())
//                //{

//                //    client.Credentials = new NetworkCredential("granitmar1_ohbed", "Va3NeMHzyY");
//                //    byte[] responseArray = await client.UploadFileTaskAsync("ftp://ftp.s51.freehost.com.ua/" + file,
//                //                                                            "STOR",
//                //                                                            Path.Combine(Files.FolderOHB_Local, file));
//                //    Report(Encoding.UTF8.GetString(responseArray) == "" ? "Ok!" :
//                //                     Encoding.UTF8.GetString(responseArray));
//                //}
//                //Report($"Ok!");
//            }
//            catch (Exception ex)
//            {
//                Report(ex.Message);
//            }
//        }


//        /// <summary>
//        /// Загружает полностью магазин поставщика с указанного URL
//        /// </summary>
//        /// <param name="url_shop"></param>
//        /// <returns></returns>
//        private static async Task<XDocument> GetShopsForXMLAsync(string url_shop)
//        {
//            try
//            {
//                //***************************************************************
//                //загружаем магазин
//                Uri uri = new Uri(url_shop);
//                Report($"{uri.OriginalString} загрузка...");

//                XDocument xYMLCatalog;
//                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//                WebRequest request = WebRequest.Create(url_shop);
//                Encoding encoding;
//                if (url_shop.Contains("Platok"))
//                    encoding = Encoding.GetEncoding("windows-1251");
//                else
//                    encoding = Encoding.GetEncoding("utf-8");
                
//                using (WebResponse response = await request.GetResponseAsync())
//                {
//                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
//                    {
//                        xYMLCatalog = XDocument.Load(reader);
//                    }
//                }
//                return xYMLCatalog;
//            }
//            catch (Exception ex)
//            {
//                Report($"GetShopsForXMLAsync - {ex.Message}");
//                return null;
//            }
//        }

//        /// <summary>
//        /// Using: XElement element = myClass.ToXElement<MyClass>();
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        public static XElement ToXElement<T>(this object obj)
//        {
//            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
//            ns.Add(string.Empty, string.Empty);
//            using (var memoryStream = new MemoryStream())
//            {
//                using (TextWriter streamWriter = new StreamWriter(memoryStream))
//                {
//                    var xmlSerializer = new XmlSerializer(typeof(T));
//                    xmlSerializer.Serialize(streamWriter, obj, ns);
//                    return XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
//                }
//            }
//        }

//        /// <summary>
//        /// Using: var newMyClass = element.FromXElement<MyClass>();
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="xElement"></param>
//        /// <returns></returns>
//        public static T FromXElement<T>(this XElement xElement)
//        {
//            var xmlSerializer = new XmlSerializer(typeof(T));
//            XmlReader reader = xElement.CreateReader();
//            reader.MoveToContent();
//            return (T)xmlSerializer.Deserialize(reader);
//        }

//        /// <summary>
//        /// Загружает выгрузки. 
//        /// Заполняет TreeView
//        /// </summary>
//        /// <param name="progress"></param>
//        /// <param name="progress2"></param>
//        /// <param name="treeView"></param>
//        /// <param name="treeViewExcludes"></param>
//        /// <returns></returns>
//        //
//        public static async Task GetShopsAsync()
//        {
//            try
//            {
//                //загружаем список магазинов
//                XDocument xdoc = XDocument.Load(Files.FolderOHB_Remote + Files.FileOHB_ListShops);
//                if (xdoc == null)
//                    throw new Exception($"Не могу загрузить {Files.FolderOHB_Remote + Files.FileOHB_ListShops}");

//                int count = xdoc.Element("shops-yml").Elements().Count();
//                Task<XDocument>[] tasks = new Task<XDocument>[count];
//                int i = 0;
//                foreach (XElement addresxml in xdoc.Element("shops-yml").Descendants()) //XElement addresxml in xdoc.Element("shops-yml").Descendants()
//                {
//                    tasks[i] = new Task<XDocument>(async () => await GetShopsForXMLAsync(addresxml.Value));
//                    tasks[i].Start();
//                    i++;
//                }
//                //await Task.WhenAll(task); // ожидаем завершения задач 
//                var processingTasks = tasks.Select(AwaitAndProcessAsync).ToList();
//                await Task.WhenAll(processingTasks);

//            }
//            catch (Exception e)
//            {
//                Report($"GetShopsAsync - {e.Message}");
//                return;
//            }
//        }

//        static async Task AwaitAndProcessAsync(Task<XDocument> task)
//        {
//            var result = await task;
//            //if (result != null)
//                //FillTree(result);
//        }

//        /// <summary>
//        /// Строит основной магазин. Категории берутся из отдельного файла и представляют структуру магазина.
//        /// Здесь заполняются xOHBShop и OHBCategories
//        /// </summary>
//        /// <param name="progress"></param>
//        /// <param name="progress2"></param>
//        /// <param name="treeView"></param>
//        /// <returns></returns>
//        public static void BuildOHBShopAsync()
//        {
//            try
//            {
//                string time_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
//                xOHBShop = new XElement(yml_catalog, new XAttribute(date, time_update),
//                               new XElement(shop,
//                                   new XElement(url, "http://onehomebeauty.com.ua")));
//                xOHBShop.Element(shop).Add(xOHBCategories, new XElement(offers));
//                XDocument xDoc = new XDocument(new XComment("YML file of OneHomeBeauty shop"), xOHBShop);
//            }
//            catch (Exception e)
//            {
//                Report($"BuildOHBShop -  {e.Message}");
//            }
//        }

//        public static void Report(string message)
//        {
//            string time_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
//            _progress?.Report($"{time_update} - {message}");
//        }
//        class OfferComparer : IEqualityComparer<XElement>
//        {
//            public bool Equals(XElement x, XElement y)
//            {
//                return x.Attribute("id").Value == y.Attribute("id").Value && x.Element("name").Value == y.Element("name").Value;
//            }


//            // If Equals() returns true for a pair of objects 
//            // then GetHashCode() must return the same value for these objects.

//            public int GetHashCode(XElement product)
//            {
//                //Check whether the object is null
//                if (object.ReferenceEquals(product, null)) return 0;

//                //Get hash code for the Name field if it is not null.
//                int hashProductName = product.Value == null ? 0 : product.Value.GetHashCode();

//                //Get hash code for the Code field.
//                int hashProductCode = product.Value.GetHashCode();

//                //Calculate the hash code for the product.
//                return hashProductName ^ hashProductCode;
//            }

//        }
//        public static T DeserializeObject<T>(Stream stream)
//        {
//            stream.Position = 0;
//            // Create an instance of the XmlSerializer.
//            XmlSerializer serializer = new XmlSerializer(typeof(T));
//            T res = (T)serializer.Deserialize(stream);

//            //TextWriter myTextWriter = new StreamWriter(Files.FolderOHB_Local + "temp.txt");
//            //serializer.Serialize(myTextWriter,res);
//            //myTextWriter.Close();
//            return res;
//        }
//        public static T DeserializeObject<T>(XElement xmlObject)
//        {
//            XmlSerializer serializer = new XmlSerializer(typeof(T));
//            return (T)serializer.Deserialize(xmlObject.CreateReader());
//        }

//        public static async Task Initialize(IProgress<string> progress,
//                                        TreeView _treeViewStructure,
//                                        TreeView _treeViewShops)
//        {
//            try
//            {
//                // подготовить переменные статуса
//                _progress = progress;

//                // подготовить TreeView's
//                treeViewStructure = _treeViewStructure;
//                treeViewShops = _treeViewShops;

//                //Строим шапку YML каталога и добавляем структуру - OHBCategories
//                //здесь заполняются xOHBShop и OHBCategories
//                BuildOHBShopAsync();

//                //парсинг магазинов, тут же заполняются TreeShopNode для каждой из выгрузок и xOHBOffers
//                await GetShopsAsync();

//                //Добавляем товары в магазин OHB
//                xOHBShop.Element(shop).Element(offers).Add(xOHBOffers.Elements());

//                Report("Готово! Всего категорий - " + xOHBShop.Element(shop).Element(categories).Elements().Count()
//                               + "; товаров - " + xOHBShop.Element(shop).Element(offers).Elements().Count());

//            }
//            catch (Exception e)
//            {
//                Report($"Initialize - {e.Message}");
//            }
//        }

//    }
//    namespace Helpers
//    {
//        public static class Files
//        {
//            public static string FolderOHB_Local { get; } = ApplicationData.Current.LocalFolder.Path + @"\";
//            public static string FolderOHB_Remote { get; } = @"http://onebeauty.com.ua/files/";
//            public static string FileOHB_Logfile { get; set; }
//            public static string FileOHB_Shop { get; } = Path.Combine(FolderOHB_Local, "onebeauty.xml");
//            public static string FileOHB_ListShops { get; } = "shops-yml.xml";
//            public static string FileOHB_Structure { get; } = Path.Combine(FolderOHB_Local, "categories.xml");
//            public static string FileOHB_RelationsOfCategories { get; } = Path.Combine(FolderOHB_Local, "relations.xml");

//            public static async Task SaveXMLShops(string fileName, string ymlCatalog, IProgress<string> progress)
//            {
//                try
//                {
//                    progress.Report("Сохраняю файл - " + fileName);
//                    string file = FolderOHB_Local + fileName; //+ uri.Host + ".xml";
//                    using (StreamWriter sw = File.CreateText(file))
//                    {
//                        await sw.WriteAsync(ymlCatalog);
//                    }
//                    FileInfo fi = new FileInfo(file);
//                    progress.Report("Ок - " + fi.Length);
//                }
//                catch (XmlException xmlEx)
//                {
//                    progress.Report(xmlEx.Message);
//                }
//                catch (Exception ex)
//                {
//                    progress.Report(ex.Message);
//                }
//            }

//            public static void SaveXml(string file, XElement xEl)
//            {
//                using (StreamWriter sw = File.CreateText(file))
//                {
//                    xEl.Save(sw);
//                }
//            }

//            public static async void SaveXmlAsync(string file, XElement xmlObject)
//            {
//                await Task.Run(() =>
//                {
//                    try
//                    {
//                        using (StreamWriter sw = File.CreateText(file))
//                        {
//                            xmlObject.Save(sw);
//                        }
//                    }
//                    catch (Exception e)
//                    {
//                        OHB_Core.Report($"SaveXmlAsync - {e.Message}");
//                    }
//                });
//            }

//            public static void SaveXml(string file, XElement xEl, out FileInfo fileInfo)
//            {
//                if (string.IsNullOrEmpty(file))
//                {
//                    throw new ArgumentException("message", nameof(file));
//                }

//                using (StreamWriter sw = File.CreateText(file))
//                {
//                    xEl.Save(sw);
//                    fileInfo = new FileInfo(file);
//                }
//            }
//            public static async Task<XDocument> LoadXMLAsync(string path)
//            {
//                return await Task.Run(() =>
//                {
//                    try
//                    {
//                        return XDocument.Load(path);
//                    }
//                    catch (FileNotFoundException)
//                    {
//                        return null;
//                    }
//                });

//            }
//            public static async Task<bool> CheckVersionsOfFilesAsync(string file, IProgress<string> progress)
//            {
//                XDocument loc = await LoadXMLAsync(FolderOHB_Local + file);
//                XDocument rem = await LoadXMLAsync(FolderOHB_Remote + file);

//                bool result = (loc?.Root.Attribute("date").Value == rem?.Root.Attribute("date").Value);

//                progress.Report("Локальный " + file + " - " + loc?.Root.Attribute("date").Value + "\r\n" +
//                                "На сервере " + file + " - " + rem?.Root.Attribute("date").Value);
//                return result;
//            }

//        }
//    }

//}
