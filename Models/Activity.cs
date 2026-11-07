using System;
using System.Collections.Generic;

namespace _4GYM.Models;

public partial class Activity
{
    public int Id { get; set; }

    public int? ActivityTypeId { get; set; }

    public string InstructorsId { get; set; } = null!;

    public int MaxParticipants { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }
}
