using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InvoiceApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is Required!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime? Date { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public PaymentMethods PaymentMethod { get; set; } // FK

        public decimal Amount { get; set; }

        public Customer Customer { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }

        public IEnumerable<InvoiceItem> InvoiceItems { get; set; }


    }


}