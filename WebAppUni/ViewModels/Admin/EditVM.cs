using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppUni.ViewModels.Admin
{
    public class EditVM
    {
        public enum PermissionLevel
        {
            User,
            Employee,
            Admin
        }
        public int Id { get; set; }
        [DisplayName("Username: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Username { get; set; }
        [DisplayName("Password: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Password { get; set; }
        [DisplayName("First Name: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string FirstName { get; set; }
        [DisplayName("Last Name: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string LastName { get; set; }
        [DisplayName("Role: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public PermissionLevel Role { get; set; }
    }
}