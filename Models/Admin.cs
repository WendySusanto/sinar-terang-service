using System;
using System.Collections.Generic;

namespace Workspace.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime DateAdded { get; set; }

    public DateTime? LastLogin { get; set; }
}
