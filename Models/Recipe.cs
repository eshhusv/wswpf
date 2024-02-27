using System;
using System.Collections.Generic;

namespace wswpf.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Dosage { get; set; }

    public string Format { get; set; } = null!;

    public int AppointmentId { get; set; }
}
