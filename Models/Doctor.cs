﻿using System;
using System.Collections.Generic;

namespace wswpf.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string DoctorLogin { get; set; } = null!;

    public string DoctorPassword { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
