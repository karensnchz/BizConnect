using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace BizConnect.Data.Entities
{
    public class Element
    {
        [Key]
        public virtual int ElementId { get; set; }

        [ScriptIgnore, IgnoreDataMember]
        public virtual Layout Layout { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual int XPosition { get; set; }

        [Required]
        public virtual int YPosition { get; set; }

        [Required]
        public virtual int Width { get; set; }

        [Required]
        public virtual int Height { get; set; }

        [Required]
        public virtual string ElementType { get; set; }

        public virtual int? Rows { get; set; }
        public virtual int? Columns { get; set; }

    }

    //public enum ElementType
    //{
    //    Text, Phone, Email, Date, Currency, Number, Boolean, Table
    //}
}