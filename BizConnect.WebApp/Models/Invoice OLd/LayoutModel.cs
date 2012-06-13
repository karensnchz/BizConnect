using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace BizConnect.WebApp.Models.Invoice
{
    public class LayoutModel
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Elements")]
        public string Elements { get; set; }

        //[Required]
        //[Display(Name = "Company_ID")]
        //public int Company_Id { get; set; }

        [Required]
        [Display(Name = "Layout Name")]
        public string Name { get; set; }
    }
}