using System;
using System.Collections.Generic;

namespace Workspace.Models;

public partial class SalesDTO
{
    public int Id { get; set; }

    public int KasirId { get; set; }

    public string KasirName { get; set; }

    public int? MemberId { get; set; }

    public string MemberName { get; set; }

    public DateTime DateAdded { get; set; }

    public int Total { get; set; }

    public sbyte Flag { get; set; }
}
