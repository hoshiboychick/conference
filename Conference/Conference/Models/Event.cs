using System;
using System.Collections.Generic;

namespace Conference.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Direction { get; set; } = null!;

    public DateTime Date { get; set; }

    public short Days { get; set; }

    public int CityId { get; set; }

    public string Photo { get; set; } = null!;

    public virtual City City { get; set; } = null!;

    public virtual string? ImagePath { get { return System.IO.Path.Combine(Environment.CurrentDirectory, $"images/{Photo}"); } }
}
