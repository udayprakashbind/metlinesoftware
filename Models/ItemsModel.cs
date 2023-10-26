using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreMgmt.Models
{
    public class ItemsModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Item Name")]

        public string ItemName { get; set; }

        [Required(ErrorMessage = "Enter Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string ItemCode { get; set; }

        [Required(ErrorMessage = "Enter Addresss ")]

        public string ItemDetails { get; set; }

        public SelectList itmslist { get; set; }

    }
}