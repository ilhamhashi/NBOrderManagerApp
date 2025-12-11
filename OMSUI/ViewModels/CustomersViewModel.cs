using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels
{
    public class CustomersViewModel : ViewModel
    {
        // Liste over kunder
        public ObservableCollection<Customer> Customers { get; set; } = new();
        public static ICollectionView CustomersCollectionView { get; set; }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
                if (_selectedCustomer != null)
                {
                    FirstName = _selectedCustomer.FirstName;
                    LastName = _selectedCustomer.LastName;
                    PhoneNumber = _selectedCustomer.PhoneNumber;
                }
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); FilterCustomers(); }
        }



        // Commands
        public ICommand CreateCustomerCommand { get; }
        public ICommand EditCustomerCommand { get; }

        public ICommand DeleteCustomerCommand { get; }

        public CustomersViewModel()
        {
            CreateCustomerCommand = new RelayCommand(o => CreateCustomer());
            EditCustomerCommand = new RelayCommand(o => EditCustomer());
            DeleteCustomerCommand = new RelayCommand(o => DeleteCustomer());


            // Eksempel-data
            Customers.Add(new Customer("Ali", "Hassan", "12345678"));
            Customers.Add(new Customer("Sara", "Mohamed", "87654321"));
            Customers = new ObservableCollection<Customer>(Customers);
        }

        private void CreateCustomer()
        {
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
            {
                var newCustomer = new Customer(FirstName, LastName, PhoneNumber);
                Customers.Add(newCustomer);
            }
        }

        private void EditCustomer()
        {
            if (SelectedCustomer != null)
            {
                SelectedCustomer.FirstName = FirstName;
                SelectedCustomer.LastName = LastName;
                SelectedCustomer.PhoneNumber = PhoneNumber;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                Customers.Remove(SelectedCustomer);
                Customers.Remove(SelectedCustomer);
                FirstName = "";
                LastName = "";
                PhoneNumber = "";
            }
        }

        private void FilterCustomers()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                CustomersCollectionView.Filter = c =>
                {
                    if (c is Customer customer)
                    {
                        return customer.FirstName.Contains(SearchText) ||
                             customer.LastName.Contains(SearchText) ||
                             customer.PhoneNumber.Contains(SearchText);
                    }

                    return false;
                };
            }
        }
    }
}
