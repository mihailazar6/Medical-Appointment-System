using MedicalAppointmentSystem.Application.Interfaces;
using MedicalAppointmentSystem.Application.Services;
using MedicalAppointmentSystem.Domain.Entities;
using MedicalAppointmentSystem.Presentation.Commands;
using MedicalAppointmentSystem.Presentation.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MedicalAppointmentSystem.Presentation.ViewModels
{
    public class DoctorDashboardViewModel : BaseViewModel
    {
        private readonly AppointmentService _appointmentService;
        private readonly INavigationService _navigationService;

        public string WelcomeMessage { get; }

        public ObservableCollection<Appointment> Appointments { get; } = new ObservableCollection<Appointment>();

        private Appointment? _selectedAppointment;
        public Appointment? SelectedAppointment
        {
            get => _selectedAppointment;
            set { _selectedAppointment = value; OnPropertyChanged(); }
        }

        private string _diagnosis = string.Empty;
        public string Diagnosis
        {
            get => _diagnosis;
            set { _diagnosis = value; OnPropertyChanged(); }
        }

        private string _treatment = string.Empty;
        public string Treatment
        {
            get => _treatment;
            set { _treatment = value; OnPropertyChanged(); }
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public RelayCommand AddConsultationCommand { get; }
        public RelayCommand LogoutCommand { get; }

        public DoctorDashboardViewModel(
            AppointmentService appointmentService,
            INavigationService navigationService)
        {
            _appointmentService = appointmentService;
            _navigationService = navigationService;

            var user = Application.Singleton.UserSession.Instance.CurrentUser as DoctorUser;
            WelcomeMessage = user != null ? $"Welcome, Dr. {user.DoctorProfile.FullName}!" : "Welcome, Doctor!";

            _appointmentService.AppointmentAdded += LoadAppointments;

            AddConsultationCommand = new RelayCommand(AddConsultation);
            LogoutCommand = new RelayCommand(Logout);

            LoadAppointments();
        }

        private void LoadAppointments()
        {
            var user = Application.Singleton.UserSession.Instance.CurrentUser as DoctorUser;
            if (user != null)
            {
                var appointments = _appointmentService.GetAppointmentsForDoctor(user.DoctorProfile.Id);
                Appointments.Clear();
                foreach (var app in appointments)
                {
                    Appointments.Add(app);
                }
            }
        }

        private void AddConsultation()
        {
            if (SelectedAppointment == null)
            {
                StatusMessage = "Please select an appointment first.";
                return;
            }

            if (SelectedAppointment.Consultation != null)
            {
                StatusMessage = "This appointment already has a consultation.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Diagnosis) || string.IsNullOrWhiteSpace(Treatment))
            {
                StatusMessage = "Please enter both diagnosis and treatment.";
                return;
            }

            _appointmentService.AddConsultation(SelectedAppointment.Id, Diagnosis, Treatment);
            
            StatusMessage = $"Consultation added for {SelectedAppointment.Patient.FullName}";
            Diagnosis = string.Empty;
            Treatment = string.Empty;

            LoadAppointments();
        }

        private void Logout()
        {
            Application.Singleton.UserSession.Instance.Logout();
            _navigationService.NavigateTo<LoginViewModel>();
        }
    }
}
