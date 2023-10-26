using Microsoft.Ajax.Utilities;
using StoreMgmt.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;

namespace StoreMgmt.Controllers
{
    public class HomeController : Controller
    {
        DataLayer dl = new DataLayer();


        #region manage dashboard

        public ActionResult Index()
        {
            if (Session["username"] != null)
            {

                ViewBag.VederCount = dl.getVendorDetails().Count();
                ViewBag.PersonCount = dl.getPersonDetails().Count();
                ViewBag.ItemCount = dl.getItemDetails().Count();
                ViewBag.Items2Count = dl.getItems2Details().Count();
                ViewBag.CircleCount = dl.getCircleDetails().Count();
                ViewBag.BlankCount = dl.GetBlankDetails().Count();
                ViewBag.Items2Count = dl.getItems2Details().Count();

                ViewBag.JobCount = dl.getJobDetails().Count();
                ViewBag.stockInwdCount = dl.getStockInwardDetails().Count();
                ViewBag.IssueCount = dl.getIssueDetails().Count();

                ViewBag.BlankJobCount = dl.getJobDetails().Where(m => m.JobType == "Blank").Count();
                ViewBag.CoilCount = dl.getCoilDetails().Count();
                ViewBag.CircleJobCount = dl.getJobDetails().Where(m => m.JobType == "Circle").Count();
                ViewBag.CoilJobCount = dl.getJobDetails().Where(m => m.JobType == "Coil").Count();
                ViewBag.DepartmentCount = dl.getDepartmentDetails().Count();
                ViewBag.PolishingCount = dl.getPolishDetails().Count();
                ViewBag.selloutwardCount = dl.GetsellItemList().Count();
                //       ViewBag.Permission = dl.getRolePermissionDetails();



                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        #endregion

        #region manage login & logout


        public ActionResult Login2()
        {
            ViewBag.rolelist = new SelectList(dl.getRoleDetails(), "Id", "RoleName");

            return View();
        }

        [HttpPost]
        public ActionResult Login2(usermodel userobj)
        {

            List<usermodel> lst = new List<usermodel>();
            lst = dl.getlogin2Details(userobj);

            foreach (var item in lst)
            {
                Session["username"] = item.username;
                Session["pass"] = item.password;
                Session["Role"] = item.RoleName;
                Session["RoleId"] = item.RoleId;
            }
            List<RolePermission> rplist = new List<RolePermission>();
            int Id = Convert.ToInt32(Session["RoleId"]);

            rplist = dl.getRolePermissionDetailsById(Id);


            Session["rplist"] = rplist;


            if (lst.Count() > 0)
            {
                return RedirectToAction("Index");

            }
            else
            {
                TempData["loginMessage"] = "<script>alert('username or password is not correct please try again')</script>";

                return RedirectToAction("Login2");
            }

        }




        public ActionResult Logout()
        {

            Session.Abandon();

            return RedirectToAction("Login2");
        }



        #endregion


        #region manage change Password

        public ActionResult ChangePassword()
        {
            if (Session["username"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword objchang)
        {

            if (objchang.password == Session["pass"].ToString())
            {

                if (objchang.confPaswd == objchang.confPaswd)
                {
                    var username = Session["username"].ToString();
                    objchang.username = username;
                    objchang.password = objchang.newPaswd;
                    dl.changPass(objchang);
                    TempData["updatepswdMessage"] = "<script>alert('password updated successfully')</script>";
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["confpassMessage"] = "<script>alert('confirm password not match')</script>";
                    return View();
                }

            }
            else
            {
                TempData["existPass"] = "<script>alert('old password not exist please try again...')</script>";
                return View();
            }


        }

        #endregion

        #region manage person master
        //PersonMaster
        public ActionResult PersonMaster()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        public ActionResult AddPerson()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        [HttpPost]
        public ActionResult AddPerson(Person psd)
        {

            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {
                    dl.AddPersonDetails(psd);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Person Details added successfully')</script>";
                    return RedirectToAction("PersonList");
                }
                else
                {
                    return View(psd);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult PersonList(string searchBy, string search)
        {
            if (Session["username"] != null)
            {
                List<Person> prsnlist = new List<Person>();


                if (searchBy == "Name")
                {

                    prsnlist = dl.getPersonDetails().Where(model => model.PersonName.ToLower().Contains(search.ToLower())).ToList();

                    return View(prsnlist);
                }
                else if (searchBy == "Contact")
                {

                    prsnlist = dl.getPersonDetails().Where(model => model.Contact.ToLower().Contains(search.ToLower())).ToList();

                    return View(prsnlist);
                }
                else
                {
                    prsnlist = dl.getPersonDetails();

                    return View(prsnlist);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult PersonDataEdit(int Id)
        {
            if (Session["username"] != null)
            {
                var prsnObj = dl.getPersonById(Id).FirstOrDefault();
                return View(prsnObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult PersonDataEdit(Person prsn)
        {
            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {
                    dl.UpdatePrsn(prsn);
                    TempData["updateMessage"] = "<script>alert('Person Data  updated successfully')</script>";

                    return RedirectToAction("PersonList");
                }
                else
                {
                    return View(prsn);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }



        }


        public ActionResult DeletePerson(int Id)
        {


            if (Session["username"] != null)
            {
                var prsnObj = dl.getPersonById(Id).FirstOrDefault();

                return View(prsnObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        [HttpPost]
        public ActionResult DeletePerson(Person prsn)
        {
            dl.DeletePersnData(prsn);
            TempData["DeleteMessage"] = "<script>alert(' Person data deleted successfully')</script>";

            return RedirectToAction("PersonList");

        }

        #endregion


        #region manage vendor master

        public ActionResult VendorMaster()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }



        public ActionResult AddVendor()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }



        [HttpPost]
        public ActionResult AddVendor(Vendor vsd)
        {


            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {
                    dl.AddVendorDetails(vsd);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Vendor Details added successfully')</script>";
                    return RedirectToAction("VendorList");
                }
                else
                {
                    return View(vsd);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }

        }
        public ActionResult VendorList(string searchBy, string search)
        {
            if (Session["username"] != null)
            {
                List<Vendor> vendlist = new List<Vendor>();

                if (searchBy == "Name")
                {
                    vendlist = dl.getVendorDetails().Where(model => model.VendorName.ToLower().Contains(search.ToLower())).ToList();

                    return View(vendlist);
                }
                else if (searchBy == "Contact")
                {
                    vendlist = dl.getVendorDetails().Where(model => model.VendorContact.ToLower().Contains(search.ToLower())).ToList();

                    return View(vendlist);
                }
                else
                {

                    vendlist = dl.getVendorDetails();

                    return View(vendlist);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }



        }



        public ActionResult VendorDataEdit(int Id)
        {
            if (Session["username"] != null)
            {
                var vendObj = dl.getVendorById(Id).FirstOrDefault();
                return View(vendObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult VendorDataEdit(Vendor vend)
        {
            if (Session["username"] != null)
            {

                if (ModelState.IsValid)
                {


                    dl.UpdateVendor(vend);
                    TempData["updateMessage"] = "<script>alert('Vendor Data  updated successfully')</script>";

                    return RedirectToAction("VendorList");
                }
                else
                {
                    return View(vend);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }



        }


        //Vendor data delete
        public ActionResult DeleteVendor(int Id)
        {
            if (Session["username"] != null)
            {
                var prsnObj = dl.getVendorById(Id).FirstOrDefault();

                return View(prsnObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }



        }

        [HttpPost]
        public ActionResult DeleteVendor(Vendor venObj)
        {
            dl.DeleteVendorData(venObj);
            TempData["DeleteMessage"] = "<script>alert('Vendor Data  Deleted successfully')</script>";

            return RedirectToAction("VendorList");

        }


        #endregion


        #region manage Item Master


        //ItemMaster
        public ActionResult ItemMaster()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        public ActionResult AddItem()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        [HttpPost]
        public ActionResult AddItem(ItemsModel itmObj)
        {


            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    dl.AddItemDetails(itmObj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Item Details added successfully')</script>";
                    return RedirectToAction("ItemList");
                }
                else
                {
                    return View(itmObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult ItemList(string searchBy, string search)
        {
            if (Session["username"] != null)
            {
                List<ItemsModel> itemlst = new List<ItemsModel>();

                if (searchBy == "Name")
                {
                    itemlst = dl.getItemDetails().Where(model => model.ItemName.ToLower().Contains(search.ToLower())).ToList();

                    return View(itemlst);
                }
                else if (searchBy == "Code")
                {
                    itemlst = dl.getItemDetails().Where(model => model.ItemCode.ToLower().Contains(search.ToLower())).ToList();

                    return View(itemlst);
                }
                else
                {
                    itemlst = dl.getItemDetails();

                    return View(itemlst);
                }
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        public ActionResult ItemDataEdit(int Id)
        {
            if (Session["username"] != null)
            {
                var itemObj = dl.getItemById(Id).FirstOrDefault();
                return View(itemObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult ItemDataEdit(ItemsModel itms)
        {
            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    dl.UpdateItem(itms);
                    TempData["updateMessage"] = "<script>alert('Item Data  updated successfully')</script>";

                    return RedirectToAction("ItemList");
                }
                else
                {
                    return View(itms);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }



        }


        //Item data delete
        public ActionResult DeleteItemData(int Id)
        {

            if (Session["username"] != null)
            {
                var itmObj = dl.getItemById(Id).FirstOrDefault();

                return View(itmObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }



        }

        [HttpPost]
        public ActionResult DeleteItemData(ItemsModel itmObj)
        {
            dl.DeleteItemData(itmObj);
            TempData["DeleteMessage"] = "<script>alert('Item Data  Deleted successfully')</script>";

            return RedirectToAction("ItemList");

        }

        #endregion

        #region manage Department master


        public ActionResult DepartmentMaster()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        public ActionResult AddDepartment()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        [HttpPost]
        public ActionResult AddDepartment(Department deptObj)
        {


            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    dl.AddDeptDetails(deptObj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Department Details added successfully')</script>";
                    return RedirectToAction("DeptList");
                }
                else
                {
                    return View(deptObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult DeptList(string searchBy, string search)
        {


            if (Session["username"] != null)
            {
                List<Department> deptlist = new List<Department>();



                if (searchBy == "Department")
                {
                    deptlist = dl.getDepartmentDetails().Where(model => model.DepartmentName.ToLower().Contains(search.ToLower())).ToList();

                    return View(deptlist);
                }
                else
                {
                    deptlist = dl.getDepartmentDetails();

                    return View(deptlist);
                }


            }
            else
            {
                return RedirectToAction("Login2");
            }




        }


        public ActionResult DeptDataEdit(int Id)
        {
            if (Session["username"] != null)
            {
                var deptObj = dl.getDeptById(Id).FirstOrDefault();
                return View(deptObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult DeptDataEdit(Department depts)
        {
            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    dl.UpdateDept(depts);
                    TempData["updateMessage"] = "<script>alert('Department Data  updated successfully')</script>";

                    return RedirectToAction("DeptList");
                }
                else
                {
                    return View(depts);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }



        }


        //Department data delete
        public ActionResult DeleteDeptData(int Id)
        {

            if (Session["username"] != null)
            {
                var deptObj = dl.getDeptById(Id).FirstOrDefault();
                return View(deptObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }



        }

        [HttpPost]
        public ActionResult DeleteDeptData(Department deptObj)
        {
            dl.DeleteDeptData(deptObj);
            TempData["DeleteMessage"] = "<script>alert('Dept Data  Deleted successfully')</script>";

            return RedirectToAction("DeptList");

        }


        #endregion

        #region manage StockInward 


        public ActionResult AddStockInward()
        {
            if (Session["username"] != null)
            {


                ViewBag.ddlVendor = new SelectList(dl.getVendorDetails(), "Id", "VendorName");

                ViewBag.ddlItem = new SelectList(dl.getItemDetails(), "Id", "ItemName");

                ViewBag.ddlPerson = new SelectList(dl.getPersonDetails(), "Id", "PersonName");

                return View();


            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        [HttpPost]
        public ActionResult AddStockInward(StockInwardAdd stObj)
        {


            if (Session["username"] != null)
            {


                ViewBag.ddlVendor = new SelectList(dl.getVendorDetails(), "Id", "VendorName");

                ViewBag.ddlItem = new SelectList(dl.getItemDetails(), "Id", "ItemName");

                ViewBag.ddlPerson = new SelectList(dl.getPersonDetails(), "Id", "PersonName");


                if (ModelState.IsValid)
                {


                    if (stObj.chooseItem == "Quantity")
                    {
                        stObj.ItemWeight = "0";
                    }
                    else if (stObj.chooseItem == "ItemWeight")
                    {
                        stObj.Quantity = "0";

                    }

                    dl.AddStockInwardDetails(stObj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Stock Inward Details added successfully')</script>";
                    return RedirectToAction("StockInwardList");
                }
                else
                {
                    return View(stObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        public ActionResult StockInwardList(string searchBy, string search)
        {

            if (Session["username"] != null)
            {
                List<StockInward> stockInwdlist = new List<StockInward>();

                if (searchBy == "Item")
                {
                    stockInwdlist = dl.getStockInwardDetails().Where(model => model.ddlItem.ToLower().Contains(search.ToLower())).ToList();

                    return View(stockInwdlist);
                }
                else if (searchBy == "Person")
                {
                    stockInwdlist = dl.getStockInwardDetails().Where(model => model.ddlPerson.ToLower().Contains(search.ToLower())).ToList();

                    return View(stockInwdlist);
                }
                else if (searchBy == "Vendor")
                {
                    stockInwdlist = dl.getStockInwardDetails().Where(model => model.ddlVendor.ToLower().Contains(search.ToLower())).ToList();

                    return View(stockInwdlist);
                }
                else
                {
                    stockInwdlist = dl.getStockInwardDetails();

                    return View(stockInwdlist);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        // edit stock Inward
        [HttpGet]
        public ActionResult StockInwardDataEdit(int Id)
        {

            if (Session["username"] != null)
            {

                ViewBag.ddlVendor = new SelectList(dl.getVendorDetails(), "Id", "VendorName");

                ViewBag.ddlItem = new SelectList(dl.getItemDetails(), "Id", "ItemName");

                ViewBag.ddlPerson = new SelectList(dl.getPersonDetails(), "Id", "PersonName");

                var stObj = dl.getStockInwardById(Id).FirstOrDefault();

                return View(stObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        [HttpPost]
        public ActionResult StockInwardDataEdit(StockInward stObj)
        {
            if (Session["username"] != null)
            {


                if (stObj.ddlVendor == null)
                {
                    stObj.ddlVendor = stObj.Vend_Id.ToString();
                }
                if (stObj.ddlItem == null)
                {
                    stObj.ddlItem = stObj.Item_Id.ToString();

                }
                if (stObj.ddlPerson == null)
                {
                    stObj.ddlPerson = stObj.Person_Id.ToString();

                }

                ViewBag.ddlVendor = new SelectList(dl.getVendorDetails(), "Id", "VendorName");

                ViewBag.ddlItem = new SelectList(dl.getItemDetails(), "Id", "ItemName");

                ViewBag.ddlPerson = new SelectList(dl.getPersonDetails(), "Id", "PersonName");

                if (ModelState.IsValid)
                {


                    if (stObj.chooseItem == "Quantity")
                    {
                        stObj.ItemWeight = "0";
                    }
                    else if (stObj.chooseItem == "ItemWeight")
                    {
                        stObj.Quantity = "0";

                    }

                    dl.UpdateStockInward(stObj);
                    TempData["updateMessage"] = "<script>alert('Stock Inward Data  updated successfully')</script>";

                    return RedirectToAction("StockInwardList");
                }
                else
                {
                    return View(stObj);
                }


            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        //Stock Inward data delete
        public ActionResult DeleteStockInwardData(int Id)
        {

            if (Session["username"] != null)
            {
                var deptObj = dl.getStockInwardById(Id).FirstOrDefault();
                return View(deptObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }



        }

        [HttpPost]
        public ActionResult DeleteStockInwardData(StockInward stObj)
        {
            dl.DeleteStockInwardData(stObj);
            TempData["DeleteMessage"] = "<script>alert('Dept Data  Deleted successfully')</script>";

            return RedirectToAction("StockInwardList");

        }





        #endregion

        #region manage Issue Item
        public ActionResult AddIssue()
        {
            if (Session["username"] != null)
            {
                ViewBag.deptlist = new SelectList(dl.getDepartmentDetails(), "Id", "DepartmentName");

                ViewBag.itmslist = new SelectList(dl.getItemDetails().ToList(), "Id", "ItemName");

                ViewBag.prsnlist = new SelectList(dl.getPersonDetails(), "Id", "PersonName");

                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        [HttpPost]
        public ActionResult AddIssue(Issue stObj, StockInward stwd)
        {


            if (Session["username"] != null)
            {
                ViewBag.deptlist = new SelectList(dl.getDepartmentDetails(), "Id", "DepartmentName");

                ViewBag.itmslist = new SelectList(dl.getItemDetails().ToList(), "Id", "ItemName");

                ViewBag.prsnlist = new SelectList(dl.getPersonDetails(), "Id", "PersonName");



                if (ModelState.IsValid)
                {
                    var itmQuantity = Convert.ToInt32(stObj.quantity);
                    int itemId = Convert.ToInt32(stObj.ddlItem);
                    var stockdata = dl.getItemStockDetailsByItemId(itemId);

                    if (stObj.chooseItem == "Quantity")
                    {
                        stObj.ItemWeight = "0";

                    }
                    else if (stObj.chooseItem == "ItemWeight")
                    {
                        stObj.quantity = "0";
                    }


                    dl.AddIssueDetails(stObj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Issue Obj Details added successfully')</script>";
                    return RedirectToAction("IssueList");
                }
                else
                {
                    return View(stObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult IssueList(string searchBy, string search)
        {
            if (Session["username"] != null)
            {
                List<Issue> Objlist = new List<Issue>();

                if (searchBy == "Item")
                {
                    Objlist = dl.getIssueDetails().Where(model => model.ddlItem.ToLower().Contains(search.ToLower())).ToList();

                    return View(Objlist);
                }
                else if (searchBy == "Person")
                {
                    Objlist = dl.getIssueDetails().Where(model => model.ddlPerson.ToLower().Contains(search.ToLower())).ToList();

                    return View(Objlist);
                }
                else if (searchBy == "Department")
                {
                    Objlist = dl.getIssueDetails().Where(model => model.ddlDept.ToLower().Contains(search.ToLower())).ToList();

                    return View(Objlist);
                }
                else
                {
                    Objlist = dl.getIssueDetails();

                    return View(Objlist);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        // edit stock Inward
        [HttpGet]
        public ActionResult IssueDataEdit(int Id)
        {

            if (Session["username"] != null)
            {

                ViewBag.deptlist = new SelectList(dl.getDepartmentDetails(), "Id", "DepartmentName");

                ViewBag.itmslist = new SelectList(dl.getItemDetails(), "Id", "ItemName");

                ViewBag.prsnlist = new SelectList(dl.getPersonDetails(), "Id", "PersonName");


                var stObj = dl.GetddlForIssue(Id).FirstOrDefault();
                return View(stObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult IssueDataEdit(Issue stObj)
        {
            if (Session["username"] != null)
            {

                ViewBag.deptlist = new SelectList(dl.getDepartmentDetails(), "Id", "DepartmentName");

                ViewBag.itmslist = new SelectList(dl.getItemDetails().ToList(), "Id", "ItemName");

                ViewBag.prsnlist = new SelectList(dl.getPersonDetails(), "Id", "PersonName");


                if (ModelState.IsValid)
                {


                    if (ModelState.IsValid)
                    {



                        if (stObj.chooseItem == "Quantity")
                        {
                            stObj.ItemWeight = "0";

                        }
                        else if (stObj.chooseItem == "ItemWeight")
                        {
                            stObj.quantity = "0";
                        }


                        dl.UpdateIssue(stObj);
                        TempData["updateMessage"] = "<script>alert('Issue Data  updated successfully')</script>";

                        return RedirectToAction("IssueList");
                    }
                    else
                    {
                        return View(stObj);
                    }


                }
                else
                {
                    return View(stObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        // Issue data delete
        public ActionResult DeleteIssueData(int Id)
        {

            if (Session["username"] != null)
            {
                var issueData = dl.getIssueDataById(Id).FirstOrDefault();
                return View(issueData);
            }
            else
            {
                return RedirectToAction("Login2");
            }



        }

        [HttpPost]
        public ActionResult DeleteIssueData(Issue stObj)
        {
            //    dl.De(stObj);
            TempData["DeleteMessage"] = "<script>alert('Issue Data  Deleted successfully')</script>";

            return RedirectToAction("IssueList");

        }



        #endregion

        #region manage Raw Invert


        public ActionResult CircleCuttingMaster()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        #region manage Coil
        public ActionResult AddCoil()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        [HttpPost]
        public ActionResult AddCoil(Coil_model coilObj)
        {


            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    dl.AddCoilDetails(coilObj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('coil Details added successfully')</script>";
                    return RedirectToAction("CoilList");
                }
                else
                {
                    return View(coilObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult CoilList()
        {


            if (Session["username"] != null)
            {
                List<Coil_model> coillst = new List<Coil_model>();

                coillst = dl.getCoilDetails();

                return View(coillst);

            }
            else
            {
                return RedirectToAction("Login2");
            }

        }




        public ActionResult CoilDataEdit(int Id)
        {
            if (Session["username"] != null)
            {
                var coilObj = dl.getCoilById(Id).FirstOrDefault();
                return View(coilObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult CoilDataEdit(Coil_model coils)
        {
            if (Session["username"] != null)
            {

                if (ModelState.IsValid)
                {
                    dl.UpdateCoil(coils);
                    TempData["updateMessage"] = "<script>alert('coil Data  updated successfully')</script>";

                    return RedirectToAction("CoilList");
                }
                else
                {
                    return View(coils);
                }


            }
            else
            {
                return RedirectToAction("Login2");
            }



        }


        //coil data delete
        public ActionResult DeleteCoilData(int Id)
        {

            if (Session["username"] != null)
            {
                var itmObj = dl.getCoilById(Id).FirstOrDefault();

                return View(itmObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }



        }

        [HttpPost]
        public ActionResult DeleteCoilData(Coil_model coilObj)
        {
            dl.DeleteCoilData(coilObj);
            TempData["DeleteMessage"] = "<script>alert('coil Data  Deleted successfully')</script>";

            return RedirectToAction("CoilList");

        }




        public ActionResult GetCoilDetailByCode(string Id)
        {

            if (Session["username"] != null)
            {
                var itmObj = dl.getCoilByCode(Id);

                return Json(itmObj, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        public ActionResult GetotalCoillistjson()
        {

            if (Session["username"] != null)
            {
                List<jobdetail> circls = new List<jobdetail>();
                circls = dl.GetTotalCoilList();
                return Json(circls, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }




        public ActionResult GetTotalCoilList()
        {

            if (Session["username"] != null)
            {
                List<jobdetail> coilList = new List<jobdetail>();
                coilList = dl.GetTotalCoilList();
                return View(coilList);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }



        #endregion





        #region manage Circle
        public ActionResult AddCircle()
        {
            if (Session["username"] != null)
            {


                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        [HttpPost]
        public ActionResult AddCircle(Circle_model circleObj)
        {


            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    dl.AddCirclDetails(circleObj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Circle Details added successfully')</script>";

                    return RedirectToAction("CircleList");


                }
                else
                {
                    return View(circleObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult CircleList()
        {

            if (Session["username"] != null)
            {
                List<Circle_model> circlellst = new List<Circle_model>();


                circlellst = dl.getCircleDetails();

                return View(circlellst);


            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        public ActionResult CircleDataEdit(int Id)
        {
            if (Session["username"] != null)
            {
                var circleObj = dl.getCircleById(Id).FirstOrDefault();
                return View(circleObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult CircleDataEdit(Circle_model circle, coilWeight cw)
        {
            if (Session["username"] != null)
            {

                if (ModelState.IsValid)
                {

                    dl.UpdateCircle(circle);
                    TempData["updateMessage"] = "<script>alert('Circle Data  updated successfully')</script>";


                    return RedirectToAction("CircleList");
                }
                else
                {
                    return View(circle);
                }


            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        //circle data delete
        public ActionResult DeleteCircleData(int Id)
        {

            if (Session["username"] != null)
            {
                var circleObj = dl.getCircleById(Id).FirstOrDefault();

                return View(circleObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }



        }

        [HttpPost]
        public ActionResult DeleteCircleData(Circle_model circleObj)
        {
            dl.DeleteCircleData(circleObj);
            TempData["DeleteMessage"] = "<script>alert('Circle Data  Deleted successfully')</script>";

            return RedirectToAction("CircleList");

        }



        public ActionResult getotalCirlelistjson()
        {

            if (Session["username"] != null)
            {
                List<jobdetail> circls = new List<jobdetail>();
                circls = dl.GetTotalCircleList();
                return Json(circls, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }


        public ActionResult GetTotalCircleList()
        {

            if (Session["username"] != null)
            {
                List<jobdetail> circleList = new List<jobdetail>();
                circleList = dl.GetTotalCircleList();
                return View(circleList);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }


        #endregion




        #region manage Blank


        public ActionResult AddBlank()
        {
            if (Session["username"] != null)
            {
                Blank_model bl = new Blank_model();

                return View(bl);
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        [HttpPost]
        public ActionResult AddBlank(Blank_model blankObj)
        {

            if (Session["username"] != null)
            {

                if (ModelState.IsValid)
                {
                    dl.AddBlankDetails(blankObj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Blank Details added successfully')</script>";
                    return RedirectToAction("BlankList");

                }
                else
                {
                    return View(blankObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        public ActionResult BlankList()
        {


            if (Session["username"] != null)
            {
                List<Blank_model> blanklist = new List<Blank_model>();


                blanklist = dl.GetBlankDetails();

                return View(blanklist);



            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult BlankDataEdit(int Id)
        {
            if (Session["username"] != null)
            {
                var Obj = dl.GetBlankById(Id).FirstOrDefault();

                return View(Obj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult BlankDataEdit(Blank_model blankObj, coilWeight cw)
        {
            if (Session["username"] != null)
            {

                if (ModelState.IsValid)
                {
                    dl.UpdateBlank(blankObj);
                    TempData["updateMessage"] = "<script>alert('Blank Data  updated successfully')</script>";

                    return RedirectToAction("BlankList");
                }
                else
                {
                    return View(blankObj);
                }


            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        //blank data delete
        public ActionResult DeleteBlankData(int Id)
        {

            if (Session["username"] != null)
            {
                var circleObj = dl.GetBlankById(Id).FirstOrDefault();

                return View(circleObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        [HttpPost]
        public ActionResult DeleteBlankData(Blank_model blankObj)
        {
            dl.DeleteBlankData(blankObj);
            TempData["DeleteMessage"] = "<script>alert('Blank Data  Deleted successfully')</script>";

            return RedirectToAction("BlankList");

        }


        public ActionResult GetTotalBlankList()
        {

            if (Session["username"] != null)
            {
                List<jobdetail> blanklist = new List<jobdetail>();
                blanklist = dl.GetTotalBlankList();
                return View(blanklist);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }


        public ActionResult GetotatBlanklistjson()
        {

            if (Session["username"] != null)
            {
                List<jobdetail> circls = new List<jobdetail>();
                circls = dl.GetTotalBlankList();
                return Json(circls, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }


        #endregion



        #endregion


        #region manage Create job

        public ActionResult CreateJob()
        {
            if (Session["username"] != null)
            {

                Job jb = new Job();
                //   ViewBag.Circle = dl.getCircleDetails().ToList();
                ViewBag.Coil = dl.getCoilDetails().ToList();
                ViewBag.Blank = dl.GetBlankDetails().ToList();
                return View(jb);
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        [HttpPost]
        public ActionResult CreateJob(Job jobObj, coilWeight cw, Coil_model coillObj)
        {




            if (Session["username"] != null)
            {
                //  ViewBag.Circle = dl.getCircleDetails().ToList();
                ViewBag.Coil = dl.getCoilDetails().ToList();
                ViewBag.Blank = dl.GetBlankDetails().ToList();

                float oldcoilwieght = float.Parse(jobObj.oldWeight);
                float CuttingWeight = Convert.ToInt32(jobObj.CuttingWeight);
                // var scrap=jobObj.Scrap;
                //  float coilupdateweight = 0;

                if (ModelState.IsValid)
                {

                    if (jobObj.JobName == "Coil")
                    {

                        //  jobObj.Scrap = "0";
                        //  jobObj.SlitWeight   = "0";
                        //  jobObj.SlitWidth   = "0";
                        // jobObj.Recovery = "0";
                        //  jobObj.slit = "No";


                        float balanceCoilWeight = oldcoilwieght - CuttingWeight;
                        jobObj.BalanceCoilWeight = balanceCoilWeight.ToString();

                        coillObj.Width = jobObj.oldWidth;
                        coillObj.Weight = jobObj.CuttingWeight;
                        dl.AddJobDetails(jobObj);
                        coillObj.AddFrom = "Inside";

                        dl.AddCoilDetails(coillObj);
                        ModelState.Clear();
                        TempData["InsertMessage"] = "<script>alert('Job Created successfully')</script>";


                        cw.Id = jobObj.rowId;
                        cw.newCoilWidth = jobObj.oldWidth;
                        cw.newCoilWeight = balanceCoilWeight.ToString();
                        dl.UpdateCoilWeight(cw);

                    }
                    else
                    {



                        //  var oldCoilWeight = Convert.ToInt32(jobObj.oldWeight);


                        if (jobObj.slit == "Yes")
                        {

                            float a = float.Parse(jobObj.Scrap);
                            float b = float.Parse(jobObj.SlitWeight);
                            float actualscrap = (a - b);


                            jobObj.Scrap = actualscrap.ToString();
                            dl.AddJobDetails(jobObj);
                            ModelState.Clear();
                            TempData["InsertMessage"] = "<script>alert('Job Created successfully')</script>";


                            coillObj.Weight = jobObj.SlitWeight;
                            coillObj.Width = jobObj.SlitWidth;
                            coillObj.AddFrom = "Inside";

                            dl.AddCoilDetails(coillObj);
                            cw.newCoilWeight = jobObj.BalanceCoilWeight;
                            cw.newCoilWidth = jobObj.oldWidth;
                            cw.Id = jobObj.rowId;
                            dl.UpdateCoilWeight(cw);

                        }
                        else if (jobObj.slit == "No")
                        {




                            dl.AddJobDetails(jobObj);
                            ModelState.Clear();
                            TempData["InsertMessage"] = "<script>alert('Job Created successfully')</script>";


                            // coilupdateweight = 0;
                            cw.newCoilWeight = jobObj.BalanceCoilWeight;

                            cw.newCoilWidth = jobObj.oldWidth;


                            cw.Id = jobObj.rowId;
                            dl.UpdateCoilWeight(cw);
                        }
                        else
                        {
                            TempData["selectslit"] = "<script>alert('please select slit Yes/No')</script>";
                            return RedirectToAction("CreateJob");

                        }
                        return RedirectToAction("joblist");


                    }
                    return RedirectToAction("joblist");


                }
                else
                {
                    return View(jobObj);
                }
            }
            else
            {
                return RedirectToAction("Login2");
            }


        }


        public ActionResult joblist()
        {
            if (Session["username"]!=null)
            {

                List<jobdetail> jobs = new List<jobdetail>();
                jobs = dl.getJobDetails();
                return View(jobs);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        public ActionResult deletjob(int id)
        {
            if (Session["username"] != null)
            {


                var job = dl.GetJobById(id).FirstOrDefault();
                int rowId = Convert.ToInt32(job.rowId);
                float jobWeight = float.Parse( job.ConsumeWeight);

                var coillist = dl.getCoilById(rowId).FirstOrDefault();

                float coilweight = float.Parse(coillist.Weight);
                float toalWeight = jobWeight + coilweight;

                updateRevesrsecoilWeight Obj = new updateRevesrsecoilWeight();
                Obj.rowId = job.rowId;
                Obj.reverseCoilWeight = toalWeight.ToString();
                dl.UpdateRevCoilWeight(Obj);

                  dl.delet_job(id);

                TempData["deletemessage"] = "<script>alert('job deleted successfully, please delete slit coil from coil list')</script>";

                return RedirectToAction("joblist");

            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        #endregion


        #region manage ItemMaster 2
        public ActionResult ItemsMaster2()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        public ActionResult AddItems2()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        [HttpPost]
        public ActionResult AddItems2(Item_Model2 itemsObj)
        {


            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    dl.AddItems2Details(itemsObj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Items Details added successfully')</script>";
                    return RedirectToAction("Items2List");
                }
                else
                {
                    return View(itemsObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult Items2List(string searchBy, string search)
        {


            if (Session["username"] != null)
            {
                List<Item_Model2> itemslist = new List<Item_Model2>();



                if (searchBy == "Name")
                {
                    itemslist = dl.getItems2Details().Where(model => model.Name.ToLower().Contains(search.ToLower())).ToList();

                    return View(itemslist);
                }
                else
                {
                    itemslist = dl.getItems2Details();

                    return View(itemslist);
                }


            }
            else
            {
                return RedirectToAction("Login2");
            }




        }


        public ActionResult ItemsRecordEdit(int Id)
        {
            if (Session["username"] != null)
            {
                var itemObj = dl.getItems2ById(Id).FirstOrDefault();
                return View(itemObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult ItemsRecordEdit(Item_Model2 itemsObj)
        {
            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    dl.UpdateItems2(itemsObj);
                    TempData["updateMessage"] = "<script>alert('Item Data  updated successfully')</script>";

                    return RedirectToAction("Items2List");
                }
                else
                {
                    return View(itemsObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }



        }


        //Items 2 data delete
        public ActionResult DeleteItemsRecord(int Id)
        {

            if (Session["username"] != null)
            {
                var itemsObj = dl.getItems2ById(Id).FirstOrDefault();
                return View(itemsObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }



        }

        [HttpPost]
        public ActionResult DeleteItemsRecord(Item_Model2 itemsObj)
        {
            dl.DeleteItems2Data(itemsObj);
            TempData["DeleteMessage"] = "<script>alert('Items Data  Deleted successfully')</script>";

            return RedirectToAction("Items2List");

        }


        #endregion

        #region manage Issue Polishing


        public ActionResult IssuePolishing(string jobType)
        {

            if (Session["username"] != null)
            {

                Polishing_Model pls = new Polishing_Model();
                ViewBag.JobDetails = dl.getJobDetails().ToList();
                ViewBag.ddlItems = new SelectList(dl.getItems2Details(), "Id", "Name");

                return View(pls);
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        [HttpPost]
        public ActionResult IssuePolishing(Polishing_Model polishObj, jobweightUpdate jbw)
        {




            if (Session["username"] != null)
            {

                var oldJobWeight = float.Parse(polishObj.JobWeight);
                var jobPices = Convert.ToInt32(polishObj.JobPices);
                var PolishPices = Convert.ToInt32(polishObj.PolishPices);
                var issueJobWeight = float.Parse(polishObj.JobIssueWeight);
                var updateJobWeight = oldJobWeight - issueJobWeight;
                var remainPices = jobPices - PolishPices;
                ViewBag.JobDetails = dl.getJobDetails().ToList();
                ViewBag.ddlItems = new SelectList(dl.getItems2Details(), "Id", "Name");

                if (ModelState.IsValid)
                {

                    dl.IssueToPolishDetails(polishObj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('Issue for Polishing  successfully')</script>";



                    //if (polishObj.JobType == "Coil")
                    //{

                    //    jbw.Id = polishObj.jobId;
                    //    jbw.JobType = polishObj.JobType;
                    //    jbw.JobUpdateWeight = updateJobWeight.ToString();
                    //    jbw.JobUpdatePices = "0";
                    //    dl.UpdateJobWeight(jbw);
                    //}
                    //else



                    jbw.Id = polishObj.jobId;
                    jbw.JobType = polishObj.JobType;
                    jbw.JobUpdateWeight = updateJobWeight.ToString();
                    jbw.JobUpdatePices = remainPices.ToString();
                    dl.UpdateJobWeight(jbw);


                    return View();
                }
                else
                {
                    return View(polishObj);
                }


            }
            else
            {
                return RedirectToAction("Login2");
            }


        }



        #endregion



        #region manage user master


        public ActionResult AddUsers()
        {
            if (Session["username"] != null)
            {
                ViewBag.department = new SelectList(dl.getDepartmentDetails(), "Id", "DepartmentName");
                ViewBag.rolelist = new SelectList(dl.getRoleDetails(), "Id", "RoleName");

                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        [HttpPost]
        public ActionResult AddUsers(usermodel userobj)
        {

            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    ViewBag.department = new SelectList(dl.getDepartmentDetails(), "Id", "DepartmentName");
                    ViewBag.rolelist = new SelectList(dl.getRoleDetails(), "Id", "RoleName");

                    dl.AddUsersDetails(userobj);
                    ModelState.Clear();
                    TempData["InsertMessage"] = "<script>alert('User Details added successfully')</script>";
                    return RedirectToAction("UsersList");
                }
                else
                {
                    return View(userobj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult UsersList()
        {
            if (Session["username"] != null)
            {
                List<usermodel> userslist = new List<usermodel>();

                userslist = dl.getUsersDetails();

                return View(userslist);


            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult UserDataEdit(int Id)
        {
            if (Session["username"] != null)
            {

                ViewBag.department = new SelectList(dl.getDepartmentDetails(), "Id", "DepartmentName");
                ViewBag.rolelist = new SelectList(dl.getRoleDetails(), "Id", "RoleName");

                var userObj = dl.getUserById(Id).FirstOrDefault();
                return View(userObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }


        [HttpPost]
        public ActionResult UserDataEdit(usermodel userobj)
        {
            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {
                    if (userobj.RoleName == null)
                    {
                        userobj.RoleName = userobj.RoleId.ToString();
                    }
                    if (userobj.department == null)
                    {
                        userobj.department = userobj.deptId.ToString();

                    }
                    ViewBag.department = new SelectList(dl.getDepartmentDetails(), "Id", "DepartmentName");
                    ViewBag.rolelist = new SelectList(dl.getRoleDetails(), "Id", "RoleName");
                    dl.UpdateuserData(userobj);
                    TempData["updateMessage"] = "<script>alert('Users Data  updated successfully')</script>";

                    return RedirectToAction("UsersList");
                }
                else
                {
                    return View(userobj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }



        }


        public ActionResult DeleteUsersRecord(int Id)
        {


            if (Session["username"] != null)
            {
                var prsnObj = dl.getUserById(Id).FirstOrDefault();

                return View(prsnObj);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        [HttpPost]
        public ActionResult DeleteUsersRecord(usermodel userobj)
        {
            dl.DeleteUserData(userobj);
            TempData["DeleteMessage"] = "<script>alert(' Users Record Deleted Successfully...')</script>";

            return RedirectToAction("UsersList");

        }



        #endregion

        #region manage role and permission


        public ActionResult PermissionAllow()
        {
            ViewBag.rolelist = new SelectList(dl.getRoleDetails(), "Id", "RoleName");
            // ViewBag.UserList = new SelectList(dl.getUsersDetails(), "Id", "UserName");
            List<PermissionAllow_Model> permList = new List<PermissionAllow_Model>();
            if (Session["username"] != null)
            {

                permList = dl.getPermissionDetails();

                return View(permList);
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        [HttpPost]
        public ActionResult PermissionAllow(RolePermission RolePermObj)
        {
            //      List<RolePermission> roleperList = new List<RolePermission>();

            if (Session["username"] != null)
            {

                ViewBag.rolelist = new SelectList(dl.getRoleDetails(), "Id", "RoleName");
                //int selectRoleId = RolePermObj.Role_Id;

                //  ViewBag.UserList = new SelectList(dl.getUsersDetails(), "Id", "UserName");

                List<PermissionAllow_Model> permList = new List<PermissionAllow_Model>();

                var roleperList = RolePermObj.Permission_Id;
                var RoleId = RolePermObj.Role_Id;

                if (ModelState.IsValid)
                {
                    var checkRoleId = dl.getCheckRolePermByRoleId(RoleId);
                    if (checkRoleId != null)
                    {

                        permList = dl.getPermissionDetails();

                        TempData["CheckExistMessage"] = "<script>alert('Role Permission Allready Allowed for this role')</script>";
                        return View(permList);

                    }
                    else
                    {
                        foreach (var permId in roleperList)
                        {
                            RolePermObj.Perm_Id = permId;

                            dl.AddRolePermissionDetails(RolePermObj);

                        }
                        ModelState.Clear();
                        TempData["InsertMessage"] = "<script>alert('Role Permission  Allowed successfully')</script>";
                        return RedirectToAction("RolePermissionList");
                    }


                }
                else
                {
                    return View(RolePermObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }
        //permission list

        public ActionResult RolePermissionList()
        {
            if (Session["username"] != null)
            {
                List<RolePermission> RPlist = new List<RolePermission>();

                RPlist = dl.getRolePermissionDetails();

                return View(RPlist);


            }
            else
            {
                return RedirectToAction("Login2");
            }


        }

        public ActionResult PermissionAllowEdit(int Id)
        {
            List<RolePermission> rplist = new List<RolePermission>();

            //   List<Role_model> roleList = new List<Role_model>();
            ViewBag.roleData = dl.getRoleDetailsById(Id);




            if (Session["username"] != null)
            {


                //  List<PermissionAllow_Model> permList = new List<PermissionAllow_Model>();

                //  permList = dl.getPermissionDetails();

                //  int Id = Convert.ToInt32(Session["RoleId"]);

                rplist = dl.getEditRolePermissionDetailsById(Id);

                return View(rplist);
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        [HttpPost]
        public ActionResult PermissionAllowEdit(RolePermission RolePermObj)
        {
            //      List<RolePermission> roleperList = new List<RolePermission>();

            if (Session["username"] != null)
            {
                dl.DeleteRolePermission(RolePermObj.Role_Id);
                //  ViewBag.rolelist = new SelectList(dl.getRoleDetails(), "Id", "RoleName");
                //    ViewBag.UserList = new SelectList(dl.getUsersDetails(), "Id", "UserName");
                var roleperList = RolePermObj.Permission_Id;
                //   var   Role = RolePermObj.Role_Id;
                if (ModelState.IsValid)
                {

                    foreach (var permId in roleperList)
                    {
                        RolePermObj.Perm_Id = permId;

                        dl.AddRolePermissionDetails(RolePermObj);

                    }
                    ModelState.Clear();
                    TempData["updateMessage"] = "<script>alert('Role Permission Accessibility  updated successfully')</script>";
                    return RedirectToAction("RolePermissionList");
                }
                else
                {
                    return View(RolePermObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }


        }


        #endregion
        

        #region manage selloutward

        public ActionResult SellOutWard()
        {

            if (Session["username"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login2");
            }

        }
        [HttpPost]
        public ActionResult SellOutWard(selloutward_model itmObj)
        {

            if (Session["username"] != null)
            {
                if (ModelState.IsValid)
                {

                    dl.SellItems(itmObj);
                    TempData["InsertMessage"] = "<script>alert('selling has been done ')</script>";


                    selloutupdateweight updateselloutObj = new selloutupdateweight();
                    updateselloutObj.rowid = Convert.ToInt32(itmObj.rowid);
                    updateselloutObj.itemtype = itmObj.itemtype;
                    updateselloutObj.addfrom = itmObj.addfrom;



                    float oldweight = float.Parse(itmObj.oldweight);
                    float newweight = float.Parse(itmObj.weight);
                    float remainingweight = 0f;
                    if (oldweight >= newweight)
                    {

                        remainingweight = oldweight - newweight;
                        updateselloutObj.updateweight = remainingweight.ToString();
                    }
                    else
                    {
                        TempData["alertmessage"] = "<script>alert('please enter weight equal or less than select row weight')</script>";
                        return View(itmObj);
                    }

                    if (itmObj.itemtype == "Circle")
                    {
                        if (itmObj.addfrom == "Outside")
                        {

                            dl.updateOutsidecircleitems(updateselloutObj);
                        }
                        else
                        {
                            dl.updateInsidecircleitems(updateselloutObj);
                        }
                    }
                    else if (itmObj.itemtype == "Blank")
                    {

                        if (itmObj.addfrom == "Outside")
                        {
                            dl.updateOutsideblanksitems(updateselloutObj);
                        }
                        else
                        {
                            dl.updateInsideblanksitems(updateselloutObj);
                        }
                    }
                    else if (itmObj.itemtype == "Coil")
                    {
                        dl.updatejobCoilweightitems(updateselloutObj);
                    }

                    return RedirectToAction("SellOutWardList");
                }
                else
                {
                    return View(itmObj);
                }

            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        public ActionResult SellOutWardList()
        {

            if (Session["username"] != null)
            {
                List<selloutward_model> lst = new List<selloutward_model>();
                lst = dl.GetsellItemList();


                return View(lst);

            }
            else
            {
                return RedirectToAction("Login2");
            }

        }

        #endregion




        #region  manage report

        public ActionResult Report()
        {

            if (Session["username"] != null)
            {

                ViewBag.VederCount = dl.getVendorDetails().Count();
                ViewBag.PersonCount = dl.getPersonDetails().Count();
                ViewBag.ItemCount = dl.getItemDetails().Count();

                ViewBag.stockInwdCount = dl.getStockInwardDetails().Count();
                ViewBag.IssueCount = dl.getIssueDetails().Count();
                ViewBag.BlankCount = dl.GetBlankDetails().Count();

                ViewBag.CoilCount = dl.getCoilDetails().Count();
                ViewBag.JobCount = dl.getJobDetails().Count();
                ViewBag.PolishingCount = dl.getPolishDetails().Count();

                ViewBag.TotalBlankCount = dl.GetTotalBlankList().Count();
                ViewBag.TotalCircleCount = dl.GetTotalCircleList().Count();
                ViewBag.TotalCoilCount = dl.GetTotalCoilList().Count();



                float totalJobScrap = 0;
                var j = dl.getJobDetails().Count();

                List<jobdetail> totalJobScrapInJob = new List<jobdetail>();
                if (j > 0)
                {
                    totalJobScrapInJob = dl.getTotalJobScrap();
                    foreach (var item in totalJobScrapInJob)
                    {

                        totalJobScrap = float.Parse(item.Scrap);

                    }
                }

                ViewBag.TotalJobScrap = totalJobScrap;






                List<Polishing_Model> PolishScrap = new List<Polishing_Model>();
                PolishScrap = dl.getTotalPolishScrap();

                float totalPolishScrap = 0;
                var p = dl.getPolishDetails().Count();

                if (p > 0)
                {

                    foreach (var item in PolishScrap)
                    {

                        totalPolishScrap = float.Parse(item.PolishScrap);

                    }
                }
                ViewBag.TotalPolishScrap = totalPolishScrap;



                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        public ActionResult VendorReport()
        {

            if (Session["username"] != null)

            {
                List<Vendor> vendlist = new List<Vendor>();
                vendlist = dl.getVendorDetails();

                return View(vendlist);
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }
        public ActionResult PersonReport()
        {

            if (Session["username"] != null)
            {
                List<Person> prsnlist = new List<Person>();
                prsnlist = dl.getPersonDetails();
                return View(prsnlist);
            }
            else
            {
                return RedirectToAction("Login2");
            }

        }
        public ActionResult ItemReport()
        {

            if (Session["username"] != null)

            {
                List<ItemsModel> items = new List<ItemsModel>();
                items = dl.getItemDetails();
                return View(items);

            }
            else
            {
                return RedirectToAction("Login2");

            }
        }



        public ActionResult BlankReport()
        {

            if (Session["username"] != null)

            {
                List<Blank_model> blanklist = new List<Blank_model>();
                blanklist = dl.GetBlankDetails();
                return View(blanklist);

            }
            else
            {
                return RedirectToAction("Login2");

            }
        }



        public ActionResult CircleReport()
        {

            if (Session["username"] != null)

            {
                List<Circle_model> circlelist = new List<Circle_model>();
                circlelist = dl.getCircleDetails();
                return View(circlelist);

            }
            else
            {
                return RedirectToAction("Login2");

            }
        }

        public ActionResult CoilReport()
        {

            if (Session["username"] != null)

            {
                List<Coil_model> coillist = new List<Coil_model>();
                coillist = dl.getCoilReportList();
                return View(coillist);

            }
            else
            {
                return RedirectToAction("Login2");
            }
        }



        #endregion

        #region manage stock report

        public ActionResult StockInwardReport(string start, string end)
        {

            if (Session["username"] != null)

            {
                List<StockInward> stocklist = new List<StockInward>();
                stocklist = dl.getStockInwardDetails();

                var totalstock = 0;

                foreach (var item in stocklist)
                {
                    var stock = Convert.ToInt32(item.Quantity);

                    totalstock = totalstock + stock;
                }
                ViewBag.TotalStock = totalstock;

                if (start != null && end != null && start != "" && end != "")
                {
                    return View(dl.getstockInwardfilterBy(start, end));

                }
                else
                {

                    return View(stocklist);

                }

            }
            else
            {
                return RedirectToAction("Login2");

            }
        }


        public ActionResult totalStockItem()
        {

            if (Session["username"] != null)
            {
                List<StockInward> stocklist = new List<StockInward>();
                stocklist = dl.getItemTotalItem();

                return Json(stocklist, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");

            }
        }



        public ActionResult GetItemStockDetailsByItemId(int Id)
        {

            if (Session["username"] != null)
            {
                List<StockInward> stocklist = new List<StockInward>();
                stocklist = dl.getItemStockDetailsByItemId(Id);


                List<StockInward> stockQuantityWeight = new List<StockInward>();
                stockQuantityWeight = dl.getTotalStockQuantityByItemId(Id);


                var totalStockQuantity = 0;
                var totalItemWeightStock = 0;
                if (stockQuantityWeight != null)
                {
                    foreach (var item in stockQuantityWeight)
                    {
                        if (item.Quantity != null && item.Quantity != "")
                        {
                            totalStockQuantity = Convert.ToInt32(item.Quantity);

                        }

                        if (item.ItemWeight != null && item.ItemWeight != "")
                        {
                            totalItemWeightStock = Convert.ToInt32(item.ItemWeight);

                        }
                    }

                }

                List<Issue> issueItem = new List<Issue>();
                issueItem = dl.GetTotalIssueQuantityByItemId(Id);


                var totalIssueQuantity = 0;
                var totalIssueItemWeight = 0;
                if (issueItem != null)
                {
                    foreach (var item in issueItem)
                    {
                        if (item.quantity != null && item.quantity != "")
                        {
                            totalIssueQuantity = Convert.ToInt32(item.quantity);

                        }
                        if
                         (item.ItemWeight != null && item.ItemWeight != "")
                        {
                            totalIssueItemWeight = Convert.ToInt32(item.ItemWeight);

                        }

                    }

                }


                var availablestockQuantity = 0;
                if (totalStockQuantity > totalIssueQuantity)
                {
                    availablestockQuantity = totalStockQuantity - totalIssueQuantity;
                }

                var availablestockItemWeight = 0;
                if (totalItemWeightStock > totalIssueItemWeight)
                {
                    availablestockItemWeight = totalItemWeightStock - totalIssueItemWeight;
                }


                Session["availableItemQuantity"] = availablestockQuantity;
                Session["availableItemWeight"] = availablestockItemWeight;

                //  List<string> alldata = new List<string>();

                IDictionary<string, object> alldata = new Dictionary<string, object>();
                alldata.Add(new KeyValuePair<string, object>("stocklist", stocklist));
                alldata.Add(new KeyValuePair<string, object>("totalStockQuantity", totalStockQuantity));
                alldata.Add(new KeyValuePair<string, object>("totalIssueQuantity", totalIssueQuantity));

                alldata.Add(new KeyValuePair<string, object>("totalStockWeight", totalItemWeightStock));
                alldata.Add(new KeyValuePair<string, object>("totalIssueWeight", totalIssueItemWeight));
                alldata.Add(new KeyValuePair<string, object>("Quantityavailability", availablestockQuantity));
                alldata.Add(new KeyValuePair<string, object>("ItemWeightavailability", availablestockItemWeight));

                return Json(alldata, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");

            }
        }


        public ActionResult itemavailabilityReport()
        {

            if (Session["username"] != null)

            {
                List<StockInward> stocklist = new List<StockInward>();
                stocklist = dl.getItemTotalItem();

                return View(stocklist);

            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        #endregion


        #region manage issue report 

        public ActionResult IssueReport()
        {

            if (Session["username"] != null)
            {
                List<Issue> isslist = new List<Issue>();
                isslist = dl.getIssueDetails();
                return View(isslist);
            }
            else
            {
                return RedirectToAction("Login2");

            }
        }


        public ActionResult IssueItemReportByItem()
        {

            if (Session["username"] != null)

            {
                List<Issue> issueList = new List<Issue>();
                issueList = dl.GetTotalIssueItem();

                return View(issueList);

            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        public ActionResult GetIssueItemsDetailsByItemId(int Id)
        {

            if (Session["username"] != null)
            {
                List<Issue> issuelist = new List<Issue>();
                issuelist = dl.getIssueDataByItemId(Id);



                List<StockInward> stockQuantityWeight = new List<StockInward>();
                stockQuantityWeight = dl.getTotalStockQuantityByItemId(Id);


                var totalStockQuantity = 0;
                var totalItemWeightStock = 0;
                if (stockQuantityWeight != null)
                {
                    foreach (var item in stockQuantityWeight)
                    {
                        if (item.Quantity != null && item.Quantity != "")
                        {
                            totalStockQuantity = Convert.ToInt32(item.Quantity);

                        }

                        if (item.ItemWeight != null && item.ItemWeight != "")
                        {
                            totalItemWeightStock = Convert.ToInt32(item.ItemWeight);

                        }
                    }

                }

                List<Issue> issueItem = new List<Issue>();
                issueItem = dl.GetTotalIssueQuantityByItemId(Id);


                var totalIssueQuantity = 0;
                var totalIssueItemWeight = 0;
                if (issueItem != null)
                {
                    foreach (var item in issueItem)
                    {
                        if (item.quantity != null && item.quantity != "")
                        {
                            totalIssueQuantity = Convert.ToInt32(item.quantity);

                        }
                        if
                         (item.ItemWeight != null && item.ItemWeight != "")
                        {
                            totalIssueItemWeight = Convert.ToInt32(item.ItemWeight);

                        }

                    }

                }


                var availablestockQuantity = 0;
                if (totalStockQuantity > totalIssueQuantity)
                {
                    availablestockQuantity = totalStockQuantity - totalIssueQuantity;
                }

                var availablestockItemWeight = 0;
                if (totalItemWeightStock > totalIssueItemWeight)
                {
                    availablestockItemWeight = totalItemWeightStock - totalIssueItemWeight;
                }


                Session["availableItemQuantity"] = availablestockQuantity;
                Session["availableItemWeight"] = availablestockItemWeight;

                //  List<string> alldata = new List<string>();

                IDictionary<string, object> alldata = new Dictionary<string, object>();
                alldata.Add(new KeyValuePair<string, object>("issuelist", issuelist));
                alldata.Add(new KeyValuePair<string, object>("totalStockQuantity", totalStockQuantity));
                alldata.Add(new KeyValuePair<string, object>("totalIssueQuantity", totalIssueQuantity));

                alldata.Add(new KeyValuePair<string, object>("totalStockWeight", totalItemWeightStock));
                alldata.Add(new KeyValuePair<string, object>("totalIssueWeight", totalIssueItemWeight));
                alldata.Add(new KeyValuePair<string, object>("Quantityavailability", availablestockQuantity));
                alldata.Add(new KeyValuePair<string, object>("ItemWeightavailability", availablestockItemWeight));
                return Json(alldata, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");

            }
        }



        #endregion


        #region manage job report


        public ActionResult GetJobReportList()
        {

            if (Session["username"] != null)

            {
                List<jobdetail> jobs = new List<jobdetail>();
                jobs = dl.getJobDetails();
                return View(jobs);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }


        public ActionResult JobReport()
        {

            if (Session["username"] != null)
            {
                List<jobdetail> jobs = new List<jobdetail>();
                jobs = dl.getJobReportList();
                return View(jobs);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }

        public ActionResult JobReportDetailsByCode(string Id)
        {

            if (Session["username"] != null)
            {
                List<jobdetail> jobs = new List<jobdetail>();
                jobs = dl.getJobReportDetailsByCode(Id);
                return Json(jobs, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }


        #endregion


        #region manage polish report

        public ActionResult PolishReport()
        {

            if (Session["username"] != null)
            {
                List<Polishing_Model> pls = new List<Polishing_Model>();
                pls = dl.getPolishDetails();
                return View(pls);

            }
            else
            {
                return RedirectToAction("Login2");

            }

        }

        public ActionResult PolishScrapReport()
        {

            if (Session["username"] != null)

            {
                List<Polishing_Model> polishscrapGroupBy = new List<Polishing_Model>();
                polishscrapGroupBy = dl.getPoishScrapByGroup();

                return View(polishscrapGroupBy);

            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        public ActionResult GetPolishScrapDetailsByRowId(string Id)
        {

            if (Session["username"] != null)
            {
                List<Polishing_Model> polishDetails = new List<Polishing_Model>();
                polishDetails = dl.getPolishDetailsByCoilRowId(Id);


                List<Polishing_Model> PolishScrapByselectCoil = new List<Polishing_Model>();
                PolishScrapByselectCoil = dl.getTotalPolishScrapByCoilId(Id);

                float selectedcoilsPolishScrap = 0;

                foreach (var item in PolishScrapByselectCoil)
                {

                    selectedcoilsPolishScrap = float.Parse(item.PolishScrap);

                }

                List<jobdetail> totalJobScrapInJob = new List<jobdetail>();
                totalJobScrapInJob = dl.getTotalJobScrap();


                List<Polishing_Model> PolishScrap = new List<Polishing_Model>();
                PolishScrap = dl.getTotalPolishScrap();




                float totalScrap = 0;
                float totalJobScrap = 0;
                float totalPolishScrap = 0;

                foreach (var item in totalJobScrapInJob)
                {

                    totalJobScrap = float.Parse(item.Scrap);



                }

                foreach (var item in PolishScrap)
                {

                    totalPolishScrap = float.Parse(item.PolishScrap);



                }



                totalScrap = totalJobScrap + totalPolishScrap;

                //jobdeateils

                //  List<string> alldata = new List<string>();

                IDictionary<string, object> alldata = new Dictionary<string, object>();
                alldata.Add(new KeyValuePair<string, object>("polishDetails", polishDetails));
                alldata.Add(new KeyValuePair<string, object>("totalScrap", totalScrap));
                alldata.Add(new KeyValuePair<string, object>("totalJobScrap", totalJobScrap));
                alldata.Add(new KeyValuePair<string, object>("totalPolishScrap", totalPolishScrap));
                alldata.Add(new KeyValuePair<string, object>("selectedcoilsPolishScrap", selectedcoilsPolishScrap));

                return Json(alldata, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");

            }
        }



        #endregion

        #region manage job scrap report

        public ActionResult JobScrapReport()
        {

            if (Session["username"] != null)

            {
                List<jobdetail> jobscrap = new List<jobdetail>();
                jobscrap = dl.getTotalJobScrapByGroup();

                return View(jobscrap);

            }
            else
            {
                return RedirectToAction("Login2");
            }
        }


        public ActionResult GetJobScrapDetailsByRowId(string Id)
        {

            if (Session["username"] != null)
            {
                List<jobdetail> jobdeateils = new List<jobdetail>();
                jobdeateils = dl.getJobdetailByrowId(Id);


                List<jobdetail> JobScrapByCoil = new List<jobdetail>();
                JobScrapByCoil = dl.getTotalJobScrapByCoil(Id);

                float selectedcoilsJob = 0;

                foreach (var item in JobScrapByCoil)
                {

                    selectedcoilsJob = float.Parse(item.Scrap);



                }



                List<jobdetail> totalJobScrapInJob = new List<jobdetail>();
                totalJobScrapInJob = dl.getTotalJobScrap();


                List<Polishing_Model> PolishScrap = new List<Polishing_Model>();
                PolishScrap = dl.getTotalPolishScrap();


                var rowCount = dl.getPolishDetails().Count();


                float totalScrap = 0;
                float totalJobScrap = 0;
                float totalPolishScrap = 0;

                foreach (var item in totalJobScrapInJob)
                {

                    totalJobScrap = float.Parse(item.Scrap);



                }
                if (rowCount > 0)
                {


                    foreach (var item in PolishScrap)
                    {

                        totalPolishScrap = float.Parse(item.PolishScrap);



                    }

                }

                totalScrap = totalJobScrap + totalPolishScrap;

                //jobdeateils

                //  List<string> alldata = new List<string>();

                IDictionary<string, object> alldata = new Dictionary<string, object>();
                alldata.Add(new KeyValuePair<string, object>("jobdeateils", jobdeateils));
                alldata.Add(new KeyValuePair<string, object>("totalScrap", totalScrap));
                alldata.Add(new KeyValuePair<string, object>("totalJobScrap", totalJobScrap));
                alldata.Add(new KeyValuePair<string, object>("totalPolishScrap", totalPolishScrap));
                alldata.Add(new KeyValuePair<string, object>("selectedcoilsJob", selectedcoilsJob));

                return Json(alldata, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("Login2");

            }
        }




        #endregion


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}