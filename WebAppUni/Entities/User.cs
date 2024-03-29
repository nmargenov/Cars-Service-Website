using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppUni.Entities
{
    public class User
    {
        public enum PermissionLevel
        {
            User,
            Employee,
            Admin
        }

        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PermissionLevel Role { get; set; }


            
    }
}