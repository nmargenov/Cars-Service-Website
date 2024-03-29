using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppUni.ViewModels.MyProfile
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
        public string passwordCheck { get; set; }

        [DisplayName("Username: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Username { get; set; }


        [DisplayName("Password: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string OldPassword { get; set; }


        [DisplayName("New Password: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string NewPassword { get; set; }


        [DisplayName("Confirm Password: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string ConfirmNewPassword { get; set; }


        [DisplayName("First Name: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string FirstName { get; set; }


        [DisplayName("Last Name: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string LastName { get; set; }
        public PermissionLevel Role { get; set; }
    }
}