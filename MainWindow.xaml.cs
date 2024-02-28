using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wswpf;
using wswpf.Models;

namespace wswpf
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ClinicContext db = new())
            {
                Doctor? doctor = db.Doctors.Where(d => d.DoctorLogin == login.Text && d.DoctorPassword == password.Password).FirstOrDefault();
                PatientsListWindow patientsListWindows = new();
                patientsListWindows.Show();
            }
        }
    }
}