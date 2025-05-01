using System;
using System.Collections.Generic;

namespace Workspace.Models;

public partial class HargaMember
{
    public int ProductId { get; set; }

    public int MemberId { get; set; }

    public int Harga { get; set; }
}
