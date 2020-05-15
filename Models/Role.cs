using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogOnSecurityTools.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key, Required]
        public Guid RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
