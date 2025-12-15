using OrderManagerDesktopUI.Core;
using OrderManagerDesktopUI.Views;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels;
public class CustomersViewModel : ViewModel
{
    private readonly ICustomerService _customerService;
    public ObservableCollection<Customer> Customers { get; set; } = new();
    public static ICollectionView CustomersCollectionView { get; set; }

    private Customer _selectedCustomer;
    public Customer SelectedCustomer
    {
        get { return _selectedCustomer; }
        set { _selectedCustomer = value; OnPropertyChanged(); }
    }

    private string _firstName;
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; OnPropertyChanged(); }
    }

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; OnPropertyChanged(); }
    }

    private string _phoneNumber;
    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set { _phoneNumber = value; OnPropertyChanged(); }
    }

    private string _searchTerm;
    public string SearchTerm
    {
        get => _searchTerm;
        set
        {
            _searchTerm = value;
            OnPropertyChanged(nameof(SearchTerm));
            FilterCustomers();
        }
    }

    public ICommand CreateCustomerCommand { get; private set; }
    public ICommand UpdateCustomerCommand { get; private set; }
    public ICommand RemoveCustomerCommand { get; private set; }

    public CustomersViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
        Customers = new ObservableCollection<Customer>(_customerService.GetAllCustomers());

        CreateCustomerCommand = new RelayCommand(execute => CreateCustomer(), canExecute => CanCreateCustomer());
        UpdateCustomerCommand = new RelayCommand(execute => UpdateCustomer(), canExecute => true);
        RemoveCustomerCommand = new RelayCommand((param) => RemoveCustomer(param), canExecute => true);
    }

    private bool CanCreateCustomer() => (!string.IsNullOrWhiteSpace(FirstName) &&
                                         !string.IsNullOrWhiteSpace(LastName) &&
                                         !string.IsNullOrWhiteSpace(PhoneNumber));

    private void CreateCustomer()
    {
        var newCustomer = new Customer(FirstName, LastName, PhoneNumber);
        newCustomer = _customerService.CreateCustomer(newCustomer);
        Customers.Add(newCustomer);
        ResetFields();
    }

    private void ResetFields()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
    }

    private void UpdateCustomer()
    {
        _customerService.UpdateCustomer(SelectedCustomer);
        SelectedCustomer = null;
    }

    private void RemoveCustomer(object rowData)
    {
        _customerService.RemoveCustomer((rowData as Customer).Id);
        Customers.Remove(rowData as Customer);
    }

    private void FilterCustomers()
    {
        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            CustomersCollectionView.Filter = c =>
            {
                if (c is Customer customer)
                {
                    return customer.FirstName.Contains(SearchTerm) ||
                           customer.LastName.Contains(SearchTerm) ||
                           customer.PhoneNumber.Contains(SearchTerm);
                }

                return false;
            };
        }
    }
}