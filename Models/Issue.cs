using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "please select Item ")]

        public string ddlItem { get; set; }

        [Required(ErrorMessage = " please select Department ")]

        public string ddlDept { get; set; }

        [Required(ErrorMessage = "select Person Name")]

        public string ddlPerson { get; set; }
        [Required(ErrorMessage = "Enter  for Use")]

        public string useFor { get; set; }
        public string chooseItem { get; set; }


        public string quantity { get; set; }
        public string ItemWeight { get; set; }

        [Required(ErrorMessage = "Enter Item serial number")]

        public string serialNo { get; set; }
        [DataType(DataType.Date)]
       // [DisplayFormat( ApplyFormatInEditMode = true,DataFormatString = "{0:yyyy-mm-dd }")]
        public int Item_Id { get; set; }
        public string date { get; set; }


      //  public List<Issue> issuelist { get; set; }

    }
}