using System;
using System.Collections.Generic;

namespace net_server.Models;

public partial class Productattribute
{
    public int AttributeId { get; set; }

    public int ProductId { get; set; }

    public string AttributeName { get; set; } = null!;

    public string AttributeValue { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
