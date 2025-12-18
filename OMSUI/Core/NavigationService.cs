using OrderManagerDesktopUI.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderManagerLibrary.Services
{
    public class NavigationService : INavigationService, INotifyPropertyChanged
    {
        private ViewModelBase _currentView;
        private ViewModelBase _currentNestedView;
        private readonly Func<Type, ViewModelBase> _viewModelFactory;

        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            private set { _currentView = value; OnPropertyChanged(); }
        }

        public ViewModelBase CurrentNestedView
        {
            get { return _currentNestedView; }
            private set { _currentNestedView = value; OnPropertyChanged(); }
        }

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }

        public void NavigateToNested<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentNestedView = viewModel;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
