using OrderManagerDesktopUI.Core;

namespace OrderManagerLibrary.Services
{
    public class NavigationService : ViewModelBase, INavigationService
    {
        private ViewModel _currentView;
        private ViewModel _currentNestedView;
        private readonly Func<Type, ViewModel> _viewModelFactory;

        public ViewModel CurrentView
        {
            get { return _currentView; }
            private set { _currentView = value; OnPropertyChanged(); }
        }

        public ViewModel CurrentNestedView
        {
            get { return _currentNestedView; }
            private set { _currentNestedView = value; OnPropertyChanged(); }
        }

        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }

        public void NavigateToNested<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentNestedView = viewModel;
        }
    }
}
