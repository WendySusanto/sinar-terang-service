using System;
using System.Collections.Generic;

namespace Workspace.Models;

public partial class ProductToPenjualan
{
    public int PenjualanId { get; set; }

    public int ProductId { get; set; }

    public int Harga { get; set; }

    public int Quantity { get; set; }
}
