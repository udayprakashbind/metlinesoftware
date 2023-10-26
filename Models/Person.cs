using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreMgmt.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
         [Required(ErrorMessage = "Enter Person Name")]

        public string PersonName { get; set; }

        [Required(ErrorMessage = "Enter Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Enter Addresss ")]

        public string Address { get; set; }

        public SelectList prsnlist { get; set; }

    }
}