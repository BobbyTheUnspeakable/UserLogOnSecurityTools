using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogOnSecurityTools.Models
{
    [Table("UserRole")]
    public class UserRole
    {
        //[Key, Required]
        //public int Id { get; set; }

        [Required, ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required, ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
