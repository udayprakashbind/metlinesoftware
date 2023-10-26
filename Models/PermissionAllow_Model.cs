using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreMgmt.Models
{
    public class PermissionAllow_Model
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
        public string RoleName { get; set; }

    }
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }
        public int[] Permission_Id { get; set; }
        public int Perm_Id { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
        public int Role_Id { get; set; }
        public string RoleName { get; set; }

    }
}