using System;
using System.Collections.Generic;

namespace Workspace.Models;

public partial class Utang
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int Jumlah { get; set; }

    public DateTime DateAdded { get; set; }

    public string Note { get; set; } = null!;

    public sbyte Flag { get; set; }
}
