using OrderManagerDesktopUI.Core;

namespace OrderManagerDesktopUI.ViewModels;
public class MainWindowViewModel : ViewModel
{
    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToNewOrderCommand { get; set; }
    public MainWindowViewModel(INavigationService navigationService)
    {
        Navigation = navigationService;
        NavigateToNewOrderCommand = new RelayCommand(o => 
        { Navigation.NavigateTo<NewOrderViewModel>(); }, o => true);
    }
}
