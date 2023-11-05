using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MMABooksEFClasses.MODELS;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual State StateNavigation { get; set; } = null!;

    public override string ToString()
    {
        return CustomerId + ", " + Name + ", " + Address + ", " + City + ", " + State + ", " + ZipCode;
    }
}
