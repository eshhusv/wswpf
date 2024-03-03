using System;
using System.Collections.Generic;

namespace wswpf.Models;

public class MedicalCard
{
    public string MedicalCardId { get; set; } = null!;

    public DateOnly DateOfIssue { get; set; }

    public DateOnly? LastAppeal { get; set; }

    public DateOnly? NextVisitDate { get; set; }

    public string? Diagnosis { get; set; }

    public string? MedicalHistory { get; set; }

    public virtual ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
