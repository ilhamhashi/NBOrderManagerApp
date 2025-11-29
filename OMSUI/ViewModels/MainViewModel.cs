using OrderManagerDesktopUI.Core;

namespace OrderManagerDesktopUI.ViewModels;
public class MainViewModel : ViewModel
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

    public MainViewModel(INavigationService navigationService)
    {
        Navigation = navigationService;
        Navigation.NavigateTo<HomeViewModel>();
        NavigateToNewOrderCommand = new RelayCommand(
            execute => { Navigation.NavigateTo<NewOrderViewModel>(); }, canExecute => true);
        NavigateToOrdersCommand = new RelayCommand(
            execute => { Navigation.NavigateTo<OrdersViewModel>(); }, canExecute => true);
        NavigateToCustomersCommand = new RelayCommand(
            execute => { Navigation.NavigateTo<CustomersViewModel>(); }, canExecute => true);
        NavigateToProductsCommand = new RelayCommand(
            execute => { Navigation.NavigateTo<ProductsViewModel>(); }, canExecute => true);
        NavigateToSalesDataCommand = new RelayCommand(
            execute => { Navigation.NavigateTo<SalesDataViewModel>(); }, canExecute => true);
        NavigateToHomeCommand = new RelayCommand(
            execute => { Navigation.NavigateTo<HomeViewModel>(); }, canExecute => true);
    }
}
