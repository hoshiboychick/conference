﻿using System;
using System.Collections.Generic;

namespace Conference.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
