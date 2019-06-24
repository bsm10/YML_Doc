using System.IO;
using System.Xml.Serialization;

namespace YML_Doc
{
    public class ViewModel
    {
        //public yml_catalog _YMLCatalog;
        public yml_catalog YMLCatalog { get; }
        //public string Url { get; set; }

        public ViewModel()
        {
            //Url=_url;
            XmlSerializer serializer = new XmlSerializer(typeof(yml_catalog));

            using (Stream reader = new FileStream(@"DataFiles/top_group.xml", FileMode.Open, FileAccess.Read))
            //using (Stream reader = new FileStream(@"DataFiles/onehomebeauty.xml", FileMode.Open, FileAccess.Read))
            {
                YMLCatalog = (yml_catalog)serializer.Deserialize(reader);
            }
        }
    }
}
