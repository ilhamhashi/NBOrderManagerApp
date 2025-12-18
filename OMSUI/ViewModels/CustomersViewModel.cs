using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels;
public class CustomersViewModel : ViewModelBase
{
    private readonly ICustomerService _customerService;
    private ObservableCollection<Customer> _customers { get; }
    public ICollectionView CustomerCollectionView { get; }

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

    public ICommand CreateCustomerCommand { get; }
    public ICommand UpdateCustomerCommand { get; }
    public ICommand RemoveCustomerCommand { get; }

    public CustomersViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
        _customers = new ObservableCollection<Customer>(_customerService.GetAllCustomers());
        CustomerCollectionView = CollectionViewSource.GetDefaultView(_customers);

        CreateCustomerCommand = new RelayCommand(execute => CreateCustomer(), canExecute => CanCreateCustomer());
        UpdateCustomerCommand = new RelayCommand(execute => UpdateCustomer(), canExecute => CanUpdateCustomer());
        RemoveCustomerCommand = new RelayCommand((param) => RemoveCustomer(param), canExecute => true);
    }

    private bool CanCreateCustomer() => (!string.IsNullOrWhiteSpace(FirstName) &&
                                         !string.IsNullOrWhiteSpace(LastName) &&
                                         !string.IsNullOrWhiteSpace(PhoneNumber));

    private bool CanUpdateCustomer() => SelectedCustomer != null;

    private void CreateCustomer()
    {
        Customer newCustomer = new Customer(FirstName, LastName, PhoneNumber);
        newCustomer = _customerService.CreateCustomer(newCustomer);
        _customers.Add(newCustomer);
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
        if (SelectedCustomer != null)
        {
            _customerService.UpdateCustomer(SelectedCustomer);
            SelectedCustomer = null;
        }
    }

    private void RemoveCustomer(object rowData)
    {
        if (rowData != null)
        {
            var customer = rowData as Customer;
            MessageBoxResult result = MessageBox.Show($"Do you want to remove {customer.FirstName} {customer.LastName}?",
                                      "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _customerService.RemoveCustomer(customer.Id);
                _customers.Remove(customer);

                MessageBox.Show($"{customer.FirstName} {customer.LastName} is removed.", "OrderManager",
                           MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    private void FilterCustomers()
    {
        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            CustomerCollectionView.Filter = c =>
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