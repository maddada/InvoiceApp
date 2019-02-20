using InvoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceApp.ViewModels
{
    public class ReportViewModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
        public DateTime? StartDate;
        public DateTime? EndDate;

        public ReportViewModel()
        {
            Invoices = new List<Invoice>();
        }
    }
}