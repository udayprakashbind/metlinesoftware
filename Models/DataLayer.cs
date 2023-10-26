using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace StoreMgmt.Models
{
    public class DataLayer
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);

        #region manage login

        public List<usermodel> getlogin2Details(usermodel usmodel)
        {
            List<usermodel> list = new List<usermodel>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_logindata", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@roleId", usmodel.RoleId);
            cmd.Parameters.AddWithValue("@username", usmodel.username);
            cmd.Parameters.AddWithValue("@password", usmodel.password);
            cmd.Parameters.AddWithValue("@Action", "login2");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    usermodel user = new usermodel();
                    user = new usermodel();
                    user.Id = Convert.ToInt32(sdr["Id"]);
                    user.RoleId = Convert.ToInt32(sdr["RoleId"]);
                    user.RoleName = sdr["RoleName"].ToString();
                    user.username = sdr["UserName"].ToString();
                    user.password = sdr["Password"].ToString();
                    list.Add(user);

                }
            }
            con.Close();
            return list;

        }


        #endregion


        #region manage change password

        public bool changPass(ChangePassword changobj)
        {
            SqlCommand cmd = new SqlCommand("sp_logindata", con);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@password", changobj.newPaswd);
                cmd.Parameters.AddWithValue("@username", changobj.username);
                cmd.Parameters.AddWithValue("@Action", "changepassword");
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                con.Close();
                cmd.Dispose();

            }
            return true;

        }


        #endregion


        #region manage person master

        public bool AddPersonDetails(Person personObj)
        {
            SqlCommand cmd = new SqlCommand("sp_PersonDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addPerson");
            cmd.Parameters.AddWithValue("@Id", personObj.Id);
            cmd.Parameters.AddWithValue("@PersonName", personObj.PersonName);
            cmd.Parameters.AddWithValue("@Contact", personObj.Contact);
            cmd.Parameters.AddWithValue("@Address", personObj.Address);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<Person> getPersonDetails()
        {
            List<Person> list = new List<Person>();
            Person prsn;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_PersonDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getdetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    prsn = new Person();
                    prsn.Id = Convert.ToInt32(sdr["Id"]);
                    prsn.PersonName = sdr["PersonName"].ToString();
                    prsn.Contact = sdr["Contact_No"].ToString();
                    prsn.Address = sdr["Address"].ToString();
                    list.Add(prsn);
                }
            }
            con.Close();
            return list;

        }

        public List<Person> getPersonById(int Id)
        {
            List<Person> list = new List<Person>();
            Person psn;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_PersonDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getdetailsById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    psn = new Person();
                    psn.Id = Convert.ToInt32(sdr["Id"]);
                    psn.PersonName = sdr["PersonName"].ToString();
                    psn.Contact = sdr["Contact_No"].ToString();
                    psn.Address = sdr["Address"].ToString();
                    list.Add(psn);

                }
            }
            con.Close();
            return list;

        }

        public bool UpdatePrsn(Person psn)
        {
            SqlCommand cmd = new SqlCommand("sp_PersonDetails", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "updateperson");
                cmd.Parameters.AddWithValue("@Id", psn.Id);
                cmd.Parameters.AddWithValue("@PersonName", psn.PersonName);
                cmd.Parameters.AddWithValue("@Contact", psn.Contact);
                cmd.Parameters.AddWithValue("@Address", psn.Address);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }

        //Delete Person details

        public int DeletePersnData(Person prsnd)
        {
            SqlCommand cmd = new SqlCommand("sp_PersonDetails", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "DeletePerson");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", prsnd.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }



        #endregion


        #region manage vendor master

        public bool AddVendorDetails(Vendor VendorObj)
        {
            SqlCommand cmd = new SqlCommand("sp_VenderDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addVender");
            cmd.Parameters.AddWithValue("@Id", VendorObj.Id);
            cmd.Parameters.AddWithValue("@VenderName", VendorObj.VendorName);
            cmd.Parameters.AddWithValue("@VenderContact", VendorObj.VendorContact);
            cmd.Parameters.AddWithValue("@VenderAddress", VendorObj.VendorAddress);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<Vendor> getVendorDetails()
        {
            List<Vendor> list = new List<Vendor>();
            Vendor vedr;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_VenderDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getVenderDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    vedr = new Vendor();
                    vedr.Id = Convert.ToInt32(sdr["Id"]);
                    vedr.VendorName = sdr["VenderName"].ToString();
                    vedr.VendorContact = sdr["Contact_No"].ToString();
                    vedr.VendorAddress = sdr["Address"].ToString();

                    list.Add(vedr);

                }
            }
            con.Close();
            return list;

        }



        public List<Vendor> getVendorById(int Id)
        {
            List<Vendor> list = new List<Vendor>();
            Vendor vndr;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_VenderDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getVenderById");
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    vndr = new Vendor();
                    vndr.Id = Convert.ToInt32(sdr["Id"]);
                    vndr.VendorName = sdr["VenderName"].ToString();
                    vndr.VendorContact = sdr["Contact_No"].ToString();
                    vndr.VendorAddress = sdr["Address"].ToString();
                    list.Add(vndr);

                }
            }
            con.Close();
            return list;

        }

        public bool UpdateVendor(Vendor vndr)
        {
            SqlCommand cmd = new SqlCommand("sp_VenderDetails", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "updateVender");
                cmd.Parameters.AddWithValue("@Id", vndr.Id);
                cmd.Parameters.AddWithValue("@VenderName", vndr.VendorName);
                cmd.Parameters.AddWithValue("@VenderContact", vndr.VendorContact);
                cmd.Parameters.AddWithValue("@VenderAddress", vndr.VendorAddress);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }



        //Delete Vendor details

        public int DeleteVendorData(Vendor venObj)
        {
            SqlCommand cmd = new SqlCommand("sp_VenderDetails", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "DeleteVender");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", venObj.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }



        #endregion

        #region manage Item Master

        public bool AddItemDetails(ItemsModel itemObj)
        {
            SqlCommand cmd = new SqlCommand("sp_ItemDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addItem");
            cmd.Parameters.AddWithValue("@Id", itemObj.Id);
            cmd.Parameters.AddWithValue("@ItemName", itemObj.ItemName);
            cmd.Parameters.AddWithValue("@ItemCode", itemObj.ItemCode);
            cmd.Parameters.AddWithValue("@ItemDetail", itemObj.ItemDetails);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<ItemsModel> getItemDetails()
        {
            List<ItemsModel> list = new List<ItemsModel>();
            ItemsModel Items;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ItemDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getItemDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Items = new ItemsModel();
                    Items.Id = Convert.ToInt32(sdr["Id"]);
                    Items.ItemName = sdr["ItemName"].ToString();
                    Items.ItemCode = sdr["ItemCode"].ToString();
                    Items.ItemDetails = sdr["ItemDetail"].ToString();

                    list.Add(Items);

                }
            }
            con.Close();
            return list;

        }


        public List<ItemsModel> getItemById(int Id)
        {
            List<ItemsModel> list = new List<ItemsModel>();
            ItemsModel itms;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ItemDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getItemById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    itms = new ItemsModel();
                    itms.Id = Convert.ToInt32(sdr["Id"]);
                    itms.ItemName = sdr["ItemName"].ToString();
                    itms.ItemCode = sdr["ItemCode"].ToString();
                    itms.ItemDetails = sdr["ItemDetail"].ToString();
                    list.Add(itms);

                }
            }
            con.Close();
            return list;

        }

        public bool UpdateItem(ItemsModel itms)
        {
            SqlCommand cmd = new SqlCommand("sp_ItemDetails", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "updateItem");
                cmd.Parameters.AddWithValue("@Id", itms.Id);
                cmd.Parameters.AddWithValue("@ItemName", itms.ItemName);
                cmd.Parameters.AddWithValue("@ItemCode", itms.ItemCode);
                cmd.Parameters.AddWithValue("@ItemDetail", itms.ItemDetails);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }

        //Delete Item details

        public int DeleteItemData(ItemsModel prsnd)
        {
            SqlCommand cmd = new SqlCommand("sp_ItemDetails", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "DeleteItem");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", prsnd.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }


        #endregion


        #region manage Department
        public bool AddDeptDetails(Department deptObj)
        {
            SqlCommand cmd = new SqlCommand("sp_Department", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addDepartment");
            cmd.Parameters.AddWithValue("@Id", deptObj.Id);
            cmd.Parameters.AddWithValue("@Department", deptObj.DepartmentName);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<Department> getDepartmentDetails()
        {
            List<Department> list = new List<Department>();
            Department dept;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Department", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getDepartment");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dept = new Department();
                    dept.Id = Convert.ToInt32(sdr["Id"]);
                    dept.DepartmentName = sdr["Department"].ToString();
                    list.Add(dept);

                }
            }
            con.Close();
            return list;

        }


        public List<Department> getDeptById(int Id)
        {
            List<Department> list = new List<Department>();
            Department dept;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Department", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getDepartmentById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dept = new Department();
                    dept.Id = Convert.ToInt32(sdr["Id"]);
                    dept.DepartmentName = sdr["Department"].ToString();

                    list.Add(dept);

                }
            }
            con.Close();
            return list;

        }

        public bool UpdateDept(Department depts)
        {
            SqlCommand cmd = new SqlCommand("sp_Department", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "updateDepartment");
                cmd.Parameters.AddWithValue("@Id", depts.Id);
                cmd.Parameters.AddWithValue("@Department", depts.DepartmentName);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }

        //Delete Department details

        public int DeleteDeptData(Department dept)
        {
            SqlCommand cmd = new SqlCommand("sp_Department", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "DeleteDepartment");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", dept.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }


        #endregion


        #region manage stock inward

        public bool AddStockInwardDetails(StockInwardAdd stObj)
        {
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addStockInward");
            cmd.Parameters.AddWithValue("@Id", stObj.Id);
            cmd.Parameters.AddWithValue("@ddlVender", stObj.ddlVendor);
            cmd.Parameters.AddWithValue("@ddlItem", stObj.ddlItem);
            cmd.Parameters.AddWithValue("@ddlPerson", stObj.ddlPerson);
            cmd.Parameters.AddWithValue("@Quantity", stObj.Quantity);
            cmd.Parameters.AddWithValue("@ItemWeight", stObj.ItemWeight);
            cmd.Parameters.AddWithValue("@Rate", stObj.Rate);
            cmd.Parameters.AddWithValue("@Remark", stObj.Remark);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<StockInward> getStockInwardDetails()
        {
            List<StockInward> list = new List<StockInward>();
            StockInward stinwd;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "selectStockInward");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    stinwd = new StockInward();
                    stinwd.Id = Convert.ToInt32(sdr["Id"]);
                    stinwd.Item_Id = Convert.ToInt32(sdr["Item_Id"]);
                    stinwd.ddlVendor = sdr["VenderName"].ToString();
                    stinwd.ddlItem = sdr["ItemName"].ToString();
                    stinwd.ddlPerson = sdr["PersonName"].ToString();
                    stinwd.Quantity = sdr["Quantity"].ToString();
                    stinwd.ItemWeight = sdr["ItemWeight"].ToString();
                    stinwd.Rate = sdr["Rate"].ToString();
                    stinwd.Remark = sdr["Remark"].ToString();
                    stinwd.Date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();
                    list.Add(stinwd);

                }
            }
            con.Close();
            return list;

        }


        //stock code
        public List<StockInward> getItemTotalItem()
        {
            List<StockInward> list = new List<StockInward>();
            StockInward stinwd;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getsockItemtotalByItem");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    stinwd = new StockInward();
                    //        stinwd.Id = Convert.ToInt32(sdr["Id"]);
                    stinwd.Item_Id = Convert.ToInt32(sdr["Item_Id"]);
                    stinwd.ddlItem = sdr["ItemName"].ToString();
                    stinwd.Quantity = sdr["AvailableItemQuantity"].ToString();
                    stinwd.ItemWeight = sdr["AvailableItemWeight"].ToString();
                    list.Add(stinwd);

                }
            }
            con.Close();
            return list;

        }



        public List<StockInward> getstockInwardfilterBy(string start, string end)
        {
            List<StockInward> list = new List<StockInward>();
            StockInward stinwd;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getstockInwarfilterBy");
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    stinwd = new StockInward();
                    stinwd.Id = Convert.ToInt32(sdr["Id"]);
                    stinwd.ddlVendor = sdr["VenderName"].ToString();
                    stinwd.ddlItem = sdr["ItemName"].ToString();
                    stinwd.ddlPerson = sdr["PersonName"].ToString();
                    stinwd.Quantity = sdr["Quantity"].ToString();
                    stinwd.ItemWeight = sdr["ItemWeight"].ToString();
                    stinwd.Rate = sdr["Rate"].ToString();
                    stinwd.Remark = sdr["Remark"].ToString();
                    stinwd.Date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();
                    list.Add(stinwd);

                }
            }
            con.Close();
            return list;

        }



        public List<StockInward> getStockInwardById(int Id)
        {
            List<StockInward> list = new List<StockInward>();
            StockInward stinwd;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getStockInwardById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    stinwd = new StockInward();
                    stinwd.Id = Convert.ToInt32(sdr["Id"]);
                    stinwd.Vend_Id = Convert.ToInt32(sdr["Vend_Id"]);
                    stinwd.Item_Id = Convert.ToInt32(sdr["Item_Id"]);
                    stinwd.Person_Id = Convert.ToInt32(sdr["Person_Id"]);
                    stinwd.ddlVendor = sdr["VenderName"].ToString();
                    stinwd.ddlItem = sdr["ItemName"].ToString();
                    stinwd.ddlPerson = sdr["PersonName"].ToString();
                    stinwd.Quantity = sdr["Quantity"].ToString();
                    stinwd.ItemWeight = sdr["ItemWeight"].ToString();
                    stinwd.Rate = sdr["Rate"].ToString();
                    stinwd.Remark = sdr["Remark"].ToString();
                    list.Add(stinwd);

                }
            }
            con.Close();
            return list;

        }
        public List<StockInward> getItemStockDetailsByItemId(int Id)
        {
            List<StockInward> list = new List<StockInward>();
            StockInward stinwd;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getItemStockDetailsByItemId");
            cmd.Parameters.AddWithValue("@ddlItem", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    stinwd = new StockInward();
                    stinwd.Id = Convert.ToInt32(sdr["Id"]);
                    stinwd.Vend_Id = Convert.ToInt32(sdr["Vend_Id"]);
                    stinwd.Item_Id = Convert.ToInt32(sdr["Item_Id"]);
                    stinwd.Person_Id = Convert.ToInt32(sdr["Person_Id"]);
                    stinwd.ddlVendor = sdr["VenderName"].ToString();
                    stinwd.ddlItem = sdr["ItemName"].ToString();
                    stinwd.ddlPerson = sdr["PersonName"].ToString();
                    stinwd.Quantity = sdr["Quantity"].ToString();
                    stinwd.ItemWeight = sdr["ItemWeight"].ToString();
                    stinwd.Rate = sdr["Rate"].ToString();
                    stinwd.Remark = sdr["Remark"].ToString();
                    stinwd.Date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();
                    list.Add(stinwd);

                }
            }
            con.Close();
            return list;

        }

        public List<StockInward> getTotalStockQuantityByItemId(int Id)
        {
            List<StockInward> list = new List<StockInward>();
            StockInward stinwd;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getTotalStockQuantityByItemId");
            cmd.Parameters.AddWithValue("@ddlItem", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    stinwd = new StockInward();
                    stinwd.Quantity = sdr["Quantity"].ToString();
                    stinwd.ItemWeight = sdr["ItemWeight"].ToString();
                    list.Add(stinwd);

                }
            }
            con.Close();
            return list;

        }

        //not use

        public List<StockInward> getddlStockInward(int Id)
        {
            List<StockInward> list = new List<StockInward>();
            StockInward stinwd;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getStockInwardById");
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    stinwd = new StockInward();
                    stinwd.Id = Convert.ToInt32(sdr["Id"]);
                    stinwd.ddlVendor = sdr["Vend_Id"].ToString();
                    stinwd.ddlItem = sdr["Item_Id"].ToString();
                    stinwd.ddlPerson = sdr["Person_Id"].ToString();
                    stinwd.Quantity = sdr["Quantity"].ToString();
                    stinwd.ItemWeight = sdr["ItemWeight"].ToString();
                    stinwd.Rate = sdr["Rate"].ToString();
                    stinwd.Remark = sdr["Remark"].ToString();
                    list.Add(stinwd);

                }
            }
            con.Close();
            return list;

        }


        public bool UpdateStockInward(StockInward stObj)
        {
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateStockInward");
                cmd.Parameters.AddWithValue("@Id", stObj.Id);
                cmd.Parameters.AddWithValue("@ddlVender", stObj.ddlVendor);
                cmd.Parameters.AddWithValue("@ddlItem", stObj.ddlItem);
                cmd.Parameters.AddWithValue("@ddlPerson", stObj.ddlPerson);
                cmd.Parameters.AddWithValue("@Quantity", stObj.Quantity);
                cmd.Parameters.AddWithValue("@ItemWeight", stObj.ItemWeight);
                cmd.Parameters.AddWithValue("@Rate", stObj.Rate);
                cmd.Parameters.AddWithValue("@Remark", stObj.Remark);

                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }

        // not use
        public bool UpdateStockInwardByItem(StockInward stObj)
        {
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateStockInwardByItem");
                cmd.Parameters.AddWithValue("@Id", stObj.Id);
                cmd.Parameters.AddWithValue("@ddlVender", stObj.ddlVendor);
                cmd.Parameters.AddWithValue("@ddlItem", stObj.ddlItem);
                cmd.Parameters.AddWithValue("@ddlPerson", stObj.ddlPerson);
                cmd.Parameters.AddWithValue("@Quantity", stObj.Quantity);
                cmd.Parameters.AddWithValue("@ItemWeight", stObj.ItemWeight);
                cmd.Parameters.AddWithValue("@Rate", stObj.Rate);
                cmd.Parameters.AddWithValue("@Remark", stObj.Remark);

                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }



        //Delete Stock details

        public int DeleteStockInwardData(StockInward stObj)
        {
            SqlCommand cmd = new SqlCommand("sp_StockInward", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "deleteStockInward");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", stObj.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }



        #endregion


        #region manage Issue

        public bool AddIssueDetails(Issue issueObj)
        {
            SqlCommand cmd = new SqlCommand("sp_Issue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addIssue");
            cmd.Parameters.AddWithValue("@Id", issueObj.Id);
            cmd.Parameters.AddWithValue("@ddlDept", issueObj.ddlDept);
            cmd.Parameters.AddWithValue("@ddlItem", issueObj.ddlItem);
            cmd.Parameters.AddWithValue("@ddlPerson", issueObj.ddlPerson);
            cmd.Parameters.AddWithValue("@useFor", issueObj.useFor);
            cmd.Parameters.AddWithValue("@quantity", issueObj.quantity);
            cmd.Parameters.AddWithValue("@ItemWeight", issueObj.ItemWeight);
            cmd.Parameters.AddWithValue("@serialNo", issueObj.serialNo);

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<Issue> getIssueDetails()
        {
            List<Issue> list = new List<Issue>();
            Issue isuObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Issue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "selectIssueDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    isuObj = new Issue();
                    isuObj.Id = Convert.ToInt32(sdr["Id"]);
                    isuObj.ddlDept = sdr["Department"].ToString();
                    isuObj.ddlItem = sdr["ItemName"].ToString();
                    isuObj.ddlPerson = sdr["PersonName"].ToString();
                    isuObj.useFor = sdr["UseFor"].ToString();
                    isuObj.quantity = sdr["Quantity"].ToString();
                    isuObj.ItemWeight = sdr["ItemWeight"].ToString();
                    isuObj.serialNo = sdr["Serial_No"].ToString();
                    isuObj.date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();

                    list.Add(isuObj);

                }
            }
            con.Close();
            return list;

        }


        public List<Issue> getIssueDataById(int Id)
        {
            List<Issue> list = new List<Issue>();
            Issue issueObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Issue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getIssueById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    issueObj = new Issue();
                    issueObj.Id = Convert.ToInt32(sdr["Id"]);
                    issueObj.ddlDept = sdr["Department"].ToString();
                    issueObj.ddlItem = sdr["ItemName"].ToString();
                    issueObj.ddlPerson = sdr["PersonName"].ToString();
                    issueObj.useFor = sdr["UseFor"].ToString();
                    issueObj.quantity = sdr["Quantity"].ToString();
                    issueObj.ItemWeight = sdr["ItemWeight"].ToString();
                    issueObj.serialNo = sdr["Serial_No"].ToString();
                    //  issueObj.date = sdr["Date"].ToString();
                    list.Add(issueObj);

                }
            }
            con.Close();
            return list;

        }


        public List<Issue> GetTotalIssueQuantityByItemId(int Id)
        {
            List<Issue> list = new List<Issue>();
            Issue issueObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Issue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "GetTotalIssueQuantityByItemId");
            cmd.Parameters.AddWithValue("@ddlItem", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    issueObj = new Issue();
                    issueObj.quantity = sdr["Quantity"].ToString();
                    issueObj.ItemWeight = sdr["ItemWeight"].ToString();
                    list.Add(issueObj);

                }
            }
            con.Close();
            return list;

        }

        public List<Issue> GetTotalIssueItem()
        {
            List<Issue> list = new List<Issue>();
            Issue its;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_issue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getTotalIssueItem");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    its = new Issue();
                    its.Id = Convert.ToInt32(sdr["Id"]);
                    its.Item_Id = Convert.ToInt32(sdr["Item_Id"]);
                    its.ddlItem = sdr["ItemName"].ToString();
                    its.quantity = sdr["Quantity"].ToString();
                    its.ItemWeight = sdr["ItemWeight"].ToString();
                    list.Add(its);

                }
            }
            con.Close();
            return list;

        }

        public List<Issue> getIssueDataByItemId(int Id)
        {
            List<Issue> list = new List<Issue>();
            Issue issueObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Issue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getTotalIssueItemByItemId");
            cmd.Parameters.AddWithValue("@ddlItem", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    issueObj = new Issue();
                    issueObj.Id = Convert.ToInt32(sdr["Id"]);
                    issueObj.ddlDept = sdr["Department"].ToString();
                    issueObj.ddlItem = sdr["ItemName"].ToString();
                    issueObj.ddlPerson = sdr["PersonName"].ToString();
                    issueObj.useFor = sdr["UseFor"].ToString();
                    issueObj.quantity = sdr["Quantity"].ToString();
                    issueObj.ItemWeight = sdr["ItemWeight"].ToString();
                    issueObj.serialNo = sdr["Serial_No"].ToString();
                    issueObj.date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();
                    list.Add(issueObj);

                }
            }
            con.Close();
            return list;

        }



        public List<Issue> GetddlForIssue(int Id)
        {
            List<Issue> list = new List<Issue>();
            Issue issueObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Issue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getIssueById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    issueObj = new Issue();
                    issueObj.Id = Convert.ToInt32(sdr["Id"]);
                    issueObj.ddlDept = sdr["Dept_Id"].ToString();
                    issueObj.ddlItem = sdr["Item_Id"].ToString();
                    issueObj.ddlPerson = sdr["Person_Id"].ToString();
                    issueObj.useFor = sdr["UseFor"].ToString();
                    issueObj.quantity = sdr["Quantity"].ToString();
                    issueObj.ItemWeight = sdr["ItemWeight"].ToString();
                    issueObj.serialNo = sdr["Serial_No"].ToString();
                    list.Add(issueObj);

                }
            }
            con.Close();
            return list;

        }



        public bool UpdateIssue(Issue stObj)
        {
            SqlCommand cmd = new SqlCommand("sp_Issue", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateIssue");
                cmd.Parameters.AddWithValue("@Id", stObj.Id);
                cmd.Parameters.AddWithValue("@ddlDept", stObj.ddlDept);
                cmd.Parameters.AddWithValue("@ddlItem", stObj.ddlItem);
                cmd.Parameters.AddWithValue("@ddlPerson", stObj.ddlPerson);
                cmd.Parameters.AddWithValue("@useFor", stObj.useFor);
                cmd.Parameters.AddWithValue("@quantity", stObj.quantity);
                cmd.Parameters.AddWithValue("@ItemWeight", stObj.ItemWeight);
                cmd.Parameters.AddWithValue("@serialNo", stObj.serialNo);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }

        //Delete Issue  details

        public int DeleteIssueData(Issue stObj)
        {
            SqlCommand cmd = new SqlCommand("sp_Issue", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "deleteIssue");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", stObj.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }



        #endregion

        #region manage coil

        public bool AddCoilDetails(Coil_model coilObj)
        {
            SqlCommand cmd = new SqlCommand("sp_coil", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addCoil");
            cmd.Parameters.AddWithValue("@Id", coilObj.Id);
            cmd.Parameters.AddWithValue("@Code", coilObj.Code);
            cmd.Parameters.AddWithValue("@CoilName", coilObj.CoilName);
            cmd.Parameters.AddWithValue("@Thickness", coilObj.Thickness);
            cmd.Parameters.AddWithValue("@Width", coilObj.Width);
            cmd.Parameters.AddWithValue("@Weight", coilObj.Weight);
            cmd.Parameters.AddWithValue("@AddFrom", coilObj.AddFrom);
            cmd.Parameters.AddWithValue("@Remark", coilObj.remark);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<Coil_model> getCoilDetails()
        {
            List<Coil_model> list = new List<Coil_model>();
            Coil_model Coils;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_coil", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCoilDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Coils = new Coil_model();
                    Coils.Id = Convert.ToInt32(sdr["Id"]);
                    Coils.Code = sdr["Code"].ToString();
                    Coils.CoilName = sdr["CoilName"].ToString();
                    Coils.Thickness = sdr["Thickness"].ToString();
                    Coils.Width = sdr["Width"].ToString();
                    Coils.Weight = sdr["Weight"].ToString();
                    Coils.AddFrom = sdr["AddFrom"].ToString();
                    Coils.Date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();
                    Coils.remark = sdr["Remark"].ToString();

                    list.Add(Coils);

                }
            }
            con.Close();
            return list;

        }




        public List<Coil_model> getCoilReportList()
        {
            List<Coil_model> list = new List<Coil_model>();
            Coil_model Coils;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_coil", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "CoilReportList");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Coils = new Coil_model();
                    Coils.Id = Convert.ToInt32(sdr["Id"]);
                    Coils.Code = sdr["Code"].ToString();
                    Coils.remark = sdr["Remark"].ToString();
                    Coils.CoilName = sdr["CoilName"].ToString();
                    Coils.AvailableWeight = sdr["AvailableWeight"].ToString();
                    Coils.InitialWeight = sdr["InitialWeight"].ToString();
                    list.Add(Coils);

                }
            }
            con.Close();
            return list;

        }



        public List<Coil_model> getCoilById(int Id)
        {
            List<Coil_model> list = new List<Coil_model>();
            Coil_model coils;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_coil", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCoilById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    coils = new Coil_model();
                    coils.Id = Convert.ToInt32(sdr["Id"]);
                    coils.Code = sdr["Code"].ToString();
                    coils.CoilName = sdr["CoilName"].ToString();
                    coils.Thickness = sdr["Thickness"].ToString();
                    coils.Width = sdr["Width"].ToString();
                    coils.Weight = sdr["Weight"].ToString();
                    coils.remark = sdr["Remark"].ToString();
                    list.Add(coils);

                }
            }
            con.Close();
            return list;

        }



        public List<Coil_model> getCoilByCode(string Cd)
        {
            List<Coil_model> list = new List<Coil_model>();
            Coil_model coils;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_coil", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCoilByCode");
            cmd.Parameters.AddWithValue("@Code", Cd);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    coils = new Coil_model();
                    coils.Id = Convert.ToInt32(sdr["Id"]);
                    coils.Code = sdr["Code"].ToString();
                    coils.CoilName = sdr["CoilName"].ToString();
                    coils.Thickness = sdr["Thickness"].ToString();
                    coils.Width = sdr["Width"].ToString();
                    coils.Weight = sdr["Weight"].ToString();
                    coils.remark = sdr["Remark"].ToString();
                    list.Add(coils);

                }
            }
            con.Close();
            return list;

        }

        public bool UpdateCoil(Coil_model coils)
        {
            SqlCommand cmd = new SqlCommand("sp_coil", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "updateCoil");
                cmd.Parameters.AddWithValue("@Id", coils.Id);
                cmd.Parameters.AddWithValue("@Code", coils.Code);
                cmd.Parameters.AddWithValue("@CoilName", coils.CoilName);
                cmd.Parameters.AddWithValue("@Thickness", coils.Thickness);
                cmd.Parameters.AddWithValue("@Width", coils.Width);
                cmd.Parameters.AddWithValue("@Weight", coils.Weight);

                cmd.Parameters.AddWithValue("@Remark", coils.remark);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }


        public bool UpdateCoilWeight(coilWeight obj)
        {
            SqlCommand cmd = new SqlCommand("sp_coil", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "updateCoilWeight");
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@Weight", obj.newCoilWeight);
                cmd.Parameters.AddWithValue("@Width", obj.newCoilWidth);

                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }




        //Delete coil details

        public int DeleteCoilData(Coil_model delCoil)
        {
            SqlCommand cmd = new SqlCommand("sp_coil", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "DeleteCoil");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", delCoil.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }


        public List<jobdetail> GetTotalCoilList()
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "TotalCoilList");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.Id = Convert.ToInt32(sdr["Id"]);
                    jobs.Code = sdr["Code"].ToString();
                    jobs.JobType = sdr["Type"].ToString();
                    jobs.CuttingWeight = sdr["Weight"].ToString();
                    jobs.JobWidth = sdr["Width"].ToString();
                    jobs.Thickness = sdr["Thickness"].ToString();
                    jobs.remark = sdr["Remark"].ToString();
                    jobs.AddFrom = sdr["AddFrom"].ToString();
                    list.Add(jobs);

                }
            }
            con.Close();
            return list;

        }



        #endregion

        #region manage Circle


        public bool AddCirclDetails(Circle_model circleObj)
        {
            SqlCommand cmd = new SqlCommand("sp_circle", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addCircle");
            cmd.Parameters.AddWithValue("@Id", circleObj.Id);
            cmd.Parameters.AddWithValue("@Code", circleObj.Code);
            cmd.Parameters.AddWithValue("@CircleName", circleObj.CircleName);
            cmd.Parameters.AddWithValue("@Thickness", circleObj.Thickness);
            cmd.Parameters.AddWithValue("@Width", circleObj.Width);
            cmd.Parameters.AddWithValue("@Weight", circleObj.Weight);
            cmd.Parameters.AddWithValue("@Remark", circleObj.remark);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<Circle_model> getCircleDetails()
        {
            List<Circle_model> list = new List<Circle_model>();
            Circle_model circle;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_circle", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCircleDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    circle = new Circle_model();
                    circle.Id = Convert.ToInt32(sdr["Id"]);
                    circle.Code = sdr["Code"].ToString();
                    circle.CircleName = sdr["CircleName"].ToString();
                    circle.Thickness = sdr["Thickness"].ToString();
                    circle.Width = sdr["Width"].ToString();
                    circle.Weight = sdr["Weight"].ToString();
                    circle.AddFrom = sdr["AddFrom"].ToString();
                    circle.remark = sdr["Remark"].ToString();
                    circle.Date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();


                    list.Add(circle);

                }
            }
            con.Close();
            return list;

        }
        public List<Circle_model> getCircleById(int Id)
        {
            List<Circle_model> list = new List<Circle_model>();
            Circle_model circle;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_circle", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCircleById");
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    circle = new Circle_model();
                    circle.Id = Convert.ToInt32(sdr["Id"]);
                    circle.Code = sdr["Code"].ToString();
                    circle.CircleName = sdr["CircleName"].ToString();
                    circle.Thickness = sdr["Thickness"].ToString();
                    circle.Width = sdr["Width"].ToString();
                    circle.Weight = sdr["Weight"].ToString();
                    circle.AddFrom = sdr["AddFrom"].ToString();
                    circle.remark = sdr["Remark"].ToString();
                    list.Add(circle);

                }
            }
            con.Close();
            return list;

        }

        public bool UpdateCircle(Circle_model coils)
        {
            SqlCommand cmd = new SqlCommand("sp_circle", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "updateCircle");
                cmd.Parameters.AddWithValue("@Id", coils.Id);
                cmd.Parameters.AddWithValue("@Code", coils.Code);
                cmd.Parameters.AddWithValue("@CircleName", coils.CircleName);
                cmd.Parameters.AddWithValue("@Thickness", coils.Thickness);
                cmd.Parameters.AddWithValue("@Width", coils.Width);
                cmd.Parameters.AddWithValue("@Weight", coils.Weight);
                cmd.Parameters.AddWithValue("@Remark", coils.remark);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }

        //Delete coil details

        public int DeleteCircleData(Circle_model delCoil)
        {
            SqlCommand cmd = new SqlCommand("sp_circle", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "DeleteCircle");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", delCoil.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }


        public List<jobdetail> GetTotalCircleList()
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "TotalCircleList");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.Id = Convert.ToInt32(sdr["Id"]);
                    jobs.Code = sdr["Code"].ToString();
                    jobs.JobType = sdr["Type"].ToString();
                    jobs.CuttingWeight = sdr["Weight"].ToString();
                    jobs.Diameter = sdr["Size"].ToString();
                    jobs.Thickness = sdr["Thickness"].ToString();
                    jobs.AddFrom = sdr["AddFrom"].ToString();
                    jobs.remark = sdr["Remark"].ToString();
                    list.Add(jobs);

                }
            }
            con.Close();
            return list;

        }


        #endregion


        #region manage blank




        public bool AddBlankDetails(Blank_model blankObj)
        {
            SqlCommand cmd = new SqlCommand("sp_blank", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addblank");
            cmd.Parameters.AddWithValue("@Id", blankObj.Id);
            cmd.Parameters.AddWithValue("@Code", blankObj.Code);
            cmd.Parameters.AddWithValue("@BlankName", blankObj.BlankName);
            cmd.Parameters.AddWithValue("@Thickness", blankObj.Thickness);
            cmd.Parameters.AddWithValue("@Width", blankObj.Width);
            cmd.Parameters.AddWithValue("@Weight", blankObj.Weight);
            cmd.Parameters.AddWithValue("@Remark", blankObj.remark);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<Blank_model> GetBlankDetails()
        {
            List<Blank_model> list = new List<Blank_model>();
            Blank_model blank;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_blank", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getBlankDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    blank = new Blank_model();
                    blank.Id = Convert.ToInt32(sdr["Id"]);
                    blank.Code = sdr["Code"].ToString();
                    blank.BlankName = sdr["BlankName"].ToString();
                    blank.Thickness = sdr["Thickness"].ToString();
                    blank.Width = sdr["Width"].ToString();
                    blank.Weight = sdr["Weight"].ToString();
                    blank.AddFrom = sdr["AddFrom"].ToString();
                    blank.remark = sdr["Remark"].ToString();
                    blank.Date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();


                    list.Add(blank);

                }
            }
            con.Close();
            return list;

        }


        public List<Blank_model> GetBlankById(int Id)
        {
            List<Blank_model> list = new List<Blank_model>();
            Blank_model blank;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_blank", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getBlankById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    blank = new Blank_model();
                    blank.Id = Convert.ToInt32(sdr["Id"]);
                    blank.Code = sdr["Code"].ToString();
                    blank.BlankName = sdr["BlankName"].ToString();
                    blank.Thickness = sdr["Thickness"].ToString();
                    blank.Width = sdr["Width"].ToString();
                    blank.Weight = sdr["Weight"].ToString();
                    blank.AddFrom = sdr["AddFrom"].ToString();
                    blank.remark = sdr["Remark"].ToString();
                    list.Add(blank);

                }
            }
            con.Close();
            return list;

        }

        public bool UpdateBlank(Blank_model blanks)
        {
            SqlCommand cmd = new SqlCommand("sp_blank", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "updateBlank");
                cmd.Parameters.AddWithValue("@Id", blanks.Id);
                cmd.Parameters.AddWithValue("@Code", blanks.Code);
                cmd.Parameters.AddWithValue("@BlankName", blanks.BlankName);
                cmd.Parameters.AddWithValue("@Thickness", blanks.Thickness);
                cmd.Parameters.AddWithValue("@Width", blanks.Width);
                cmd.Parameters.AddWithValue("@Weight", blanks.Weight);
                cmd.Parameters.AddWithValue("@Remark", blanks.remark);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }

        //Delete coil details

        public int DeleteBlankData(Blank_model delCoil)
        {
            SqlCommand cmd = new SqlCommand("sp_blank", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "DeleteBlank");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", delCoil.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }


        public List<jobdetail> GetTotalBlankList()
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "TotalBlankList");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.Id = Convert.ToInt32(sdr["Id"]);
                    jobs.Code = sdr["Code"].ToString();
                    jobs.JobType = sdr["Type"].ToString();
                    jobs.CuttingWeight = sdr["Weight"].ToString();
                    jobs.Diameter = sdr["Size"].ToString();
                    jobs.Thickness = sdr["Thickness"].ToString();
                    jobs.AddFrom = sdr["AddFrom"].ToString();
                    jobs.remark = sdr["Remark"].ToString();
                    list.Add(jobs);

                }
            }
            con.Close();
            return list;

        }


        #endregion


        #region manage Job

        public bool AddJobDetails(Job jobObj)
        {
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addJob");
            cmd.Parameters.AddWithValue("@Id", jobObj.Id);
            cmd.Parameters.AddWithValue("@rowId", jobObj.rowId);
            cmd.Parameters.AddWithValue("@Code", jobObj.Code);
            cmd.Parameters.AddWithValue("@Remark", jobObj.remark);
            cmd.Parameters.AddWithValue("@JobName", jobObj.JobName);
            cmd.Parameters.AddWithValue("@Slit", jobObj.slit);
            cmd.Parameters.AddWithValue("@Thickness", jobObj.Thickness);
            cmd.Parameters.AddWithValue("@Width", jobObj.oldWidth);
            cmd.Parameters.AddWithValue("@Diameter", jobObj.Diameter);
            cmd.Parameters.AddWithValue("@Recovery", jobObj.Recovery);
            cmd.Parameters.AddWithValue("@Weight", jobObj.ConsumeWeight);
            cmd.Parameters.AddWithValue("@CoilWeight", jobObj.oldWeight);
            cmd.Parameters.AddWithValue("@CuttingWeight", jobObj.CuttingWeight);
            cmd.Parameters.AddWithValue("@Pices", jobObj.Pices);
            cmd.Parameters.AddWithValue("@Scrap", jobObj.Scrap);
            cmd.Parameters.AddWithValue("@SlitWeight", jobObj.SlitWeight);
            cmd.Parameters.AddWithValue("@SlitWidth", jobObj.SlitWidth);
            cmd.Parameters.AddWithValue("@BalanceCoilWeight", jobObj.BalanceCoilWeight);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<jobdetail> getJobDetails()
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getJobDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.Id = Convert.ToInt32(sdr["Id"]);
                    jobs.JobType = sdr["JobName"].ToString();
                    jobs.Code = sdr["Code"].ToString();
                    jobs.remark = sdr["Remark"].ToString();
                    jobs.CoilName = sdr["CoilName"].ToString();
                    jobs.oldWeight = sdr["CoilWeight"].ToString();
                    jobs.CuttingWeight = sdr["CuttingWeight"].ToString();
                    jobs.Thickness = sdr["Thickness"].ToString();
                    jobs.ConsumeWeight = sdr["Weight"].ToString();
                    jobs.Diameter = sdr["Diameter"].ToString();
                    jobs.Pices = sdr["Pices"].ToString();
                    jobs.Scrap = sdr["Scrap"].ToString();
                    jobs.SlitWeight = sdr["SlitWeight"].ToString();
                    jobs.SlitWidth = sdr["SlitWidth"].ToString();
                    jobs.slit = sdr["Slit"].ToString();
                    jobs.Recovery = sdr["Recovery"].ToString();
                    jobs.BalanceCoilWeight = sdr["BalanceCoilWeight"].ToString();
                    jobs.Date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();

                    list.Add(jobs);

                }
            }
            con.Close();
            return list;

        }



        // working from her for job scrap



        public List<jobdetail> getJobdetailByrowId(string Id)
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getJobdetailByrowId");
            cmd.Parameters.AddWithValue("@Code", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.Id = Convert.ToInt32(sdr["Id"]);
                    jobs.JobType = sdr["JobName"].ToString();
                    jobs.Code = sdr["Code"].ToString();
                    jobs.remark = sdr["Remark"].ToString();
                    jobs.CoilName = sdr["CoilName"].ToString();
                    jobs.Thickness = sdr["Thickness"].ToString();
                    jobs.ConsumeWeight = sdr["Weight"].ToString();
                    jobs.Diameter = sdr["Diameter"].ToString();
                    jobs.Pices = sdr["Pices"].ToString();
                    jobs.Scrap = sdr["Scrap"].ToString();
                    jobs.Recovery = sdr["Recovery"].ToString();
                    list.Add(jobs);

                }
            }
            con.Close();
            return list;

        }






        public List<jobdetail> GetJobById(int Id)
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getjobbyrowId");
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.Id = Convert.ToInt32(sdr["Id"]);
                    jobs.rowId =Convert.ToInt32( sdr["rowId"]);
                    jobs.ConsumeWeight = sdr["Weight"].ToString();
                    list.Add(jobs);
                }
            }
            con.Close();
            return list;

        }

      


        public bool UpdateRevCoilWeight(updateRevesrsecoilWeight obj)
        {
            SqlCommand cmd = new SqlCommand("sp_job", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateRevCoilWeight");
                cmd.Parameters.AddWithValue("@rowId", obj.rowId);
                cmd.Parameters.AddWithValue("@ReverseCoilWeight", obj.reverseCoilWeight);
                con.Open();

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }


        public bool delet_job(int id)
        {
            


            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("sp_job",con);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "deletjob");
            cmd.Parameters.AddWithValue("@Id", id);
            int i=cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }






        }


        #endregion

        #region manage job report

        public List<jobdetail> getTotalJobScrapByGroup()
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getTotalJobScrapByGroup");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.rowId = Convert.ToInt32(sdr["RowId"]);
                    jobs.Code = sdr["Code"].ToString();
                    jobs.remark = sdr["Remark"].ToString();
                    jobs.CoilName = sdr["CoilName"].ToString();
                    jobs.Scrap = sdr["Scrap"].ToString();
                    list.Add(jobs);

                }
            }
            con.Close();
            return list;

        }




        public List<jobdetail> getTotalJobScrap()
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getTotalJobScrap");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.Scrap = sdr["Scrap"].ToString();
                    list.Add(jobs);

                }
            }
            con.Close();
            return list;

        }

        // get total job scrap by coilId

        public List<jobdetail> getTotalJobScrapByCoil(string Id)
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getTotalJobScrapByCoil");
            cmd.Parameters.AddWithValue("@Code", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.CoilName = sdr["CoilName"].ToString();
                    jobs.Code = sdr["Code"].ToString();
                    jobs.remark = sdr["Remark"].ToString();
                    jobs.Scrap = sdr["Scrap"].ToString();
                    list.Add(jobs);

                }
            }
            con.Close();
            return list;

        }



        public List<jobdetail> getJobReportList()
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getjobreportlist");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.rowId = Convert.ToInt32(sdr["RowId"]);
                    jobs.Code = sdr["Code"].ToString();
                    jobs.remark = sdr["Remark"].ToString();
                    jobs.CoilName = sdr["CoilName"].ToString();
                    list.Add(jobs);

                }
            }
            con.Close();
            return list;

        }


        public List<jobdetail> getJobReportDetailsByCode(string cd)
        {
            List<jobdetail> list = new List<jobdetail>();
            jobdetail jobs;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_job", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "jobdetailsInReportByCode");
            cmd.Parameters.AddWithValue("@Code", cd);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jobs = new jobdetail();
                    jobs.JobType = sdr["JobName"].ToString();
                    //     jobs.CoilName = sdr["CoilName"].ToString(); 
                    jobs.oldWeight = sdr["CoilWeight"].ToString();
                    jobs.Code = sdr["Code"].ToString();
                    jobs.remark = sdr["Remark"].ToString();
                    jobs.Thickness = sdr["Thickness"].ToString();
                    jobs.CuttingWeight = sdr["CuttingWeight"].ToString();
                    jobs.ConsumeWeight = sdr["Weight"].ToString();
                    jobs.Diameter = sdr["Diameter"].ToString();
                    jobs.Pices = sdr["Pices"].ToString();
                    jobs.Scrap = sdr["Scrap"].ToString();
                    jobs.SlitWeight = sdr["SlitWeight"].ToString();
                    jobs.SlitWidth = sdr["SlitWidth"].ToString();
                    jobs.slit = sdr["Slit"].ToString();
                    jobs.Recovery = sdr["Recovery"].ToString();
                    jobs.BalanceCoilWeight = sdr["BalanceCoilWeight"].ToString();
                    jobs.IssueWeightToPolish = sdr["IssueJobWeight"].ToString();
                    jobs.Date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();
                    list.Add(jobs);
                }
            }
            con.Close();
            return list;

        }


        #endregion


        #region manage Item master 2



        public bool AddItems2Details(Item_Model2 itmObj)
        {
            SqlCommand cmd = new SqlCommand("sp_items2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addItemsDetail");
            cmd.Parameters.AddWithValue("@Id", itmObj.Id);
            cmd.Parameters.AddWithValue("@Name", itmObj.Name);
            cmd.Parameters.AddWithValue("@Size", itmObj.Size);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<Item_Model2> getItems2Details()
        {
            List<Item_Model2> list = new List<Item_Model2>();
            Item_Model2 items;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_items2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getItems");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    items = new Item_Model2();
                    items.Id = Convert.ToInt32(sdr["Id"]);
                    items.Name = sdr["Name"].ToString();
                    items.Size = sdr["Size"].ToString();
                    list.Add(items);

                }
            }
            con.Close();
            return list;

        }


        public List<Item_Model2> getItems2ById(int Id)
        {
            List<Item_Model2> list = new List<Item_Model2>();
            Item_Model2 items;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_items2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getItemsById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    items = new Item_Model2();
                    items.Id = Convert.ToInt32(sdr["Id"]);
                    items.Name = sdr["Name"].ToString();
                    items.Size = sdr["Size"].ToString();

                    list.Add(items);

                }
            }
            con.Close();
            return list;

        }

        public bool UpdateItems2(Item_Model2 items)
        {
            SqlCommand cmd = new SqlCommand("sp_items2", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateItems");
                cmd.Parameters.AddWithValue("@Id", items.Id);
                cmd.Parameters.AddWithValue("@Name", items.Name);
                cmd.Parameters.AddWithValue("@Size", items.Size);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }

        //Delete Items2 details

        public int DeleteItems2Data(Item_Model2 items)
        {
            SqlCommand cmd = new SqlCommand("sp_items2", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteItems");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", items.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }


        #endregion


        #region manage Issue Polishing

        public bool IssueToPolishDetails(Polishing_Model plsObj)
        {
            SqlCommand cmd = new SqlCommand("sp_issuePolish", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "issuePolish");
            cmd.Parameters.AddWithValue("@Id", plsObj.Id);
            cmd.Parameters.AddWithValue("@JobId", plsObj.jobId);
            cmd.Parameters.AddWithValue("@ItemName", plsObj.ItemName);
            cmd.Parameters.AddWithValue("@JobType", plsObj.JobType);
            cmd.Parameters.AddWithValue("@Code", plsObj.Code);
            cmd.Parameters.AddWithValue("@Remark", plsObj.Code);
            cmd.Parameters.AddWithValue("@JobName", plsObj.JobName);
            cmd.Parameters.AddWithValue("@IssueJobWeight", plsObj.JobIssueWeight);
            cmd.Parameters.AddWithValue("@PolishWeight", plsObj.PolishWeight);
            cmd.Parameters.AddWithValue("@PolishScrap", plsObj.PolishScrap);
            cmd.Parameters.AddWithValue("@JobWeight", plsObj.JobWeight);
            cmd.Parameters.AddWithValue("@PolishPices", plsObj.PolishPices);

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public bool UpdateJobWeight(jobweightUpdate obj)
        {
            SqlCommand cmd = new SqlCommand("sp_issuePolish", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateJobWeight");
                cmd.Parameters.AddWithValue("@JobId", obj.Id);
                cmd.Parameters.AddWithValue("@JobType", obj.JobType);
                cmd.Parameters.AddWithValue("@UpdateJobWeight", obj.JobUpdateWeight);
                cmd.Parameters.AddWithValue("@UpdatePices", obj.JobUpdatePices);

                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }
        public List<Polishing_Model> getPolishDetails()
        {
            List<Polishing_Model> list = new List<Polishing_Model>();
            Polishing_Model pls;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_issuePolish", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getpolishDetatils");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pls = new Polishing_Model();
                    pls.Id = Convert.ToInt32(sdr["Id"]);
                    pls.ItemName = sdr["Name"].ToString();
                    pls.JobType = sdr["JobType"].ToString();
                    pls.Code = sdr["Code"].ToString();
                    pls.remark = sdr["Remark"].ToString();
                    pls.JobName = sdr["JobName"].ToString();
                    pls.JobIssueWeight = sdr["IssueJobWeight"].ToString();
                    pls.PolishWeight = sdr["PolishWeight"].ToString();
                    pls.remainingJobWeight = sdr["CuttingWeight"].ToString();
                    pls.JobWeight = sdr["JobWeight"].ToString();
                    pls.PolishScrap = sdr["PolishScrap"].ToString();
                    pls.PolishPices = sdr["PolishPices"].ToString();
                    pls.Date = DateTime.Parse((sdr["Date"].ToString())).ToShortDateString();
                    list.Add(pls);
                }
            }
            con.Close();
            return list;

        }






        // working from here for  scrap


        public List<Polishing_Model> getPolishDetailsByCoilRowId(string Id)
        {
            List<Polishing_Model> list = new List<Polishing_Model>();
            Polishing_Model pls;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_issuePolish", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getpolishDetailsByrowId");
            cmd.Parameters.AddWithValue("@Code", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pls = new Polishing_Model();
                    pls.Id = Convert.ToInt32(sdr["Id"]);
                    pls.Code = sdr["Code"].ToString();
                    pls.remark = sdr["Remark"].ToString();
                    pls.ItemName = sdr["Name"].ToString();
                    pls.JobType = sdr["JobType"].ToString();
                    pls.JobName = sdr["JobName"].ToString();
                    pls.PolishWeight = sdr["PolishWeight"].ToString();
                    pls.remainingJobWeight = sdr["Weight"].ToString();
                    pls.JobWeight = sdr["JobWeight"].ToString();
                    pls.PolishScrap = sdr["PolishScrap"].ToString();
                    pls.PolishPices = sdr["PolishPices"].ToString();
                    list.Add(pls);
                }
            }
            con.Close();
            return list;

        }





        public List<Polishing_Model> getTotalPolishScrap()
        {
            List<Polishing_Model> list = new List<Polishing_Model>();
            Polishing_Model pls;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_issuePolish", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getTotalPolishScrap");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pls = new Polishing_Model();
                    pls.JobName = sdr["JobName"].ToString();
                    pls.PolishScrap = sdr["PolishScrap"].ToString();
                    list.Add(pls);
                }
            }
            con.Close();
            return list;

        }



        public List<Polishing_Model> getPoishScrapByGroup()
        {
            List<Polishing_Model> list = new List<Polishing_Model>();
            Polishing_Model pls;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_issuePolish", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getPoishScrapByGroup");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pls = new Polishing_Model();
                    pls.JobName = sdr["JobName"].ToString();
                    pls.Code = sdr["Code"].ToString();
                    pls.remark = sdr["Remark"].ToString();
                    pls.rowId = Convert.ToInt32(sdr["rowId"]);
                    pls.PolishScrap = sdr["PolishScrap"].ToString();
                    list.Add(pls);
                }
            }
            con.Close();
            return list;

        }


        public List<Polishing_Model> getTotalPolishScrapByCoilId(string Id)
        {
            List<Polishing_Model> list = new List<Polishing_Model>();
            Polishing_Model pls;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_issuePolish", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getTotalPolishScrapByCoilId");
            cmd.Parameters.AddWithValue("@Code", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pls = new Polishing_Model();
                    pls.JobName = sdr["JobName"].ToString();
                    pls.PolishScrap = sdr["PolishScrap"].ToString();
                    list.Add(pls);
                }
            }
            con.Close();
            return list;

        }




        #endregion


        #region manage selloutward



        public bool SellItems(selloutward_model itmObj)
        {
            SqlCommand cmd = new SqlCommand("sp_sellitemoutwards", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "sellitems");
            cmd.Parameters.AddWithValue("@id", itmObj.id);
            cmd.Parameters.AddWithValue("@rowid", itmObj.rowid);
            cmd.Parameters.AddWithValue("@code", itmObj.code);
            cmd.Parameters.AddWithValue("@itemtype", itmObj.itemtype);
            cmd.Parameters.AddWithValue("@weight", itmObj.weight);
            cmd.Parameters.AddWithValue("@partyname", itmObj.partyname);
            cmd.Parameters.AddWithValue("@thickness", itmObj.thickness);
            cmd.Parameters.AddWithValue("@size", itmObj.size);
            cmd.Parameters.AddWithValue("@quantity", itmObj.quantity);
            cmd.Parameters.AddWithValue("@remarks", itmObj.remarks);
            cmd.Parameters.AddWithValue("@addfrom", itmObj.addfrom);
            cmd.Parameters.AddWithValue("@oldweight", itmObj.oldweight);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }




        public List<selloutward_model> GetsellItemList()
        {
            List<selloutward_model> list = new List<selloutward_model>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_sellitemoutwards", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "getsellingdetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    selloutward_model selobj = new selloutward_model();
                    selobj.id = Convert.ToInt32(sdr["id"]);
                    selobj.rowid = Convert.ToInt32(sdr["rowid"]);
                    selobj.code = sdr["code"].ToString();
                    selobj.itemtype = sdr["itemtype"].ToString();
                    selobj.weight = sdr["weight"].ToString();
                    selobj.oldweight = sdr["oldweight"].ToString();
                    selobj.partyname = sdr["partyname"].ToString();
                    selobj.thickness = sdr["thickness"].ToString();
                    selobj.size = sdr["size"].ToString();
                    selobj.quantity = sdr["quantity"].ToString();
                    selobj.remarks = sdr["remarks"].ToString();
                    selobj.date = DateTime.Parse(sdr["date"].ToString()).ToShortDateString();
                    list.Add(selobj);
                }
            }
            con.Close();
            return list;
        }




        public bool updateOutsidecircleitems(selloutupdateweight itmObj)
        {
            SqlCommand cmd = new SqlCommand("sp_sellitemoutwards", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "updateOutsideCircleweight");
            cmd.Parameters.AddWithValue("@rowid", itmObj.rowid);
            cmd.Parameters.AddWithValue("@weight", itmObj.updateweight);
            cmd.Parameters.AddWithValue("@itemtype", itmObj.itemtype);
            cmd.Parameters.AddWithValue("@addfrom", itmObj.addfrom);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool updateInsidecircleitems(selloutupdateweight itmObj)
        {
            SqlCommand cmd = new SqlCommand("sp_sellitemoutwards", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "updateInsideCircleweight");
            cmd.Parameters.AddWithValue("@rowid", itmObj.rowid);
            cmd.Parameters.AddWithValue("@weight", itmObj.updateweight);
            cmd.Parameters.AddWithValue("@itemtype", itmObj.itemtype);
            cmd.Parameters.AddWithValue("@addfrom", itmObj.addfrom);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool updateOutsideblanksitems(selloutupdateweight itmObj)
        {
            SqlCommand cmd = new SqlCommand("sp_sellitemoutwards", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "updateOutsideBlankWeight");
            cmd.Parameters.AddWithValue("@rowid", itmObj.rowid);
            cmd.Parameters.AddWithValue("@weight", itmObj.updateweight);
            cmd.Parameters.AddWithValue("@itemtype", itmObj.itemtype);
            cmd.Parameters.AddWithValue("@addfrom", itmObj.addfrom);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool updateInsideblanksitems(selloutupdateweight itmObj)
        {
            SqlCommand cmd = new SqlCommand("sp_sellitemoutwards", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "updateInsideBlankweight");
            cmd.Parameters.AddWithValue("@rowid", itmObj.rowid);
            cmd.Parameters.AddWithValue("@weight", itmObj.updateweight);
            cmd.Parameters.AddWithValue("@itemtype", itmObj.itemtype);
            cmd.Parameters.AddWithValue("@addfrom", itmObj.addfrom);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool updatejobCoilweightitems(selloutupdateweight itmObj)
        {
            SqlCommand cmd = new SqlCommand("sp_sellitemoutwards", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "updatecoiljobweight");
            cmd.Parameters.AddWithValue("@rowid", itmObj.rowid);
            cmd.Parameters.AddWithValue("@weight", itmObj.updateweight);
            cmd.Parameters.AddWithValue("@itemtype", itmObj.itemtype);
            cmd.Parameters.AddWithValue("@addfrom", itmObj.addfrom);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        #endregion

        #region manage user master

        public bool AddUsersDetails(usermodel userObj)
        {
            SqlCommand cmd = new SqlCommand("sp_users", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "addusersDeatails");
            cmd.Parameters.AddWithValue("@Id", userObj.Id);
            cmd.Parameters.AddWithValue("@UserName", userObj.username);
            cmd.Parameters.AddWithValue("@mobile", userObj.mobile);
            cmd.Parameters.AddWithValue("@Department", userObj.department);
            cmd.Parameters.AddWithValue("@RoleId", userObj.RoleName);
            cmd.Parameters.AddWithValue("@password", userObj.password);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<usermodel> getUsersDetails()
        {
            List<usermodel> list = new List<usermodel>();
            usermodel userobj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_users", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getUserDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    userobj = new usermodel();
                    userobj.Id = Convert.ToInt32(sdr["Id"]);
                    userobj.username = sdr["UserName"].ToString();
                    userobj.mobile = sdr["mobile"].ToString();
                    userobj.department = sdr["Department"].ToString();
                    userobj.RoleName = sdr["RoleName"].ToString();
                    userobj.department = sdr["Department"].ToString();
                    list.Add(userobj);
                }
            }
            con.Close();
            return list;

        }

        public List<usermodel> getUserById(int Id)
        {
            List<usermodel> list = new List<usermodel>();
            usermodel userobj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_users", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getUserById");
            cmd.Parameters.AddWithValue("@Id", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    userobj = new usermodel();
                    userobj.Id = Convert.ToInt32(sdr["Id"]);
                    userobj.username = sdr["UserName"].ToString();
                    userobj.mobile = sdr["Mobile"].ToString();
                    userobj.department = sdr["Department"].ToString();
                    userobj.deptId = Convert.ToInt32(sdr["DeptId"]);
                    userobj.RoleName = sdr["RoleName"].ToString();
                    userobj.RoleId = Convert.ToInt32(sdr["RoleId"]);
                    list.Add(userobj);

                }
            }
            con.Close();
            return list;

        }

        public bool UpdateuserData(usermodel userobj)
        {
            SqlCommand cmd = new SqlCommand("sp_users", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateUsers");
                cmd.Parameters.AddWithValue("@Id", userobj.Id);
                cmd.Parameters.AddWithValue("@UserName", userobj.username);
                cmd.Parameters.AddWithValue("@Mobile", userobj.mobile);
                cmd.Parameters.AddWithValue("@RoleId", userobj.RoleName);
                cmd.Parameters.AddWithValue("@Department", userobj.department);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }

        //Delete Users details

        public int DeleteUserData(usermodel userobj)
        {
            SqlCommand cmd = new SqlCommand("sp_users", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteUserRecord");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", userobj.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }


        #endregion

        #region manage role and permission


        public List<Role_model> getRoleDetails()
        {
            List<Role_model> list = new List<Role_model>();
            Role_model roleObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getRoleDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    roleObj = new Role_model();
                    roleObj.Id = Convert.ToInt32(sdr["Id"]);
                    roleObj.RoleName = sdr["RoleName"].ToString();
                    list.Add(roleObj);
                }
            }
            con.Close();
            return list;

        }


        public List<Role_model> getRoleDetailsById(int Id)
        {
            List<Role_model> list = new List<Role_model>();
            Role_model roleObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getRoleDetailsById");
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    roleObj = new Role_model();
                    roleObj.Id = Convert.ToInt32(sdr["Id"]);
                    roleObj.RoleName = sdr["RoleName"].ToString();
                    list.Add(roleObj);
                }
            }
            con.Close();
            return list;

        }

        public bool AddRolePermissionDetails(RolePermission permObj)
        {
            SqlCommand cmd = new SqlCommand("sp_RolePermission", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "AddRolePerm");
            cmd.Parameters.AddWithValue("@Id", permObj.Id);
            cmd.Parameters.AddWithValue("@RoleId", permObj.Role_Id);
            cmd.Parameters.AddWithValue("@PermissionId", permObj.Perm_Id);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<RolePermission> getRolePermissionDetails()
        {
            List<RolePermission> list = new List<RolePermission>();
            RolePermission rolePermObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_RolePermission", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getRolePermDetailsList");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    rolePermObj = new RolePermission();
                    rolePermObj.Id = Convert.ToInt32(sdr["Id"]);
                    rolePermObj.PermissionName = sdr["PermissionName"].ToString();
                    rolePermObj.RoleName = sdr["RoleName"].ToString();
                    list.Add(rolePermObj);
                }
            }
            con.Close();
            return list;

        }
        public List<RolePermission> getRolePermissionDetailsById(int Id)
        {
            List<RolePermission> list = new List<RolePermission>();
            RolePermission rolePermObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_RolePermission", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getRolePermByRoleId");
            cmd.Parameters.AddWithValue("@RoleId", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    rolePermObj = new RolePermission();
                    rolePermObj.Id = Convert.ToInt32(sdr["Id"]);
                    rolePermObj.PermissionName = sdr["PermissionName"].ToString();
                    //      rolePermObj.RoleName = sdr["RoleName"].ToString();
                    list.Add(rolePermObj);

                }
            }
            con.Close();
            return list;

        }
        public List<RolePermission> getCheckRolePermByRoleId(int Id)
        {
            List<RolePermission> list = new List<RolePermission>();
            RolePermission rolePermObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_RolePermission", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCheckRolePermByRoleId");
            cmd.Parameters.AddWithValue("@RoleId", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    rolePermObj = new RolePermission();
                    rolePermObj.Id = Convert.ToInt32(sdr["Id"]);
                    rolePermObj.Role_Id = Convert.ToInt32(sdr["Role_Id"]);
                    //     rolePermObj.PermissionName = sdr["Role_Id"].ToString();
                    //      rolePermObj.RoleName = sdr["RoleName"].ToString();
                    list.Add(rolePermObj);

                }
            }
            con.Close();
            return list;

        }


        public List<RolePermission> getEditRolePermissionDetailsById(int Id)
        {
            List<RolePermission> list = new List<RolePermission>();
            RolePermission rolePermObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_RolePermission", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getEditRolePermByRoleId");
            cmd.Parameters.AddWithValue("@RoleId", Id);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    rolePermObj = new RolePermission();
                    rolePermObj.Id = Convert.ToInt32(sdr["Id"]);
                    rolePermObj.PermissionName = sdr["PermissionName"].ToString();
                    if (sdr["Permission_Id"] != DBNull.Value)
                    {
                        rolePermObj.Perm_Id = 1;
                    }
                    else
                    {
                        rolePermObj.Perm_Id = 0;
                    }
                    list.Add(rolePermObj);

                }
            }
            con.Close();
            return list;

        }



        public bool UpdateRolePermissionDetails(RolePermission RPObj)
        {
            SqlCommand cmd = new SqlCommand("sp_RolePermission", con);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateRolePerm");
                cmd.Parameters.AddWithValue("@Id", RPObj.Id);
                cmd.Parameters.AddWithValue("@RoleId", RPObj.Role_Id);
                cmd.Parameters.AddWithValue("@PermissionId", RPObj.Permission_Id);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return true;
        }
        public int DeleteRolePermission(int Id)
        {
            SqlCommand cmd = new SqlCommand("sp_RolePermission", con);

            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteRolePermission");
                con.Open();
                cmd.Parameters.AddWithValue("@RoleId", Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return row;
        }


        public List<PermissionAllow_Model> getPermissionDetails()
        {
            List<PermissionAllow_Model> list = new List<PermissionAllow_Model>();
            PermissionAllow_Model roleObj;
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Permission", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getPermissionDetails");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    roleObj = new PermissionAllow_Model();
                    roleObj.Id = Convert.ToInt32(sdr["Id"]);
                    roleObj.PermissionName = sdr["PermissionName"].ToString();
                    // roleObj.PermissionDescription = sdr["status"].ToString();
                    list.Add(roleObj);
                }
            }
            con.Close();
            return list;

        }


        #endregion


    }
}