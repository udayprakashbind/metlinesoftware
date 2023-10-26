using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreMgmt.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Vendor Name")]

        public string VendorName { get; set; }
        [Required(ErrorMessage = "Enter Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string VendorContact { get; set; }

        [Required(ErrorMessage = "Enter Addresss ")]

        public string VendorAddress { get; set; }

    }
}