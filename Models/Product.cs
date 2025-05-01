using System;
using System.Collections.Generic;

namespace Workspace.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Satuan { get; set; } = null!;

    public int Harga { get; set; }

    public int Modal { get; set; }

    public string Expired { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public string Note { get; set; } = null!;

    public sbyte Flag { get; set; }

    public DateTime DateAdded { get; set; }
}
