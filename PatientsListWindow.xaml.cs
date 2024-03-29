﻿using System;
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
    public partial class PatientsListWindow : Window
    {
        private Doctor doctor;
        public PatientsListWindow(Doctor doctor)
        {
            InitializeComponent();
            this.doctor = doctor;
            LoadUpdate();
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
            using (ClinicContext db = new())
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
                }).Where(c => c.PatientId == p.PatientId && c.DoctorId == doctor.DoctorId);
                PatientsList.ItemsSource = list.ToList();
            }
        }

        private void FindByQR_Click(object sender, RoutedEventArgs e)
        {
            using (ClinicContext db = new())
            {
                Patient p = db.Patients.Where(p => p.MedicalCard == PatientMedicalCard_TextBox.Text).FirstOrDefault()!;
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
        }

        private void PatientsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string? item = PatientsList.SelectedItem.ToString();
            item = item!.Replace("{", "").Trim();
            item = item!.Replace("}", "").Trim();

            string[] mas = item.Split(",");
            int index = -1;
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i].StartsWith(" AppointmentId")) index = i;
            }
            index = int.Parse(mas[index].Split("=")[1]);
            PatientEditWindow window = new PatientEditWindow(index, doctor);
            if (window.ShowDialog() == true)
            {
                LoadUpdate();
            }
        }
    }
}

