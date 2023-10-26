using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class Circle_model
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage ="Please enter Code")]
        [RegularExpression("^[^<>'.!@#$%*\"]+$", ErrorMessage = "Special Character Don't allowance.")]

        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter Circle Name")]

        public string CircleName { get; set; }
        [Required(ErrorMessage = "Please enter Thickness")]

        public string Thickness { get; set; }

        [Required(ErrorMessage = "Please enter Width")]


        public string Width { get; set; }

        [Required(ErrorMessage = "Please enter Weight")]

        public string Weight { get; set; }
        public string remark { get; set; }
        public string AddFrom { get; set; }


         public string Date { get; set; }

    }
}