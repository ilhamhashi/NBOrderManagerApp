using OrderManagerDesktopUI.Core;
using System.Windows;
using System.Windows.Input;

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
    public RelayCommand NavigateToDashboardCommand { get; set; }

    public ICommand CloseCommand { get; }
    public ICommand MaximizeCommand { get; }
    public ICommand MinimizeCommand { get; }


    public MainViewModel(INavigationService navigationService)
    {
        // Window control commands -> Close, Maximise and Minimize window
        CloseCommand = new RelayCommand(o =>
        {
            Application.Current.Shutdown();
        });

        MaximizeCommand = new RelayCommand(o =>
        {
            var window = Application.Current.MainWindow;
            window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        });

        MinimizeCommand = new RelayCommand(o =>
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        });

        Navigation = navigationService;
        Navigation.NavigateTo<DashboardViewModel>();

        NavigateToDashboardCommand = new RelayCommand(
            execute => { Navigation.NavigateTo<DashboardViewModel>(); }, canExecute => true);
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

    }
}
