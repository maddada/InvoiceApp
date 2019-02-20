
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InvoiceApp.Models;

namespace InvoiceApp.ViewModels
{
    public class InvoicesViewModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }

        public InvoicesViewModel()
        {
            Invoice = new Invoice();
        }
    }
}