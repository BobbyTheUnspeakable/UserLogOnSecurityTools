using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels
{
    public class PasswordResetViewModel
    {
        public User User { get; set; }
        public Guid ActionId { get; set; }

        [Required]
        [Display(Name = "Security Code")]
        public string SecurityCode { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(20, ErrorMessage = "Must be at least 8 characters", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[A-Za-z\d\W]{8,}$",
            ErrorMessage = "Password must have at least 1 upper case letter, 1 lower case letter, 1 number and 1 special character!")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Confirm New Password")]
        public string NewPasswordConfirm { get; set; }
    }
}