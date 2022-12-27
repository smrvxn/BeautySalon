using System;
using System.Collections.Generic;

namespace BeautySaloon.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
