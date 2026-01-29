using System.Windows;
using MedicalAppointmentSystem.Presentation.ViewModels;

namespace MedicalAppointmentSystem.Presentation
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
