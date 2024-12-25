using System;
using System.Collections.Generic;

namespace net_server.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string? ProductImage { get; set; }

    public virtual ProductsCategory Category { get; set; } = null!;

    public virtual ICollection<Productattribute> Productattributes { get; set; } = new List<Productattribute>();

    public virtual ICollection<Productimage> Productimages { get; set; } = new List<Productimage>();
}
