﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppUni.ViewModels.Admin
{
    public class CreateUserVM
    {
        public enum PermissionLevel
        {
            User,
            Employee,
            Admin
        }
        [DisplayName("Username: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Username { get; set; }
        [DisplayName("Password: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Password { get; set; }
        [DisplayName("Confirm password: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string ConfirmPassword { get; set; }
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