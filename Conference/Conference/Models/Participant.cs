using System;
using System.Collections.Generic;

namespace Conference.Models;

public partial class Participant
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public int CountryId { get; set; }

    public string Phone { get; set; } = null!;

    public string? Direction { get; set; }

    public string? Event { get; set; }

    public string Password { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
