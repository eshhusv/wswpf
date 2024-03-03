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
    public partial class PatientEditWindow : Window
    {
        private Reception? reception;
        public PatientEditWindow()
        {
            InitializeComponent();
        }
        public PatientEditWindow(int index, Doctor doctor)
        {
            InitializeComponent();
            DoctorSignature.Text = doctor.DoctorName;
            using (ClinicContext db = new ClinicContext())
            {
                reception = db.Receptions.Where(p => p.AppointmentId == index).FirstOrDefault();
                tbAnamnesis.Text = reception.Anamnesis;
                tbDiagnosis.Text = reception.Diagnosis;
                tbInstrumentalOrLaboratoryTests.Text = reception.InstrumentalOrLaboratoryTests;
                tbProcedures.Text = reception.Procedures;
                tbRecomendations.Text = reception.Recomendations;
                tbReferralForConsultation.Text = reception.ReferralForConsultation;
                tbSymptomsDetails.Text = reception.SymptomsDetails;
                List<Recipe> recipes = db.Recipes.Where(p => p.AppointmentId == reception.AppointmentId).ToList();
                RecipeList.ItemsSource = recipes;
                if (recipes.Count == 10)
                    AddRecipe.Visibility = Visibility.Hidden;
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string? item = RecipeList.SelectedItem.ToString();
            item = item!.Replace("{", "").Trim();
            item = item!.Replace("}", "").Trim();
            string name = item.Split(",")[0].Split("=")[1].Trim();

            RecipeEditWindow recipeEditWindow = new(name);
            if (recipeEditWindow.ShowDialog() == true)
            {

            }
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            RecipeEditWindow recipeEditWindow = new();
            if (recipeEditWindow.ShowDialog() == true)
            {
                using (ClinicContext db = new())
                {
                    Recipe recipe = new Recipe();
                    recipe.Name = recipeEditWindow.NamePrep;
                    recipe.Dosage = double.Parse(recipeEditWindow.Dosage);
                    recipe.Format = recipeEditWindow.Format;
                    recipe.AppointmentId = reception!.AppointmentId;
                    db.Recipes.Add(recipe);
                    db.SaveChanges();
                    RecipeList.ItemsSource = null;
                    List<Recipe> recipes = db.Recipes.Where(p => p.AppointmentId == reception.AppointmentId).ToList();
                    RecipeList.ItemsSource = recipes;
                }
            }
        }

        private void SaveReception_Click(object sender, RoutedEventArgs e)
        {
            using(ClinicContext db = new())
            {
                reception.Anamnesis = tbAnamnesis.Text;
                reception.Recomendations = tbRecomendations.Text;
                reception.Procedures = tbProcedures.Text;
                reception.InstrumentalOrLaboratoryTests = tbInstrumentalOrLaboratoryTests.Text;
                reception.Diagnosis = tbDiagnosis.Text;
                reception.SymptomsDetails = tbSymptomsDetails.Text;
                reception.ReferralForConsultation = tbReferralForConsultation.Text;
                db.Update(reception);
                db.SaveChanges();
            }
            DialogResult = true;
        }
    }
}
