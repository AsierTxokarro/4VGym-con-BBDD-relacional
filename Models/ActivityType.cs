using System;
using System.Collections.Generic;

namespace _4GYM.Models;

public partial class ActivityType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int NumberMonitors { get; set; }
}
