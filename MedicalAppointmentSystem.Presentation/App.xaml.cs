using MedicalAppointmentSystem.Application.Services;
using MedicalAppointmentSystem.Infrastructure.Data;
using MedicalAppointmentSystem.Infrastructure.Repositories;
using MedicalAppointmentSystem.Infrastructure.Seed;
using MedicalAppointmentSystem.Presentation.Services;
using MedicalAppointmentSystem.Presentation.ViewModels;
using MedicalAppointmentSystem.Presentation.Views;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace MedicalAppointmentSystem.Presentation
{
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var options = new DbContextOptionsBuilder<MedicalDbContext>()
                    .UseSqlite("Data Source=medical.db")
                    .Options;

                var dbContext = new MedicalDbContext(options);
                dbContext.Database.EnsureCreated();
                DbSeeder.Seed(dbContext);

                var userRepo = new UserRepository(dbContext);
                var authService = new AuthenticationService(userRepo);

                var appointmentRepo = new AppointmentRepository(dbContext);
                var appointmentService = new AppointmentService(appointmentRepo);

                var doctorRepo = new DoctorRepository(dbContext);
                var patientRepo = new PatientRepository(dbContext);

                // Setup Navigation
                MainViewModel mainViewModel = new MainViewModel(null!);
                
                // We need a way to resolve the factory later because of circular dependency
                Func<Type, BaseViewModel> viewModelFactory = null!;

                var navigationService = new NavigationService(mainViewModel, type => viewModelFactory(type));

                viewModelFactory = type =>
                {
                    if (type == typeof(LoginViewModel))
                    {
                        return new LoginViewModel(authService, navigationService);
                    }
                    if (type == typeof(AdminDashboardViewModel))
                    {
                        return new AdminDashboardViewModel(appointmentService, doctorRepo, patientRepo, navigationService);
                    }
                    if (type == typeof(DoctorDashboardViewModel))
                    {
                        return new DoctorDashboardViewModel(appointmentService, navigationService);
                    }
                    if (type == typeof(PatientDashboardViewModel))
                    {
                        return new PatientDashboardViewModel(appointmentService, navigationService);
                    }
                    throw new ArgumentException($"Unknown ViewModel: {type}");
                };

                // Set initial view
                navigationService.NavigateTo<LoginViewModel>();

                // Show Window
                var mainWindow = new MainWindow(mainViewModel);
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during startup: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"An unhandled exception occurred: {e.Exception.Message}\n\nStack Trace:\n{e.Exception.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
