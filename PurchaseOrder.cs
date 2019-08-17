using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PurchaseOrderService.DataContract
{
    [DataContract]
    public class PurchaseOrder
    {
        string _orderNo = "";
        DateTime _purchaseDate = default(DateTime);
        string _supplierNo = "";
        string _itemCode = "";
        int _quantity = 0;
        string _supplierName = "";
        string _supplierAddress = "";
        string _itemDescription = "";
        decimal _itemRate = 0;

        List<Supplier> _supplierList = new List<Supplier>();
        List<Item> _itemList = new List<Item>();

        [DataMember]
        public List<Supplier> SupplierList
        {
            get { return _supplierList; }
            set { _supplierList = value; }
        }

        [DataMember]
        public List<Item> ItemList
        {
            get { return _itemList; }
            set { _itemList = value; }
        }

        [DataMember]
        public string ItemCode
        {
            get { return _itemCode; }
            set { _itemCode = value; }
        }

        [DataMember]
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        [DataMember]
        public string PurchaseOrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }

        [DataMember]
        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }
        }

        [DataMember]
        public string SupplierNo
        {
            get { return _supplierNo; }
            set { _supplierNo = value; }
        }


        [DataMember]
        public string SupplierName
        {
            get { return _supplierName; }
            set { _supplierName = value; }
        }

        [DataMember]
        public string SupplierAddress
        {
            get { return _supplierAddress; }
            set { _supplierAddress = value; }
        }


        [DataMember]
        public string ItemDescription
        {
            get { return _itemDescription; }
            set { _itemDescription = value; }
        }

        [DataMember]
        public Decimal ItemRate
        {
            get { return _itemRate; }
            set { _itemRate = value; }
        }
    }
}
