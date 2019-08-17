using PurchaseOrderService.DatabaseLayer;
using PurchaseOrderService.DataContract;
using System;
using System.Collections.Generic;
using System.Data;

namespace PurchaseOrderService.BusinessLayer
{
    public class PurchaseOrderBusinessLayer
    {
        PurchaseOrderDatabaseLayer poDatabaseLayer = new PurchaseOrderDatabaseLayer();


        public List<Supplier> GetSupplierDetails(string supplierNo)
        {
            var suppliersList = new List<Supplier>();
            var suppliers = new Supplier();

            var ds = poDatabaseLayer.GetSuppliersDetails(supplierNo);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    suppliersList.Add(new Supplier
                    {
                        No = (string)dr["SUPLNO"],
                        Name = (string)dr["SUPLNAME"],
                        Address = (string)dr["SUPLADDR"]
                    });
                }
            }

            return suppliersList;
        }
        public string AddSupplierDetails(Supplier supplier)
        {
            return poDatabaseLayer.AddSupplierDetails(supplier);
        }
        public string UpdateSupplierDetails(Supplier supplier)
        {
            return poDatabaseLayer.UpdateSupplierRecord(supplier);
        }
        public string DeleteSupplierDetails(Supplier supplier)
        {
            return poDatabaseLayer.DeleteSupplierDetails(supplier);
        }

        //--------------------

        public List<Item> GetItemDetails(string code)
        {
            var items = new List<Item>();

            var ds = poDatabaseLayer.GetItemDetails(code);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    items.Add(new Item
                    {
                        Code = (string)dr["ITCODE"],
                        Description = (string)dr["ITDESC"],
                        Rate = (decimal)dr["ITRATE"]
                    });
                }
            }

            return items;
        }
        public string AddItemDetails(Item item)
        {
            return poDatabaseLayer.AddItemRecord(item);
        }
        public string UpdateItemDetails(Item item)
        {
            return poDatabaseLayer.UpdateItemRecord(item);
        }
        public string DeleteItemDetails(Item item)
        {
            return poDatabaseLayer.DeleteItemRecord(item);
        }

        //--------------------
        public List<PurchaseOrder> GetPurchaseOrder(string orderNo)
        {
            var purchaseOrders = new List<PurchaseOrder>();

            var ds = poDatabaseLayer.GetPurchaseOrder(orderNo);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    purchaseOrders.Add(new PurchaseOrder
                    {
                        PurchaseOrderNo = (string)dr["PONO"],
                        PurchaseDate = (DateTime)dr["PODATE"],
                        SupplierName = (string)dr["SUPLNAME"],
                        SupplierAddress= (string)dr["SUPLADDR"],
                        ItemDescription = (string)dr["ITDESC"],
                        ItemRate= (decimal)dr["ITRATE"],
                        Quantity = (int)dr["QTY"]
                    });
                }
            }

            return purchaseOrders;
        }
        public string AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            return poDatabaseLayer.AddPurchaseOrder(purchaseOrder);
        }
        public string UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            return poDatabaseLayer.UpdatePurchaseOrder(purchaseOrder);
        }
        public string DeletePurchaseOrder(PurchaseOrder puchaseOrder)
        {
            return poDatabaseLayer.DeletePurchaseOrder(puchaseOrder);
        }
    }
}
