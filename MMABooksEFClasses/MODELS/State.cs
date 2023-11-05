using MMABooksEFClasses.MarisModels;
using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.MODELS;

public partial class State
{
    public string StateCode { get; set; } = null!;

    public string StateName { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public override string ToString()
    {
        return StateCode + ", " + StateName;
    }
}
