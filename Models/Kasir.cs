using System;
using System.Collections.Generic;

namespace Workspace.Models;

public partial class Kasir
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Kode { get; set; } = null!;

    public sbyte Flag { get; set; }
}
