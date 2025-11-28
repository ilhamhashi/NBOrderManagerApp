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
    public RelayCommand NavigateToOrdersCommand { get; set; }
    public RelayCommand NavigateToCustomersCommand { get; set; }
    public RelayCommand NavigateToProductsCommand { get; set; }
    public RelayCommand NavigateToSalesDataCommand { get; set; }
    public RelayCommand NavigateToHomeCommand { get; set; }

    public MainWindowViewModel(INavigationService navigationService)
    {
        Navigation = navigationService;
        NavigateToNewOrderCommand = new RelayCommand(o => 
        { Navigation.NavigateTo<NewOrderViewModel>(); }, o => true);
        NavigateToOrdersCommand = new RelayCommand(o =>
        { Navigation.NavigateTo<OrdersViewModel>(); }, o => true);
        NavigateToCustomersCommand = new RelayCommand(o =>
        { Navigation.NavigateTo<CustomersViewModel>(); }, o => true);
        NavigateToProductsCommand = new RelayCommand(o =>
        { Navigation.NavigateTo<ProductsViewModel>(); }, o => true);
        NavigateToSalesDataCommand = new RelayCommand(o =>
        { Navigation.NavigateTo<SalesDataViewModel>(); }, o => true);
        NavigateToHomeCommand = new RelayCommand(o =>
        { Navigation.NavigateTo<HomeViewModel>(); }, o => true);
    }
}
