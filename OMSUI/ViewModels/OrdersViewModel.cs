using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels
{
    public class OrdersViewModel : ViewModel
    {
        // <<<<<< Lists/Repositories >>>>>>
        public ObservableCollection<Order> Orders { get; set; } = [];
        public ObservableCollection<OrderLine> OrderLines { get; set; } = [];
        public ObservableCollection<Product> Products { get; set; } = [];
        private List<IPaymentMethod>? _paymentMethods { get; set; } = [];
        private List<Payment>? _payments { get; set; } = [];
        public static ICollectionView? OrdersCollectionView { get; set; }

        // <<<<<< Properties >>>>>>
        // General Properties
        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }

        // Properties: UpdateOrder

        // Properties: UpdateOrderStatus

        // Properties: AddPayment
        private MobilePayment _selectedPaymentMethod;
        public MobilePayment SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set
            {
                _selectedPaymentMethod = value;
                OnPropertyChanged();
            }
        }
        private decimal _paymentAmount;
        public decimal PaymentAmount
        {
            get => _paymentAmount;
            set
            {
                _paymentAmount = value;
                OnPropertyChanged();
            }
        }
        private decimal _outstandingAmount;
        public decimal OutstandingAmount
        {
            get => _outstandingAmount;
            set
            {
                _outstandingAmount = value;
                OnPropertyChanged();
            }
        }
        private DateTime _paymentDate;
        public DateTime PaymentDate
        {
            get => _paymentDate;
            set
            {
                _paymentDate = value;
                OnPropertyChanged();
            }
        }

        // <<<<<< Commands >>>>>>
        // Navigation Commands

        // Other Commands
       // public ICommand UpdateOrderCommand = new RelayCommand(execute => UpdateOrder(), canExecute => CanUpdateOrder());
        //public ICommand UpdateOrderStatusCommand => new RelayCommand(execute => UpdateOrderStatus(), canExecute => CanUpdateOrderStatus());
        //public ICommand UpdateCustomerInfo => new RelayCommand(execute => UpdateCustomerInfo(), canExecute => CanUpdateCustomerInfo());
        //public ICommand UpdateDeliveryAndPickUp => new RelayCommand(execute => UpdateDeliveryAndPickUp(), canExecute => CanUpdateDeliveryAndPickUp());
        //public ICommand AddPaymentCommand => new RelayCommand(execute => AddPayment(), canExecute => CanAddPayment());

        // Bools for CanExecute
        private bool CanUpdateOrder() => SelectedOrder != null;
        private bool CanUpdateOrderStatus() => SelectedOrder != null;
        private bool CanUpdateCustomerInfo() => SelectedOrder != null;
        private bool CanUpdateDeliveryAndPickUp() => SelectedOrder != null;
        private bool CanAddPayment() => SelectedOrder != null && SelectedPaymentMethod != null && PaymentAmount != null;

        // <<<<<< Constructor >>>>>>
        public OrdersViewModel()
        {
            OrdersCollectionView = CollectionViewSource.GetDefaultView(Orders);
        }

        // <<<<<< Methods >>>>>>
        // Method: UpdateOrder
        // Description: Updates the details of the selected order.

        // Method: UpdateOrderStatus
        // Description: Updates the status of the selected order.

        // Method: AddPayment
        // Description: Adds a payment to the selected order.
        private void AddPayment()
        {
            Payment newPayment = new Payment(SelectedOrder.Payment, PaymentDate, SelectedPaymentMethod.PaymentMethodId);

            _payments.Add(newPayment);
            _paymentMethods.Add(SelectedPaymentMethod);
        }

        // Method: SearchOrder
        // Description: Searches for orders based on an inputted search word.
    }
}