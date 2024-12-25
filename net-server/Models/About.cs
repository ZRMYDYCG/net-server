using System;
using System.Collections.Generic;

namespace net_server.Models;

public partial class About
{
    public int AboutId { get; set; }

    public string? CompanyIntroduction { get; set; }

    public int? ProductCount { get; set; }

    public int? EmployeeCount { get; set; }

    public int? WorkshopCount { get; set; }
}
