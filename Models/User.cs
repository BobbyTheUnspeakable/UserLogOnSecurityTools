using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserLogOnSecurityTools.Models
{
    [Table("Users")]
    //public class User : IdentityUser<Guid>
    public class User
    {

        public User()
        {
            //this.Roles = new HashSet<Role>();
        }

        [Key, Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ApplicationId { get; set; }

        [Required, MaxLength(128)]
        //public override string UserName { get; set; }
        public string UserName { get; set; }

        public bool IsAnonymous { get; set; }

        public DateTime LastActivityDate { get; set; }


        //public virtual Application Application { get; set; }
        public virtual Membership Membership { get; set; }
        public virtual ICollection<Role> UserRoles { get; set; }
        public virtual List<UserAction> UserActions { get; set; }
        //public virtual Profile Profile { get; set; }
        
    }
}
