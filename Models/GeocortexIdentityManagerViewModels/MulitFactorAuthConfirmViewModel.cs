using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels
{
    public class MultiFactorAuthConfirmViewModel
    {
        public User User { get; set; }
        public Guid ActionId { get; set; }

        [Required]
        [Display(Name = "Security Code")]
        public string SecurityCode { get; set; }

        public string Passthrough { get; set; }
    }
}