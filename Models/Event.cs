using System;
using System.Collections.Generic;

namespace wswpf.Models;

public class Event
{
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public DateOnly EventDate { get; set; }

    public string EventType { get; set; } = null!;

    public string EventResult { get; set; } = null!;

    public string EventRecommendations { get; set; } = null!;

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
