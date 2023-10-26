using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class ChangePassword
    {
        public int Id { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string newPaswd { get; set; }
        public string confPaswd { get; set; }
    }
}