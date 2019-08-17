using PurchaseOrderService.DataContract;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Data;
using System;

namespace PurchaseOrderService.DatabaseLayer
{
    public class PurchaseOrderDatabaseLayer
    {
        string sqlConn = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        public DataSet GetSuppliersDetails(string supplierNo)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConn))
                {
                    string qry = "";

                    if (supplierNo == string.Empty)
                    {
                        qry = "SELECT * FROM SUPPLIER";
                    }
                    else
                    {
                        qry = "SELECT * FROM SUPPLIER WHERE SUPLNO = " + supplierNo;
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(qry, conn);
                    sda.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            return ds;
        }
        public string AddSupplierDetails(Supplier supplier)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConn))
                {
                    SqlCommand cmd = new SqlCommand();

                    string Query = @"INSERT INTO SUPPLIER (SUPLNO,SUPLNAME,SUPLADDR)  
                                               Values((SELECT ISNULL(MAX(SUPLNO), 0) + 1 FROM SUPPLIER), @SUPLNAME, @SUPLADDR)";

                    cmd = new SqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@SUPLNAME", supplier.Name);
                    cmd.Parameters.AddWithValue("@SUPLADDR", supplier.Address);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }
            return "Success";
        }
        public string UpdateSupplierRecord(Supplier supplier)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConn))
                {
                    SqlCommand cmd = new SqlCommand();

                    string Query = "UPDATE SUPPLIER SET SUPLNAME=@SUPLNAME, SUPLADDR=@SUPLADDR WHERE SUPLNO=@SUPLNO";

                    cmd = new SqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@SUPLNO", supplier.No);
                    cmd.Parameters.AddWithValue("@SUPLNAME", supplier.Name);
                    cmd.Parameters.AddWithValue("@SUPLADDR", supplier.Address);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }
            return "Success";
        }
        public string DeleteSupplierDetails(Supplier supplier)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConn))
                {
                    SqlCommand cmd = new SqlCommand();

                    string Query = "IF NOT EXISTS(SELECT * FROM POMASTER WHERE SUPLNO = @SUPLNO) "
                    + "BEGIN DELETE FROM SUPPLIER WHERE SUPLNO=@SUPLNO; SELECT 1 END ELSE BEGIN SELECT 0 END;";

                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@SUPLNO", supplier.No);
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            if (result == 1)
            { return "Success"; }
            else
            { return "Cannot Delete."; }
        }


        //---------------

        public DataSet GetItemDetails(string code)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConn))
                {

                    string que = "";
                    if (code != string.Empty)
                    {
                        que = "SELECT * FROM ITEM WHERE ITCODE = " + code;
                    }
                    else
                    {
                        que = "SELECT * FROM ITEM ";
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(que, conn);
                    sda.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            return ds;
        }
        public string AddItemRecord(Item item)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(sqlConn))
                {
                    SqlCommand cmd = new SqlCommand();

                    string Query = @"INSERT INTO ITEM (ITCODE,ITDESC,ITRATE)  
                                               Values((SELECT ISNULL(MAX(ITCODE), 0) + 1 FROM ITEM),@ITDESC,@ITRATE)";

                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@ITDESC", item.Description);
                    cmd.Parameters.AddWithValue("@ITRATE", item.Rate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            return "Success";

        }
        public string UpdateItemRecord(Item item)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConn))
                {
                    SqlCommand cmd = new SqlCommand();

                    string Query = "UPDATE ITEM SET ITDESC=@ITDESC, ITRATE=@ITRATE WHERE ITCODE=@ITCODE";

                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@ITCODE", item.Code);
                    cmd.Parameters.AddWithValue("@ITDESC", item.Description);
                    cmd.Parameters.AddWithValue("@ITRATE", item.Rate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            return "Success";
        }
        public string DeleteItemRecord(Item item)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConn))
                {
                    SqlCommand cmd = new SqlCommand();
                    string Query = "IF NOT EXISTS(SELECT * FROM PODETAIL WHERE ITCODE = @ITCODE) "
                      + "BEGIN DELETE FROM ITEM Where ITCODE=@ITCODE; SELECT 1 END ELSE BEGIN SELECT 0 END;";

                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@ITCODE", item.Code);
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            if (result == 1)
            { return "Success"; }
            else
            { return "Cannot Delete."; }
        }
        //--------------------


        public DataSet GetPurchaseOrder(string orderNo)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConn))
                {
                    string query = "";

                    query = @"SELECT POM.PONO, POM.PODATE, SUP.SUPLNAME, SUP.SUPLADDR, IT.ITDESC, IT.ITRATE, POD.QTY FROM POMASTER POM"
                            + " INNER JOIN PODETAIL POD ON POD.PONO = POM.PONO "
                            + " INNER JOIN SUPPLIER SUP ON SUP.SUPLNO = POM.SUPLNO "
                            + " INNER JOIN ITEM IT ON IT.ITCODE  = POD.ITCODE";

                    if (orderNo != string.Empty)
                    {
                        query += " WHERE POM.PONO = " + orderNo;
                    }
                    new SqlDataAdapter(query, con).Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            return ds;
        }
        public string AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConn))
                {
                    SqlCommand cmd = new SqlCommand();

                    string Query = "BEGIN INSERT INTO POMASTER (PONO,PODATE,SUPLNO) Values((SELECT ISNULL(MAX(PONO), 0) + 1 FROM POMASTER),@PODATE,@SUPLNO); "
                        + " INSERT INTO PODETAIL (PONO, ITCODE, QTY) Values((SELECT ISNULL(MAX(PONO), 0) FROM POMASTER), @ITCODE, @QTY) "
                        + " END";

                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@PODATE", System.DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@SUPLNO", purchaseOrder.SupplierNo.Trim());
                    cmd.Parameters.AddWithValue("@ITCODE", purchaseOrder.ItemCode.Trim());
                    cmd.Parameters.AddWithValue("@QTY", purchaseOrder.Quantity);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            return "Success";
        }
        public string DeletePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConn))
                {
                    SqlCommand cmd = new SqlCommand();
                    string Query = "BEGIN DELETE FROM PODETAIL Where PONO = @PONO; DELETE FROM POMASTER Where PONO=@PONO; END;";

                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@PONO", purchaseOrder.PurchaseOrderNo);
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            return "Success";
        }
        public string UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sqlConn))
                {
                    SqlCommand cmd = new SqlCommand();

                    string Query = "UPDATE PODETAIL SET QTY=@QTY WHERE PONO=@PONO;";

                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@PONO", purchaseOrder.PurchaseOrderNo);
                    cmd.Parameters.AddWithValue("@QTY", purchaseOrder.Quantity);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.ToString());
            }

            return "Success";
        }

        //-----------------------------

    }
}
