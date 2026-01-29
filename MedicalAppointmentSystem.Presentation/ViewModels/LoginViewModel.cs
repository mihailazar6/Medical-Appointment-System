using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalAppointmentSystem.Application.Services;
using MedicalAppointmentSystem.Presentation.Commands;
using MedicalAppointmentSystem.Presentation.Services;

namespace MedicalAppointmentSystem.Presentation.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;

        private readonly AuthenticationService _authService;
        private readonly INavigationService _navigationService;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public RelayCommand LoginCommand { get; }

        public LoginViewModel(AuthenticationService authService, INavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            bool success = _authService.Login(Username, Password);

            if (!success)
            {
                ErrorMessage = "Invalid username or password.";
                return;
            }

            ErrorMessage = string.Empty;
            
            // Get the logged in user to check role
            var user = MedicalAppointmentSystem.Application.Singleton.UserSession.Instance.CurrentUser;
            if (user == null) return;

            if (user.Role == Domain.Enums.UserRole.Admin)
            {
                _navigationService.NavigateTo<AdminDashboardViewModel>();
            }
            else if (user.Role == Domain.Enums.UserRole.Doctor)
            {
                _navigationService.NavigateTo<DoctorDashboardViewModel>();
            }
            else if (user.Role == Domain.Enums.UserRole.Patient)
            {
                _navigationService.NavigateTo<PatientDashboardViewModel>();
            }
        }
    }
}
