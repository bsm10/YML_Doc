using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace YML_Doc
{
    ///// <remarks/>
    //[XmlType(AnonymousType = true)]
    //[XmlRoot(Namespace = "", IsNullable = false)]
    //public partial class yml_catalog
    //{
    //    private yml_catalogShop shopField;

    //    private string dateField;

    //    /// <remarks/>
    //    public yml_catalogShop shop
    //    {
    //        get
    //        {
    //            return this.shopField;
    //        }
    //        set
    //        {
    //            this.shopField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [XmlAttribute("date")]
    //    public string date
    //    {
    //        get
    //        {
    //            return this.dateField;
    //        }
    //        set
    //        {
    //            this.dateField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[XmlType(AnonymousType = true)]
    //public partial class yml_catalogShop
    //{

    //    private string urlField;

    //    private yml_catalogShopCurrencies currenciesField;

    //    private yml_catalogShopCategory[] categoriesField;

    //    private yml_catalogShopItem[] offersField;

    //    /// <remarks/>
    //    public string url
    //    {
    //        get
    //        {
    //            return this.urlField;
    //        }
    //        set
    //        {
    //            this.urlField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public yml_catalogShopCurrencies currencies
    //    {
    //        get
    //        {
    //            return this.currenciesField;
    //        }
    //        set
    //        {
    //            this.currenciesField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [XmlArrayItem("category", IsNullable = false)]
    //    public yml_catalogShopCategory[] categories
    //    {
    //        get
    //        {
    //            return this.categoriesField;
    //        }
    //        set
    //        {
    //            this.categoriesField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [XmlArrayItem(IsNullable = false)]
    //    public yml_catalogShopItem[] offers
    //    {
    //        get
    //        {
    //            return this.offersField;
    //        }
    //        set
    //        {
    //            this.offersField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[XmlType(AnonymousType = true)]
    //public partial class yml_catalogShopCurrencies
    //{

    //    private yml_catalogShopCurrenciesCurrency currencyField;

    //    /// <remarks/>
    //    public yml_catalogShopCurrenciesCurrency currency
    //    {
    //        get
    //        {
    //            return this.currencyField;
    //        }
    //        set
    //        {
    //            this.currencyField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[XmlType(AnonymousType = true)]
    //public partial class yml_catalogShopCurrenciesCurrency
    //{

    //    private string idField;

    //    private byte rateField;

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttributeAttribute()]
    //    public string id
    //    {
    //        get
    //        {
    //            return this.idField;
    //        }
    //        set
    //        {
    //            this.idField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttributeAttribute()]
    //    public byte rate
    //    {
    //        get
    //        {
    //            return this.rateField;
    //        }
    //        set
    //        {
    //            this.rateField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[XmlType(AnonymousType = true)]
    //public partial class yml_catalogShopCategory
    //{

    //    private byte idField;

    //    private byte parentIdField;

    //    private bool parentIdFieldSpecified;

    //    private string valueField;

    //    /// <remarks/>
    //    [XmlAttribute()]
    //    public byte id
    //    {
    //        get
    //        {
    //            return this.idField;
    //        }
    //        set
    //        {
    //            this.idField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [XmlAttribute()]
    //    public byte parentId
    //    {
    //        get
    //        {
    //            return this.parentIdField;
    //        }
    //        set
    //        {
    //            this.parentIdField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [XmlIgnore()]
    //    public bool parentIdSpecified
    //    {
    //        get
    //        {
    //            return this.parentIdFieldSpecified;
    //        }
    //        set
    //        {
    //            this.parentIdFieldSpecified = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [XmlText()]
    //    public string Value
    //    {
    //        get
    //        {
    //            return this.valueField;
    //        }
    //        set
    //        {
    //            this.valueField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[XmlType(AnonymousType = true)]
    //public partial class yml_catalogShopItem
    //{

    //    private string vendorCodeField;

    //    private byte categoryIdField;

    //    private string nameField;

    //    private decimal priceField;

    //    private string availableField;

    //    private decimal price_optField;

    //    private string[] pictureField;

    //    private string descriptionField;

    //    private string idField;

    //    private string selling_typeField;

    //    /// <remarks/>
    //    public string vendorCode
    //    {
    //        get
    //        {
    //            return this.vendorCodeField;
    //        }
    //        set
    //        {
    //            this.vendorCodeField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte categoryId
    //    {
    //        get
    //        {
    //            return this.categoryIdField;
    //        }
    //        set
    //        {
    //            this.categoryIdField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string name
    //    {
    //        get
    //        {
    //            return this.nameField;
    //        }
    //        set
    //        {
    //            this.nameField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public decimal price
    //    {
    //        get
    //        {
    //            return this.priceField;
    //        }
    //        set
    //        {
    //            this.priceField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string available
    //    {
    //        get
    //        {
    //            return this.availableField;
    //        }
    //        set
    //        {
    //            this.availableField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public decimal price_opt
    //    {
    //        get
    //        {
    //            return this.price_optField;
    //        }
    //        set
    //        {
    //            this.price_optField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlElementAttribute("picture")]
    //    public string[] picture
    //    {
    //        get
    //        {
    //            return this.pictureField;
    //        }
    //        set
    //        {
    //            this.pictureField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string description
    //    {
    //        get
    //        {
    //            return this.descriptionField;
    //        }
    //        set
    //        {
    //            this.descriptionField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttributeAttribute()]
    //    public string id
    //    {
    //        get
    //        {
    //            return this.idField;
    //        }
    //        set
    //        {
    //            this.idField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlAttributeAttribute()]
    //    public string selling_type
    //    {
    //        get
    //        {
    //            return this.selling_typeField;
    //        }
    //        set
    //        {
    //            this.selling_typeField = value;
    //        }
    //    }
    //}
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class yml_catalog
    {

        private yml_catalogShop shopField;

        private string dateField;

        /// <remarks/>
        public yml_catalogShop shop
        {
            get
            {
                return shopField;
            }
            set
            {
                shopField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string date
        {
            get
            {
                return dateField;
            }
            set
            {
                dateField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShop
    {
        [XmlElement("url")]
        public string url { get; set; }
        /// <remarks/>
        [XmlArrayItem("category", IsNullable = false)]
        public yml_catalogShopCategory[] categories { get; set; }

        /// <remarks/>
        public yml_catalogShopOffers offers { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShopCategory
    {

        /// <remarks/>
        [XmlAttribute()]
        public uint id { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public uint parentId { get; set; }

        /// <remarks/>
        [XmlIgnore()]
        public bool parentIdSpecified { get; set; }

        /// <remarks/>
        [XmlText()]
        public string Value { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShopOffers
    {

        /// <remarks/>
        [XmlElement("item")]
        public yml_catalogShopOffersItem[] item { get; set; }

        /// <remarks/>
        [XmlElement("offer")]
        public yml_catalogShopOffersOffer[] offer { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShopOffersItem
    {

        private string vendorCodeField;

        private byte categoryIdField;

        private string nameField;

        private decimal priceField;

        private string availableField;

        private decimal price_optField;

        private yml_catalogShopOffersItemParam[] paramField;

        private string[] pictureField;

        private string descriptionField;

        private string idField;

        private string selling_typeField;

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
        public decimal price
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
        public string available
        {
            get
            {
                return this.availableField;
            }
            set
            {
                this.availableField = value;
            }
        }

        /// <remarks/>
        public decimal price_opt
        {
            get
            {
                return this.price_optField;
            }
            set
            {
                this.price_optField = value;
            }
        }

        /// <remarks/>
        [XmlElement("param")]
        public yml_catalogShopOffersItemParam[] param
        {
            get
            {
                return paramField;
            }
            set
            {
                this.paramField = value;
            }
        }

        /// <remarks/>
        [XmlElement("picture")]
        public string[] picture
        {
            get
            {
                return pictureField;
            }
            set
            {
                pictureField = value;
            }
        }

        /// <remarks/>
        public string description
        {
            get
            {
                return descriptionField;
            }
            set
            {
                descriptionField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
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
        [XmlAttribute()]
        public string selling_type
        {
            get
            {
                return this.selling_typeField;
            }
            set
            {
                this.selling_typeField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShopOffersItemParam
    {

        private string nameField;

        private string unitField;

        private string valueField;

        /// <remarks/>
        [XmlAttribute()]
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
        [XmlAttribute()]
        public string unit
        {
            get
            {
                return this.unitField;
            }
            set
            {
                this.unitField = value;
            }
        }

        /// <remarks/>
        [XmlText()]
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

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShopOffersOffer
    {

        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        private uint idField;

        private bool availableField;

        private string selling_typeField;

        private uint group_idField;

        private bool group_idFieldSpecified;

        /// <remarks/>
        [XmlElement("categoryId", typeof(uint))]
        [XmlElement("country_of_origin", typeof(string))]
        [XmlElement("currencyId", typeof(string))]
        [XmlElement("delivery", typeof(bool))]
        [XmlElement("description", typeof(string))]
        [XmlElement("keywords", typeof(string))]
        [XmlElement("name", typeof(string))]
        [XmlElement("oldprice", typeof(decimal))]
        [XmlElement("param", typeof(yml_catalogShopOffersOfferParam))]
        [XmlElement("pickup", typeof(bool))]
        [XmlElement("picture", typeof(string))]
        [XmlElement("portal_category_id", typeof(string))]
        [XmlElement("price", typeof(decimal))]
        [XmlElement("priceIn", typeof(string))]
        [XmlElement("url", typeof(string))]
        [XmlElement("vendor", typeof(string))]
        [XmlElement("vendorCode", typeof(string))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlElement("ItemsElementName")]
        [XmlIgnore()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public uint id
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
        [XmlAttribute()]
        public bool available
        {
            get
            {
                return this.availableField;
            }
            set
            {
                this.availableField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string selling_type
        {
            get
            {
                return this.selling_typeField;
            }
            set
            {
                this.selling_typeField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public uint group_id
        {
            get
            {
                return this.group_idField;
            }
            set
            {
                this.group_idField = value;
            }
        }

        /// <remarks/>
        [XmlIgnore()]
        public bool group_idSpecified
        {
            get
            {
                return this.group_idFieldSpecified;
            }
            set
            {
                this.group_idFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShopOffersOfferParam
    {

        private string nameField;

        private string unitField;

        private string valueField;

        /// <remarks/>
        [XmlAttribute()]
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
        [XmlAttribute()]
        public string unit
        {
            get
            {
                return this.unitField;
            }
            set
            {
                this.unitField = value;
            }
        }

        /// <remarks/>
        [XmlText()]
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

    /// <remarks/>
    [XmlType(IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        categoryId,

        /// <remarks/>
        country_of_origin,

        /// <remarks/>
        currencyId,

        /// <remarks/>
        delivery,

        /// <remarks/>
        description,

        /// <remarks/>
        keywords,

        /// <remarks/>
        name,

        /// <remarks/>
        oldprice,

        /// <remarks/>
        param,

        /// <remarks/>
        pickup,

        /// <remarks/>
        picture,

        /// <remarks/>
        portal_category_id,

        /// <remarks/>
        price,

        /// <remarks/>
        priceIn,

        /// <remarks/>
        url,

        /// <remarks/>
        vendor,

        /// <remarks/>
        vendorCode,
    }

}
