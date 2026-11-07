using System;
using System.Collections.Generic;

namespace _4GYM.Models;

public partial class Instructor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhotoUrl { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
