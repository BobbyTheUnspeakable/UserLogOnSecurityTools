﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels
{
    public class PasswordResetRequestViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Comments { get; set; }
    }
}
