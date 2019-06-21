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
                return this.shopField;
            }
            set
            {
                this.shopField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShop
    {

        private yml_catalogShopCategory[] categoriesField;

        private yml_catalogShopOffers offersField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("category", IsNullable = false)]
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

        /// <remarks/>
        public yml_catalogShopOffers offers
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

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShopCategory
    {

        private uint idField;

        private uint parentIdField;

        private bool parentIdFieldSpecified;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint parentId
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

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class yml_catalogShopOffers
    {

        private yml_catalogShopOffersItem[] itemField;

        private yml_catalogShopOffersOffer[] offerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("item")]
        public yml_catalogShopOffersItem[] item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("offer")]
        public yml_catalogShopOffersOffer[] offer
        {
            get
            {
                return this.offerField;
            }
            set
            {
                this.offerField = value;
            }
        }
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
        [System.Xml.Serialization.XmlElementAttribute("param")]
        public yml_catalogShopOffersItemParam[] param
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
        [System.Xml.Serialization.XmlElementAttribute("picture")]
        public string[] picture
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        [System.Xml.Serialization.XmlElementAttribute("categoryId", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("country_of_origin", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("currencyId", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("delivery", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("description", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("keywords", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("name", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("oldprice", typeof(decimal))]
        [System.Xml.Serialization.XmlElementAttribute("param", typeof(yml_catalogShopOffersOfferParam))]
        [System.Xml.Serialization.XmlElementAttribute("pickup", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("picture", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("portal_category_id", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("price", typeof(decimal))]
        [System.Xml.Serialization.XmlElementAttribute("priceIn", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("url", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vendor", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vendorCode", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
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
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
