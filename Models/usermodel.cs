using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class usermodel
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; }
        public string mobile { get; set; }
        public string department { get; set; }
        public int deptId { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public string password { get; set; }
       

    }

}