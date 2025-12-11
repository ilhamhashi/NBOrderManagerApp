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
    private string noteText = "mainvm";

    public string NoteText
    {
        get { return noteText; }
        set { noteText = value; OnPropertyChanged(); }
    }
    public RelayCommand NavigateToNewOrderCommand { get; set; }
    public RelayCommand NavigateToOrdersCommand { get; set; }
    public RelayCommand NavigateToCustomersCommand { get; set; }
    public RelayCommand NavigateToProductsCommand { get; set; }
    public RelayCommand NavigateToSalesDataCommand { get; set; }
    public RelayCommand NavigateToHomeCommand { get; set; }

    public ICommand CloseCommand { get; }
    public ICommand MaximizeCommand { get; }
    public ICommand MinimizeCommand { get; }


    public MainViewModel(INavigationService navigationService)
    {
        // Window control commands -> Close, maximize, minimize
        CloseCommand = new RelayCommand(o =>
        {
            Application.Current.Shutdown();
        });

        MaximizeCommand = new RelayCommand(o =>
        {
            var window = Application.Current.MainWindow;
            window.WindowState = window.WindowState == WindowState.Normal
                ? WindowState.Maximized
                : WindowState.Normal;
        });

        MinimizeCommand = new RelayCommand(o =>
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        });

        // Navigation commands
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
