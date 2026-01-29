using MedicalAppointmentSystem.Presentation.ViewModels;
using System;

namespace MedicalAppointmentSystem.Presentation.Services
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>() where TViewModel : BaseViewModel;
        void NavigateTo(BaseViewModel viewModel);
    }

    public class NavigationService : INavigationService
    {
        private readonly MainViewModel _mainViewModel;
        private readonly Func<Type, BaseViewModel> _viewModelFactory;

        public NavigationService(MainViewModel mainViewModel, Func<Type, BaseViewModel> viewModelFactory)
        {
            _mainViewModel = mainViewModel;
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            var viewModel = _viewModelFactory(typeof(TViewModel));
            _mainViewModel.CurrentViewModel = viewModel;
        }

        public void NavigateTo(BaseViewModel viewModel)
        {
            _mainViewModel.CurrentViewModel = viewModel;
        }
    }
}
