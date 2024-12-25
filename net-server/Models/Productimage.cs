using System;
using System.Collections.Generic;

namespace net_server.Models;

public partial class Productimage
{
    public int ImageId { get; set; }

    public int ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
