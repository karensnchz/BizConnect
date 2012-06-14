using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BizConnect.Data.Entities
{
    public class Invoice
    {
        [Key]
        public virtual int InvoiceId { get; set; }
        [Required]
        public virtual int LayoutId { get; set; }

        [ForeignKey("LayoutId")]
        public virtual Layout Layout { get; set; }
        public virtual ICollection<InvoiceData> Data { get; set; }
    }
}