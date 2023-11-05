using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace MMABooksEFClasses.MODELS;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int CustomerId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public decimal ProductTotal { get; set; }

    public decimal SalesTax { get; set; }

    public decimal Shipping { get; set; }

    public decimal InvoiceTotal { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Invoicelineitem> Invoicelineitems { get; set; } = new List<Invoicelineitem>();

    public override string ToString()
    {
        return InvoiceId + ", " + CustomerId + ", " + InvoiceDate + ", " + ProductTotal + ", " + SalesTax + ", " + Shipping + ", " + InvoiceTotal;
    }
}
