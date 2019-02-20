using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InvoiceApp.Models
{
    public class InvoiceItem
    {
        // Could be implemented without Key (Compound Keys only)
        // but this isn't recommended by docs.microsoft.com
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Key, Column(Order = 1)]
        public Invoice Invoice { get; set; } //PK,FK

        public int InvoiceId { get; set; }

        [Key, Column(Order = 2)]
        public Product Product { get; set; } //PK,FK

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

    }
}