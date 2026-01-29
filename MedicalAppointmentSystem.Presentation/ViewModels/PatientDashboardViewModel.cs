using MedicalAppointmentSystem.Application.Services;
using MedicalAppointmentSystem.Domain.Entities;
using MedicalAppointmentSystem.Presentation.Commands;
using MedicalAppointmentSystem.Presentation.Services;
using System.Collections.ObjectModel;

namespace MedicalAppointmentSystem.Presentation.ViewModels
{
    public class PatientDashboardViewModel : BaseViewModel
    {
        private readonly AppointmentService _appointmentService;
        private readonly INavigationService _navigationService;

        public string WelcomeMessage { get; }

        public ObservableCollection<Appointment> MyAppointments { get; } = new ObservableCollection<Appointment>();

        private Appointment? _selectedAppointment;
        public Appointment? SelectedAppointment
        {
            get => _selectedAppointment;
            set 
            { 
                _selectedAppointment = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedConsultationDiagnosis));
                OnPropertyChanged(nameof(SelectedConsultationTreatment));
                OnPropertyChanged(nameof(HasConsultation));
            }
        }

        public string SelectedConsultationDiagnosis => SelectedAppointment?.Consultation?.Diagnosis ?? "No consultation yet";
        public string SelectedConsultationTreatment => SelectedAppointment?.Consultation?.Treatment ?? "";
        public bool HasConsultation => SelectedAppointment?.Consultation != null;

        public RelayCommand LogoutCommand { get; }

        public PatientDashboardViewModel(
            AppointmentService appointmentService,
            INavigationService navigationService)
        {
            _appointmentService = appointmentService;
            _navigationService = navigationService;

            var user = Application.Singleton.UserSession.Instance.CurrentUser as PatientUser;
            WelcomeMessage = user != null ? $"Welcome, {user.PatientProfile.FullName}!" : "Welcome, Patient!";

            _appointmentService.AppointmentAdded += LoadAppointments;
            _appointmentService.ConsultationAdded += LoadAppointments;

            LogoutCommand = new RelayCommand(Logout);

            LoadAppointments();
        }

        private void LoadAppointments()
        {
            var user = Application.Singleton.UserSession.Instance.CurrentUser as PatientUser;
            if (user != null)
            {
                var appointments = _appointmentService.GetAppointmentsForPatient(user.PatientProfile.Id);
                MyAppointments.Clear();
                foreach (var app in appointments)
                {
                    MyAppointments.Add(app);
                }
            }
        }

        private void Logout()
        {
            Application.Singleton.UserSession.Instance.Logout();
            _navigationService.NavigateTo<LoginViewModel>();
        }
    }
}
