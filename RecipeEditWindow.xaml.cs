using Microsoft.EntityFrameworkCore;
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
    public partial class RecipeEditWindow : Window
    {
        private Recipe? recipe;
        public RecipeEditWindow()
        {
            InitializeComponent();
        }
        public RecipeEditWindow(Recipe item)
        {
            InitializeComponent();
            tbName.Text = item.Name;
            tbDosage.Text = item.Dosage.ToString();
            tbFormat.Text = item.Format;
            DeleteRecipe.Visibility = Visibility.Visible;
            recipe = item;
        }
        public string NamePrep
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }
        public string Dosage
        {
            get { return tbDosage.Text; }
            set { tbDosage.Text = value; }
        }
        public string Format
        {
            get { return tbFormat.Text; }
            set { tbFormat.Text = value; }
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            using(ClinicContext db = new())
            {
                db.Recipes.Remove(recipe);
                db.SaveChanges();
            }
            DialogResult = true;
        }
    }
}
