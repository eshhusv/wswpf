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
    /// Логика взаимодействия для PatientEditWindow.xaml
    /// </summary>
    public partial class PatientEditWindow : Window
    {
        public PatientEditWindow(int index)
        {
            InitializeComponent();
            using (ClinicContext db=new ClinicContext())
            {
                Reception? reception = db.Receptions.Where(p => p.AppointmentId == index).FirstOrDefault();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
