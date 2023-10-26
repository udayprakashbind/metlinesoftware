using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace StoreMgmt.Models
{
    public class Coil_model
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Code  Required")]
        [RegularExpression("^[^<>'.!@#$%*\"]+$", ErrorMessage = "Special Character Don't allowance.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Coil Name  Required")]
        public string CoilName { get; set; }

        [Required(ErrorMessage = "Thickness  Required")]
        public string Thickness { get; set; }

        public string Width { get; set; }

        [Required(ErrorMessage = "Weight  Required")]
        public string Weight { get; set; }
        public string AvailableWeight { get; set; }
        public string InitialWeight { get; set; }
        public string remark { get; set; }
        public string AddFrom { get; set; }
        public string Date { get; set; }


    }
    public class coilWeight
    {
        public int Id { get; set; }
        public string newCoilWeight { get; set; }
        public string newCoilWidth { get; set; }

    }
}