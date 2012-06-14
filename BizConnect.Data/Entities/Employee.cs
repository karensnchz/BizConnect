using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BizConnect.Data.Entities
{
    public class Employee
    {
        [Key, ForeignKey("Account")]
        public virtual Guid EmployeeId { get; set; }

        [Display(Name = "Address")]
        public virtual string Address { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public virtual string Phone { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual User Account { get; set; }
    }
}