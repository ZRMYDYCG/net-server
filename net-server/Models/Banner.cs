using System;
using System.Collections.Generic;

namespace net_server.Models;

public partial class Banner
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string ProductImage { get; set; } = null!;
}
