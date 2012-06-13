using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace BizConnect.Data.Entities
{
    public class Layout
    {
        [Key]
        public virtual int LayoutId { get; set; }
        public virtual string Name { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Element> Elements { get; set; }

    }
}