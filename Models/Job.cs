using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        public int rowId { get; set; }
        public string Code { get; set; }


        [Required (ErrorMessage ="please select Job")]

        public string JobName { get; set; }
        public string CoilName { get; set; }

        public string Recovery { get; set; }

        public string Thickness { get; set; }

        public string Diameter { get; set; }

        public string ConsumeWeight { get; set; }
        public string oldWeight { get; set; }
        public string oldWidth { get; set; }
        public string CuttingWeight { get; set; }
        public string Pices { get; set; }
        public string Scrap { get; set; }
        public string slit { get; set; }

        public string SlitWeight { get; set; }
        public string SlitWidth { get; set; }
        public string ScrapWidth { get; set; }
        public string BalanceCoilWeight { get; set; }
        public string remark { get; set; }
        public string AddFrom { get; set; }


    }
     
    public class jobdetail
    {
        public int Id { get; set; }
        public string JobType { get; set; }
        public string Code { get; set; }
        public string CoilName { get; set; }
        public string CuttingWeight { get; set; }

        public string oldWeight { get; set; }
        public int rowId { get; set; }

        public string Recovery { get; set; }
        public string Thickness { get; set; }
       public string Width { get; set; }

        public string Diameter { get; set; }
        public string ConsumeWeight { get; set; }
        public string Pices { get; set; }
        public string Scrap { get; set; }
        public string SlitWeight { get; set; }
        public string SlitWidth { get; set; }
        public string ScrapWidth { get; set; }
        public string slit { get; set; }

        public string BalanceCoilWeight { get; set; }
        public string IssueWeightToPolish { get; set; }
        public string JobWidth { get; set; }
        public string remark { get; set; }
        public string AddFrom { get; set; }

        public string Date { get; set; }
    }

    public class updateRevesrsecoilWeight
    {
        public int rowId { get; set; }
        public string reverseCoilWeight { get; set; }

          
    }
}