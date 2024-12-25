using System;
using System.Collections.Generic;

namespace net_server.Models;

public partial class ProductsCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
