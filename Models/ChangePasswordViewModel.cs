using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class ChangePasswordViewModel
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Please enter new password.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Compare("ConfirmNewPassword")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
