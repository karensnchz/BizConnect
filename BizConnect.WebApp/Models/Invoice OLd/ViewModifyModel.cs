using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BizConnect.WebApp.Models.Invoice
{
    public class ViewModifyModel
    {
        [Required]
        [Display(Name = "InvoiceId")]
        public int InvoiceId { get; set; }

        [Required]
        [Display(Name = "ClientId")]
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "Elements")]
        public string Elements { get; set; }

        
 
    }
}