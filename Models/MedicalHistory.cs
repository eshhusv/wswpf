using System;
using System.Collections.Generic;

namespace wswpf.Models;

public class MedicalHistory
{
    public int MedicalHistoryId { get; set; }

    public DateOnly MedicalHistoryDate { get; set; }

    public string PatientMedicalCard { get; set; } = null!;

    public bool? IsVisited { get; set; }

    public virtual MedicalCard PatientMedicalCardNavigation { get; set; } = null!;
}
