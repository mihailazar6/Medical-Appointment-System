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
    public class AdminDashboardViewModel : BaseViewModel
    {
        private readonly AppointmentService _appointmentService;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly INavigationService _navigationService;

        public string WelcomeMessage => "Welcome, Admin!";

        public ObservableCollection<Appointment> Appointments { get; } = new ObservableCollection<Appointment>();
        public ObservableCollection<Doctor> Doctors { get; } = new ObservableCollection<Doctor>();
        public ObservableCollection<Patient> Patients { get; } = new ObservableCollection<Patient>();

        private Doctor? _selectedDoctor;
        public Doctor? SelectedDoctor
        {
            get => _selectedDoctor;
            set { _selectedDoctor = value; OnPropertyChanged(); }
        }

        private Patient? _selectedPatient;
        public Patient? SelectedPatient
        {
            get => _selectedPatient;
            set { _selectedPatient = value; OnPropertyChanged(); }
        }

        private DateTime _selectedDate = DateTime.Now.AddDays(1);
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(); }
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public RelayCommand CreateAppointmentCommand { get; }
        public RelayCommand LogoutCommand { get; }

        public AdminDashboardViewModel(
            AppointmentService appointmentService,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            INavigationService navigationService)
        {
            _appointmentService = appointmentService;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _navigationService = navigationService;

            _appointmentService.AppointmentAdded += LoadAppointments;

            CreateAppointmentCommand = new RelayCommand(CreateAppointment);
            LogoutCommand = new RelayCommand(Logout);

            LoadData();
        }

        private void LoadData()
        {
            LoadAppointments();
            LoadDoctors();
            LoadPatients();
        }

        private void LoadAppointments()
        {
            var appointments = _appointmentService.GetAllAppointments();
            Appointments.Clear();
            foreach (var app in appointments)
            {
                Appointments.Add(app);
            }
        }

        private void LoadDoctors()
        {
            var doctors = _doctorRepository.GetAll();
            Doctors.Clear();
            foreach (var doc in doctors)
            {
                Doctors.Add(doc);
            }
            if (Doctors.Any()) SelectedDoctor = Doctors.First();
        }

        private void LoadPatients()
        {
            var patients = _patientRepository.GetAll();
            Patients.Clear();
            foreach (var pat in patients)
            {
                Patients.Add(pat);
            }
            if (Patients.Any()) SelectedPatient = Patients.First();
        }

        private void CreateAppointment()
        {
            if (SelectedDoctor == null || SelectedPatient == null)
            {
                StatusMessage = "Please select a doctor and patient.";
                return;
            }

            _appointmentService.ScheduleAppointment(SelectedDoctor, SelectedPatient, SelectedDate);
            StatusMessage = $"Appointment created for {SelectedPatient.FullName} with Dr. {SelectedDoctor.FullName}";
        }

        private void Logout()
        {
            Application.Singleton.UserSession.Instance.Logout();
            _navigationService.NavigateTo<LoginViewModel>();
        }
    }
}
