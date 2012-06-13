using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BizConnect.Data.Entities
{
    public class Role
    {
        [Key]
        public virtual Guid RoleId { get; set; }

        [Required]
        public virtual string RoleName { get; set; }

        public virtual string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}