using System;
using System.Collections.Generic;

namespace wswpf.Models;

public partial class Reception
{
    public int PatientId { get; set; }

    public string Anamnesis { get; set; } = null!;

    public string SymptomsDetails { get; set; } = null!;

    public string Diagnosis { get; set; } = null!;

    public string Recomendations { get; set; } = null!;

    public int DoctorId { get; set; }

    public int AppointmentId { get; set; }

    public string ReferralForConsultation { get; set; } = null!;

    public string InstrumentalOrLaboratoryTests { get; set; } = null!;

    public string Procedures { get; set; } = null!;
}
