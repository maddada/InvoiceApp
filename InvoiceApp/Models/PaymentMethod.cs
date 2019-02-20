using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceApp.Models
{
    // Enum for Payment Methods!
    public enum PaymentMethods : byte
    {
        Credit = 0,
        Cash = 1
    }
}