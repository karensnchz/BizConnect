using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizConnect.Data;
using BizConnect.Data.Entities;

namespace BizConnect.WebApp.Models
{
    [Flags]
    public enum OperationType
    {
        Create,
        Edit,
        Delete,
        Details
    }

    public class LayoutModel
    {
        public Layout Layout { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public OperationType OperationType { get; set; }

    }
}