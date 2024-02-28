using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wswpf.Models;

namespace wswpf
{
    /// <summary>
    /// Логика взаимодействия для PatientsListWindow.xaml
    /// </summary>
    public partial class PatientsListWindow : Window
    {
        public PatientsListWindow()
        {
            InitializeComponent();
            using (ClinicContext db = new ClinicContext())
            {
                var listReception = db.Receptions.Select(p => new
                {
                    Anamnesis = p.Anamnesis,
                    FIODoctor = db.Doctors.Where(d => d.DoctorId == p.DoctorId).FirstOrDefault()!.DoctorName,
                    DoctorId = p.DoctorId,
                    PatientId = p.PatientId,
                    FIOPatient = db.Patients.Where(pat => pat.PatientId == p.PatientId).FirstOrDefault()!.Firstname,
                    SymptomsDetails = p.SymptomsDetails,
                    Diagnosis = p.Diagnosis,
                    Recomendations = p.Recomendations,
                    AppointmentId = p.AppointmentId,
                    ReferralForConsultation = p.ReferralForConsultation,
                    InstrumentalOrLaboratoryTests = p.InstrumentalOrLaboratoryTests,
                    Procedures = p.Procedures
                });

                PatientsList.ItemsSource = listReception.ToList();
            }
        }
    }
 }

