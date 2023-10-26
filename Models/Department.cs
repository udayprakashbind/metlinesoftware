using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace StoreMgmt.Models
{
    public class Department
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Department Name")]
        public string DepartmentName{ get; set; }

    }
}