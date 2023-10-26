using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class selloutward_model
    {
        [Key]
        public int id { get; set; }
        public int rowid { get; set; }
        public string code { get; set; }
        public string weight { get; set; }
        public string itemtype { get; set; }

        public string partyname { get; set; }

        public string thickness { get; set; }
        public string quantity { get; set; }
        public string remarks { get; set; }
        public string size { get; set; }
        public string addfrom { get; set; }
        public string oldweight { get; set; }


        public string date { get; set; }
    }
    public class selloutupdateweight
    {
        public int rowid { get; set; }
        public string addfrom { get; set; }
        public string itemtype { get; set; }
        public string updateweight { get; set; }
    }
}