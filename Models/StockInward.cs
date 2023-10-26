using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreMgmt.Models
{
    public class StockInward
    {
        [Key]
        public int Id { get; set; }
        public int Vend_Id { get; set; }
        public int Item_Id { get; set; }
        public int Person_Id { get; set; }


     //  [Required(ErrorMessage = " please select Vendor ")]

        public string ddlVendor { get; set; }


     //  [Required(ErrorMessage = "please select Item ")]

        public string ddlItem { get; set; }
     //  [Required(ErrorMessage = "select Person Name")]

        public string ddlPerson { get; set; }


        public string Quantity { get; set; }
        public string ItemWeight { get; set; }
        public string chooseItem { get; set; }
        public string Rate { get; set; }
        public string Remark { get; set; }

        public string Date { get; set; }



    }

    public class StockInwardAdd
    {
        [Key]
        public int Id { get; set; }
        public int Vend_Id { get; set; }
        public int Item_Id { get; set; }
        public int Person_Id { get; set; }


          [Required(ErrorMessage = " please select Vendor ")]

        public string ddlVendor { get; set; }


          [Required(ErrorMessage = "please select Item ")]

        public string ddlItem { get; set; }
         [Required(ErrorMessage = "select Person Name")]

        public string ddlPerson { get; set; }


        public string Quantity { get; set; }
        public string ItemWeight { get; set; }

        [Required(ErrorMessage = "please Choose Item Type")]

        public string chooseItem { get; set; }
        public string Rate { get; set; }
        public string Remark { get; set; }

        public string Date { get; set; }



    }


}