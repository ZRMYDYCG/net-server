using System;
using System.Collections.Generic;

namespace net_server.Models;

public partial class New
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Content { get; set; } = null!;

    public string? Cover { get; set; }
}
