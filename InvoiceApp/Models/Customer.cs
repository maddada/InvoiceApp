using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceApp.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "#")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must provide a name")]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "You must provide a number")]
        [Required]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{7})$", ErrorMessage =
            "Not a valid mobile number!")]
        public string MobileNumber { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

    }
}