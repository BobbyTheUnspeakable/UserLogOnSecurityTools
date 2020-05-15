using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels
{
    public class EditUserViewModel
    {
        public User User { get; set; }
        public string NewPassword { get; set; }
    }
}
