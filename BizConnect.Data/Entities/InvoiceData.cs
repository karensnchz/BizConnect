using System.ComponentModel.DataAnnotations;

namespace BizConnect.Data.Entities
{
    public class InvoiceData
    {
        [Key, Column(Order = 1)]
        public virtual int ElementId { get; set; }
        [Key, Column(Order = 2)]
        public virtual int InvoiceId { get; set; }
        public virtual string Data { get; set; }

        [ForeignKey("ElementId")]
        public virtual Element Element { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
    }
}