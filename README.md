# Medical Appointment System

A comprehensive WPF application demonstrating **Object-Oriented Programming (OOP)** principles and **Design Patterns** in a real-world medical appointment management system.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=.net)
![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)
![WPF](https://img.shields.io/badge/WPF-Windows-0078D7?logo=windows)
![SQLite](https://img.shields.io/badge/SQLite-07405E?logo=sqlite)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-Core-512BD4)

---

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [OOP Principles](#oop-principles)
- [Design Patterns](#design-patterns)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [User Credentials](#user-credentials)
- [Technologies Used](#technologies-used)

---

## Features

### Admin Features
- View all appointments across the system
- Create new appointments (assign doctor to patient)
- Manage doctors and patients
- Real-time appointment updates

### Doctor Features
- View personal appointments
- Add consultations (diagnosis & treatment)
- Track appointment status (Pending/Completed)
- Auto-refresh on new appointments

### Patient Features
- View personal appointments
- Access consultation details
- View diagnosis and treatment information
- Track appointment status (Scheduled/Completed)

---

## Architecture

The application follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                        │
│                   (WPF + MVVM Pattern)                       │
├─────────────────────────────────────────────────────────────┤
│                    Application Layer                         │
│           (Business Logic + Services + DTOs)                 │
├─────────────────────────────────────────────────────────────┤
│                   Infrastructure Layer                       │
│        (Data Access + Repository Implementation)             │
├─────────────────────────────────────────────────────────────┤
│                      Domain Layer                            │
│              (Entities + Business Rules)                     │
└─────────────────────────────────────────────────────────────┘
```

---

## OOP Principles

### 1. Encapsulation
Hiding internal state and requiring interaction through well-defined interfaces.

```csharp
public class Appointment
{
    // Private fields - hidden from outside
    public Guid Id { get; private set; }
    public Consultation? Consultation { get; private set; }
    
    // Public method - controlled access
    public void AddConsultation(Consultation consultation)
    {
        Consultation = consultation;
    }
}
```

**Benefits:**
- Data integrity (no direct property setters)
- Controlled state changes
- Business logic enforcement

### 2. Inheritance
Creating hierarchies to promote code reuse and establish relationships.

```csharp
// Base class
public abstract class User
{
    public Guid Id { get; protected set; }
    public string Username { get; protected set; }
    public UserRole Role { get; protected set; }
}

// Derived classes
public class AdminUser : User { }
public class DoctorUser : User 
{
    public Doctor DoctorProfile { get; private set; }
}
public class PatientUser : User 
{
    public Patient PatientProfile { get; private set; }
}
```

**Benefits:**
- Code reuse (common User properties)
- Polymorphic behavior
- Type hierarchy

### 3. Polymorphism
Objects of different types responding to the same interface differently.

```csharp
// Runtime polymorphism - different dashboard based on user type
var user = UserSession.Instance.CurrentUser;

if (user.Role == UserRole.Admin)
    navigationService.NavigateTo<AdminDashboardViewModel>();
else if (user.Role == UserRole.Doctor)
    navigationService.NavigateTo<DoctorDashboardViewModel>();
else if (user.Role == UserRole.Patient)
    navigationService.NavigateTo<PatientDashboardViewModel>();
```

**Benefits:**
- Flexible code
- Runtime decision making
- Easy extensibility

### 4. Abstraction
Hiding complexity and exposing only essential features.

```csharp
// Interface abstraction
public interface IAppointmentRepository
{
    void Add(Appointment appointment);
    void Update(Appointment appointment);
    IEnumerable<Appointment> GetAll();
    IEnumerable<Appointment> GetByDoctor(Guid doctorId);
}

// Implementation hidden from consumers
public class AppointmentService
{
    private readonly IAppointmentRepository _repository;
    
    public void ScheduleAppointment(Doctor doctor, Patient patient, DateTime date)
    {
        var appointment = new Appointment(doctor, patient, date);
        _repository.Add(appointment); // Implementation details hidden
    }
}
```

**Benefits:**
- Decoupled code
- Easy testing (mock interfaces)
- Implementation flexibility

---

## Design Patterns

### 1. Repository Pattern
Abstracts data access logic and provides a collection-like interface.

```csharp
// Repository interface
public interface IAppointmentRepository
{
    void Add(Appointment appointment);
    Appointment? GetById(Guid id);
    IEnumerable<Appointment> GetAll();
}

// Repository implementation
public class AppointmentRepository : IAppointmentRepository
{
    private readonly MedicalDbContext _context;
    
    public void Add(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        _context.SaveChanges();
    }
}
```

**Benefits:**
- Separation of concerns
- Centralized data access logic
- Easy to test (mock repositories)
- Database-agnostic services

### 2. Singleton Pattern
Ensures only one instance of a class exists.

```csharp
public sealed class UserSession
{
    private static readonly Lazy<UserSession> _instance = 
        new(() => new UserSession());

    public static UserSession Instance => _instance.Value;
    
    public User? CurrentUser { get; private set; }
    
    private UserSession() { }
    
    public void Login(User user) => CurrentUser = user;
    public void Logout() => CurrentUser = null;
}
```

**Benefits:**
- Global state management
- Thread-safe implementation
- Controlled access point

### 3. Observer Pattern
Notifies multiple objects about state changes.

```csharp
public class AppointmentService
{
    // Event subscribers get notified
    public event Action? AppointmentAdded;
    public event Action? ConsultationAdded;
    
    public void ScheduleAppointment(Doctor doctor, Patient patient, DateTime date)
    {
        var appointment = new Appointment(doctor, patient, date);
        _repository.Add(appointment);
        AppointmentAdded?.Invoke(); // Notify observers
    }
}

// Observer subscribes
public class AdminDashboardViewModel
{
    public AdminDashboardViewModel(AppointmentService service)
    {
        service.AppointmentAdded += LoadAppointments; // Subscribe
    }
}
```

**Benefits:**
- Loose coupling
- Real-time UI updates
- Event-driven architecture

### 4. MVVM Pattern (Model-View-ViewModel)
Separates UI from business logic.

```
┌──────────┐        ┌──────────────┐        ┌───────┐
│   View   │ ◄────► │  ViewModel   │ ◄────► │ Model │
│  (XAML)  │ Binding│   (Logic)    │        │(Data) │
└──────────┘        └──────────────┘        └───────┘
```

```csharp
// ViewModel
public class DoctorDashboardViewModel : BaseViewModel
{
    private string _diagnosis;
    public string Diagnosis
    {
        get => _diagnosis;
        set { _diagnosis = value; OnPropertyChanged(); }
    }
    
    public RelayCommand AddConsultationCommand { get; }
}
```

```xml
<!-- View (XAML) -->
<TextBox Text="{Binding Diagnosis, UpdateSourceTrigger=PropertyChanged}"/>
<Button Command="{Binding AddConsultationCommand}"/>
```

**Benefits:**
- Testable UI logic
- Designer-developer workflow
- Data binding

### 5. Factory Pattern
Creates objects without specifying their exact classes.

```csharp
Func<Type, BaseViewModel> viewModelFactory = type =>
{
    if (type == typeof(LoginViewModel))
        return new LoginViewModel(authService, navigationService);
    if (type == typeof(AdminDashboardViewModel))
        return new AdminDashboardViewModel(appointmentService, ...);
    if (type == typeof(DoctorDashboardViewModel))
        return new DoctorDashboardViewModel(appointmentService, ...);
    throw new ArgumentException($"Unknown ViewModel: {type}");
};
```

**Benefits:**
- Centralized object creation
- Dependency injection
- Easy to extend

### 6. Dependency Injection (DI)
Injects dependencies rather than creating them.

```csharp
public class AppointmentService
{
    private readonly IAppointmentRepository _repository;
    
    // Dependencies injected via constructor
    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }
}
```

**Benefits:**
- Loose coupling
- Testability
- Flexibility

### 7. Strategy Pattern
Used in password hashing.

```csharp
public static class PasswordHasher
{
    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    public static bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
```

---

## Project Structure

```
MedicalAppointmentSystem/
│
├── MedicalAppointmentSystem.Domain/          # Core business entities
│   ├── Entities/
│   │   ├── User.cs                           # Abstract base class
│   │   ├── AdminUser.cs                      # Inheritance
│   │   ├── DoctorUser.cs                     # Inheritance
│   │   ├── PatientUser.cs                    # Inheritance
│   │   ├── Doctor.cs                         # Domain entity
│   │   ├── Patient.cs                        # Domain entity
│   │   ├── Appointment.cs                    # Encapsulation
│   │   └── Consultation.cs                   # Domain entity
│   └── Enums/
│       └── UserRole.cs
│
├── MedicalAppointmentSystem.Application/     # Business logic
│   ├── Services/
│   │   ├── AuthenticationService.cs          # Service layer
│   │   └── AppointmentService.cs             # Observer pattern
│   ├── Interfaces/                           # Abstraction
│   │   ├── IUserRepository.cs
│   │   ├── IAppointmentRepository.cs
│   │   ├── IDoctorRepository.cs
│   │   └── IPatientRepository.cs
│   ├── Security/
│   │   └── PasswordHasher.cs                 # Strategy pattern
│   └── Singleton/
│       └── UserSession.cs                    # Singleton pattern
│
├── MedicalAppointmentSystem.Infrastructure/  # Data access
│   ├── Data/
│   │   └── MedicalDbContext.cs               # EF Core context
│   ├── Repositories/                         # Repository pattern
│   │   ├── UserRepository.cs
│   │   ├── AppointmentRepository.cs
│   │   ├── DoctorRepository.cs
│   │   └── PatientRepository.cs
│   ├── Configurations/                       # EF configurations
│   │   ├── UserConfiguration.cs
│   │   ├── AppointmentConfiguration.cs
│   │   └── ConsultationConfiguration.cs
│   └── Seed/
│       └── DbSeeder.cs
│
└── MedicalAppointmentSystem.Presentation/    # UI Layer
    ├── ViewModels/                           # MVVM pattern
    │   ├── BaseViewModel.cs                  # Base class
    │   ├── MainViewModel.cs
    │   ├── LoginViewModel.cs
    │   ├── AdminDashboardViewModel.cs
    │   ├── DoctorDashboardViewModel.cs
    │   └── PatientDashboardViewModel.cs
    ├── Views/                                # XAML views
    │   ├── LoginView.xaml
    │   ├── AdminDashboardView.xaml
    │   ├── DoctorDashboardView.xaml
    │   └── PatientDashboardView.xaml
    ├── Commands/
    │   └── RelayCommand.cs                   # Command pattern
    ├── Services/
    │   ├── INavigationService.cs
    │   └── NavigationService.cs              # Navigation
    └── App.xaml.cs                           # DI container
```

---

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or JetBrains Rider
- Windows OS (for WPF)

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/MedicalAppointmentSystem.git
cd MedicalAppointmentSystem
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Build the solution**
```bash
dotnet build
```

4. **Run the application**
```bash
dotnet run --project MedicalAppointmentSystem.Presentation
```

The database will be automatically created and seeded with test data.

---

## User Credentials

| Role | Username | Password | Description |
|------|----------|----------|-------------|
| **Admin** | `admin` | `admin123` | Full system access |
| **Doctor** | `house` | `house123` | Dr. House - Diagnostician |
| **Patient** | `johndoe` | `patient123` | John Doe - Patient |

---

## Technologies Used

| Technology | Purpose |
|------------|---------|
| **C# 12.0** | Programming language |
| **WPF** | Desktop UI framework |
| **Entity Framework Core** | ORM for data access |
| **SQLite** | Embedded database |
| **MVVM** | UI architecture pattern |
| **BCrypt.Net** | Password hashing |

---

## Key Takeaways

### OOP Principles Applied
- **Encapsulation** - Private setters, controlled access
- **Inheritance** - User hierarchy (Admin, Doctor, Patient)
- **Polymorphism** - Role-based UI navigation
- **Abstraction** - Repository interfaces

### Design Patterns Implemented
- **Repository** - Data access abstraction
- **Singleton** - UserSession management
- **Observer** - Event-driven UI updates
- **MVVM** - Presentation layer architecture
- **Factory** - ViewModel creation
- **Dependency Injection** - Service dependencies
- **Strategy** - Password hashing

---


