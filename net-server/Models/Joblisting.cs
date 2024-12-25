using System;
using System.Collections.Generic;

namespace net_server.Models;

public partial class Joblisting
{
    public int JobId { get; set; }

    public string JobTitle { get; set; } = null!;

    public string? Department { get; set; }

    public int? NumberOfOpenings { get; set; }
}
