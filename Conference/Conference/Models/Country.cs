using System;
using System.Collections.Generic;

namespace Conference.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string EnglishName { get; set; } = null!;

    public string Code { get; set; } = null!;

    public short NumCode { get; set; }

}
