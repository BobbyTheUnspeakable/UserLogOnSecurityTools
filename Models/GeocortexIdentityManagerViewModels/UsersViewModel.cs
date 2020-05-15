using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogOnSecurityTools.Models.GeocortexIdentityManagerViewModels
{
    public class UsersViewModel
    {

        public List<ListItem> Users { get; set; }

        public class ListItem
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string LogOnDate { get; set; }
        }
    }
}
