using System;
using System.Collections.Generic;

namespace CustomerModule.Models;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? EmailId { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
