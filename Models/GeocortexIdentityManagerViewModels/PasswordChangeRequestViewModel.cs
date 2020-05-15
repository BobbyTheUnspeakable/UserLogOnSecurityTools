using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels
{
    public class PasswordChangeRequestViewModel
    {
        public Guid UserId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}
