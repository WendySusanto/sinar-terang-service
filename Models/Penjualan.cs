using System;
using System.Collections.Generic;

namespace Workspace.Models;

public partial class Penjualan
{
    public int Id { get; set; }

    public int KasirId { get; set; }

    public int? MemberId { get; set; }

    public DateTime DateAdded { get; set; }

    public int Total { get; set; }

    public sbyte Flag { get; set; }
}
