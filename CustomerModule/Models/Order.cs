using System;
using System.Collections.Generic;

namespace CustomerModule.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public Guid? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }
}
