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
        public RecipeEditWindow(string name)
        {
            using(ClinicContext db = new())
            {
                recipe = db.Recipes.Where(r=> r.Name == name).FirstOrDefault();
                tbName.Text = recipe.Name;
                tbDosage.Text = recipe.Dosage.ToString();
                tbFormat.Text = recipe.Format;
            }
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
    }
}
