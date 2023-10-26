using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class Role_model
    {
            [Key]
            public int Id { get; set; }
            public string RoleName { get; set; }
        
    }
}