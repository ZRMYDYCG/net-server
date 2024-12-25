using System;
using System.Collections.Generic;

namespace net_server.Models;

public partial class Jobdetail
{
    public int JobId { get; set; }

    public string JobTitle { get; set; } = null!;

    public int NumberOfOpenings { get; set; }

    public string Salary { get; set; } = null!;

    public string Qualifications { get; set; } = null!;

    public string Responsibilities { get; set; } = null!;
}
