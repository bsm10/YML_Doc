using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YML_Doc.ViewModel
{
    public partial class YML_Catalog
    {

        private yml_catalogShop shopField;

        private string dateField;

        /// <remarks/>
        public yml_catalogShop shop
        {
            get
            {
                return this.shopField;
            }
            set
            {
                this.shopField = value;
            }
        }

        public string date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }
    }

    public partial class yml_catalogShop
    {

        private string nameField;

        private string companyField;

        private string urlField;

        private yml_catalogShopCurrency[] currenciesField;

        private yml_catalogShopCategory[] categoriesField;

        private yml_catalogShopDeliveryoptions deliveryoptionsField;

        private yml_catalogShopOffer[] offersField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string company
        {
            get
            {
                return this.companyField;
            }
            set
            {
                this.companyField = value;
            }
        }

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        public yml_catalogShopCurrency[] currencies
        {
            get
            {
                return this.currenciesField;
            }
            set
            {
                this.currenciesField = value;
            }
        }

        public yml_catalogShopCategory[] categories
        {
            get
            {
                return this.categoriesField;
            }
            set
            {
                this.categoriesField = value;
            }
        }

        public yml_catalogShopDeliveryoptions deliveryoptions
        {
            get
            {
                return this.deliveryoptionsField;
            }
            set
            {
                this.deliveryoptionsField = value;
            }
        }

        public yml_catalogShopOffer[] offers
        {
            get
            {
                return this.offersField;
            }
            set
            {
                this.offersField = value;
            }
        }
    }

    public partial class yml_catalogShopCurrency
    {

        private string idField;

        private byte rateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte rate
        {
            get
            {
                return this.rateField;
            }
            set
            {
                this.rateField = value;
            }
        }
    }

    public partial class yml_catalogShopCategory
    {

        private byte idField;

        private byte parentIdField;

        private bool parentIdFieldSpecified;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte parentId
        {
            get
            {
                return this.parentIdField;
            }
            set
            {
                this.parentIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool parentIdSpecified
        {
            get
            {
                return this.parentIdFieldSpecified;
            }
            set
            {
                this.parentIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    public partial class yml_catalogShopDeliveryoptions
    {

        private yml_catalogShopDeliveryoptionsOption optionField;

        /// <remarks/>
        public yml_catalogShopDeliveryoptionsOption option
        {
            get
            {
                return this.optionField;
            }
            set
            {
                this.optionField = value;
            }
        }
    }

    public partial class yml_catalogShopDeliveryoptionsOption
    {

        private ushort costField;

        private byte daysField;

        private byte orderbeforeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort cost
        {
            get
            {
                return this.costField;
            }
            set
            {
                this.costField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte days
        {
            get
            {
                return this.daysField;
            }
            set
            {
                this.daysField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("order-before")]
        public byte orderbefore
        {
            get
            {
                return this.orderbeforeField;
            }
            set
            {
                this.orderbeforeField = value;
            }
        }
    }

    public partial class yml_catalogShopOffer
    {

        private string typePrefixField;

        private string nameField;

        private string vendorField;

        private ushort modelField;

        private bool modelFieldSpecified;

        private string vendorCodeField;

        private string urlField;

        private ushort priceField;

        private ushort oldpriceField;

        private string currencyIdField;

        private byte categoryIdField;

        private string pictureField;

        private bool storeField;

        private bool pickupField;

        private bool deliveryField;

        private yml_catalogShopOfferDeliveryoptions deliveryoptionsField;

        private string descriptionField;

        private yml_catalogShopOfferParam paramField;

        private string sales_notesField;

        private bool manufacturer_warrantyField;

        private string country_of_originField;

        private ulong barcodeField;

        private ushort idField;

        private byte bidField;

        private string typeField;

        /// <remarks/>
        public string typePrefix
        {
            get
            {
                return this.typePrefixField;
            }
            set
            {
                this.typePrefixField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string vendor
        {
            get
            {
                return this.vendorField;
            }
            set
            {
                this.vendorField = value;
            }
        }

        /// <remarks/>
        public ushort model
        {
            get
            {
                return this.modelField;
            }
            set
            {
                this.modelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool modelSpecified
        {
            get
            {
                return this.modelFieldSpecified;
            }
            set
            {
                this.modelFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string vendorCode
        {
            get
            {
                return this.vendorCodeField;
            }
            set
            {
                this.vendorCodeField = value;
            }
        }

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        public ushort price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }

        /// <remarks/>
        public ushort oldprice
        {
            get
            {
                return this.oldpriceField;
            }
            set
            {
                this.oldpriceField = value;
            }
        }

        /// <remarks/>
        public string currencyId
        {
            get
            {
                return this.currencyIdField;
            }
            set
            {
                this.currencyIdField = value;
            }
        }

        /// <remarks/>
        public byte categoryId
        {
            get
            {
                return this.categoryIdField;
            }
            set
            {
                this.categoryIdField = value;
            }
        }

        /// <remarks/>
        public string picture
        {
            get
            {
                return this.pictureField;
            }
            set
            {
                this.pictureField = value;
            }
        }

        /// <remarks/>
        public bool store
        {
            get
            {
                return this.storeField;
            }
            set
            {
                this.storeField = value;
            }
        }

        /// <remarks/>
        public bool pickup
        {
            get
            {
                return this.pickupField;
            }
            set
            {
                this.pickupField = value;
            }
        }

        /// <remarks/>
        public bool delivery
        {
            get
            {
                return this.deliveryField;
            }
            set
            {
                this.deliveryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("delivery-options")]
        public yml_catalogShopOfferDeliveryoptions deliveryoptions
        {
            get
            {
                return this.deliveryoptionsField;
            }
            set
            {
                this.deliveryoptionsField = value;
            }
        }

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public yml_catalogShopOfferParam param
        {
            get
            {
                return this.paramField;
            }
            set
            {
                this.paramField = value;
            }
        }

        /// <remarks/>
        public string sales_notes
        {
            get
            {
                return this.sales_notesField;
            }
            set
            {
                this.sales_notesField = value;
            }
        }

        /// <remarks/>
        public bool manufacturer_warranty
        {
            get
            {
                return this.manufacturer_warrantyField;
            }
            set
            {
                this.manufacturer_warrantyField = value;
            }
        }

        /// <remarks/>
        public string country_of_origin
        {
            get
            {
                return this.country_of_originField;
            }
            set
            {
                this.country_of_originField = value;
            }
        }

        /// <remarks/>
        public ulong barcode
        {
            get
            {
                return this.barcodeField;
            }
            set
            {
                this.barcodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte bid
        {
            get
            {
                return this.bidField;
            }
            set
            {
                this.bidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    public partial class yml_catalogShopOfferDeliveryoptions
    {

        private yml_catalogShopOfferDeliveryoptionsOption optionField;

        /// <remarks/>
        public yml_catalogShopOfferDeliveryoptionsOption option
        {
            get
            {
                return this.optionField;
            }
            set
            {
                this.optionField = value;
            }
        }
    }

    public partial class yml_catalogShopOfferDeliveryoptionsOption
    {

        private ushort costField;

        private byte daysField;

        private byte orderbeforeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort cost
        {
            get
            {
                return this.costField;
            }
            set
            {
                this.costField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte days
        {
            get
            {
                return this.daysField;
            }
            set
            {
                this.daysField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("order-before")]
        public byte orderbefore
        {
            get
            {
                return this.orderbeforeField;
            }
            set
            {
                this.orderbeforeField = value;
            }
        }
    }

    public partial class yml_catalogShopOfferParam
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }


}
