using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BizConnect.Data.Entities
{

    public class Company
    {
        [Key]
        public virtual int CompanyId { get; set; }

        public virtual Guid OwnerId { get; set; }

        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Phone { get; set; }

        [ForeignKey("OwnerId")]
        public virtual Employee Owner { get; set; }

        public virtual ICollection<Layout> Layouts { get; set; }
       
    }
}
