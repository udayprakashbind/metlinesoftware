using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class Item_Model2
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Please Fill The Item Name Field")]  
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Fill The Item Size Field")]

        public string Size { get; set; }
    }
}