using System;
using System.Collections.Generic;

namespace Workspace.Models;

public partial class Member
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Note { get; set; } = null!;

    public sbyte Flag { get; set; }

    public DateTime DateAdded { get; set; }
}
