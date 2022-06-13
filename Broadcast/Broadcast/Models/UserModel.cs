using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Broadcast.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public string Fullname { get; set; }
        public string Firstname { get; set; } 
        public string MiddelName { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; } 
        public string Country { get; set; }
        public string Password { get; set; }
        public string CPassword { get; set; }
        public string OldPassword { get; set; }
        public string CountryCode { get; set; } 

    }
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}