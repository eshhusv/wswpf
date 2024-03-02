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
        private Doctor doctor;
        public PatientsListWindow(Doctor doctor)
        {
            InitializeComponent();
            this.doctor = doctor;
        }
        private void LoadUpdate()
        {
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
                }).Where(p => p.DoctorId == doctor.DoctorId);
                PatientsList.ItemsSource = listReception.ToList();
            }
        }

        private void FindByFullName_Click(object sender, RoutedEventArgs e)
        {
            using(ClinicContext db = new())
            {
                Patient p = db.Patients.Where(p => p.Surname + " " + p.Firstname + " " + p.Lastname == PatientFullName_TextBox.Text).FirstOrDefault()!;
                var list = db.Receptions.Select(l => new
                {
                    AppointmentId = l.AppointmentId,
                    DoctorId = l.DoctorId,
                    PatientId = l.PatientId,
                    Anamnesis = l.Anamnesis,
                    SymptomsDetails = l.SymptomsDetails,
                    Diagnosis = l.Diagnosis,
                    Recomendations = l.Recomendations,
                    ReferralForConsultation = l.ReferralForConsultation,
                    InstrumentalOrLaboratoryTests = l.InstrumentalOrLaboratoryTests,
                    Procedures = l.Procedures
                }).Where(c=>c.PatientId==p.PatientId&&c.DoctorId==doctor.DoctorId);
                PatientsList.ItemsSource = list.ToList();
            }
        }

        private void FindByQR_Click(object sender, RoutedEventArgs e)
        {
            PatientFindWindow patientFindWindow = new();
            if (patientFindWindow.ShowDialog() == true)
            {
                string medicalCard = patientFindWindow.MedicalCard;
                using (ClinicContext db = new())
                {
                    Patient p = db.Patients.Where(p => p.MedicalCard==medicalCard).FirstOrDefault()!;
                    var list = db.Receptions.Select(l => new
                    {
                        AppointmentId = l.AppointmentId,
                        DoctorId = l.DoctorId,
                        PatientId = l.PatientId,
                        Anamnesis = l.Anamnesis,
                        SymptomsDetails = l.SymptomsDetails,
                        Diagnosis = l.Diagnosis,
                        Recomendations = l.Recomendations,
                        ReferralForConsultation = l.ReferralForConsultation,
                        InstrumentalOrLaboratoryTests = l.InstrumentalOrLaboratoryTests,
                        Procedures = l.Procedures
                    }).Where(c => c.PatientId == p.PatientId && c.DoctorId == doctor.DoctorId);
                    PatientsList.ItemsSource = list.ToList();
                }
            };
        }

        private void PatientsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string? item = PatientsList.SelectedItem.ToString();
            item=item!.Replace("{", "").Trim();
            item = item!.Replace("}", "").Trim();
            int index = int.Parse(item.Split(",")[0].Split("=")[1].Trim());
            PatientEditWindow window = new PatientEditWindow(index); 
            int i = 0;
        }
    }
 }

