using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserLogOnSecurityTools.Models
{
    [Table("UserActions")]
    public class UserAction
    {
        [Key, Required]
        public Guid ActionId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public string ActionType { get; set; }

        public DateTime ActionDate { get; set; }

        public string Comments { get; set; }

        public string SecurityCode { get; set; }

        public string ApplicationUrl { get; set; }

        public virtual User User { get; set; }
    }
}
