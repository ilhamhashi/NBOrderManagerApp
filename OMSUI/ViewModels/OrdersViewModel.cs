using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels
{
    public class OrdersViewModel : ViewModel
    {
        // <<<<<< Services >>>>>>
        private readonly IOrderService _orderService;

        // <<<<<< Lists/Repositories >>>>>>
        public ObservableCollection<Order> Orders { get; set; } = [];
        public ObservableCollection<OrderLine> OrderLines { get; set; } = [];
        public ObservableCollection<Product> Products { get; set; } = [];
        private List<IPaymentMethod>? _paymentMethods { get; set; } = [];
        private List<Payment>? _payments { get; set; } = [];
        public static ICollectionView? OrdersCollectionView { get; set; }

        // <<<<<< Properties >>>>>>
        // General Properties
        private OrderDetail _selectedOrder;
        public OrderDetail SelectedOrder
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
        private OrderStatus _selectedStatus;
        public OrderStatus SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                OnPropertyChanged();
            }
        }

        // Properties: AddPayment
        private IPaymentMethod _selectedPaymentMethod;
        public IPaymentMethod SelectedPaymentMethod
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

        private decimal _lineTotal;
        public decimal LineTotal
        {
            get => _lineTotal;
            set
            {
                _lineTotal = value;
                OnPropertyChanged();
            }
        }

        private decimal _orderTotal;
        public decimal OrderTotal
        {
            get => _orderTotal;
            set
            {
                _orderTotal = value;
                OnPropertyChanged();
            }
        }

        // Properties: DeliveryAndPickUp
        private ICollectionType _collectionDateTime;
        public ICollectionType CollectionDateTime
        {
            get => _collectionDateTime;
            set
            {
                _collectionDateTime = value;
                OnPropertyChanged();
            }
        }

        private ICollectionType _collectionNeighborhood;
        public ICollectionType CollectionNeighborhood
        {
            get => _collectionNeighborhood;
            set
            {
                _collectionNeighborhood = value;
                OnPropertyChanged();
            }
        }

        private DateTime _newPickUp;
        public DateTime NewPickUp
        {
            get => _newPickUp;
            set
            {
                _newPickUp = value;
                OnPropertyChanged();
            }
        }

        private bool _isDelivery;
        public bool IsDelivery
        {
            get => _isDelivery;
            set
            {
                _isDelivery = value;
                OnPropertyChanged();
            }
        }

        // Properties: FilterOrder
        private string _searchWord;
        public string SearchWord
        {
            get => _searchWord;
            set
            {
                _searchWord = value;
                OnPropertyChanged();
            }
        }

        // Proprieties: UpdateCustommerInfo
        private string _newPhoneNumber;
        public string NewPhoneNumber
        {
            get => _newPhoneNumber;
            set
            {
                _newPhoneNumber = value;
                OnPropertyChanged();
            }
        }

        // <<<<<< Commands >>>>>>
        // Navigation Commands

        // Other Commands
        // public ICommand UpdateOrderCommand = new RelayCommand(execute => UpdateOrder(), canExecute => CanUpdateOrder());
        public ICommand UpdateOrderStatusCommand => new RelayCommand(execute => UpdateOrderStatus(), canExecute => CanUpdateOrderStatus());
        public ICommand UpdateDeliveryAndPickUpCommand => new RelayCommand(execute => UpdateDeliveryAndPickUp(), canExecute => CanUpdateDeliveryAndPickUp());
        public ICommand AddPaymentCommand => new RelayCommand(execute => AddPayment(), canExecute => CanAddPayment());
        public ICommand UpdateCustomerInfoCommand => new RelayCommand(execute => UpdateCustomerInfo(), canExecute => CanUpdateCustomerInfo());

        // Bools for CanExecute
        private bool CanUpdateOrder() => SelectedOrder != null;
        private bool CanUpdateOrderStatus() => SelectedOrder != null && SelectedStatus != null;
        private bool CanUpdateDeliveryAndPickUp() => SelectedOrder != null && CollectionDateTime != null;
        private bool CanAddPayment() => SelectedOrder != null && SelectedPaymentMethod != null && PaymentAmount != null;
        private bool CanUpdateCustomerInfo() => SelectedOrder != null && NewPhoneNumber != null;

        // <<<<<< Constructor >>>>>>
        public OrdersViewModel(IOrderService orderService)
        {
            _orderService = orderService;

            // Sample Data
            Orders = new ObservableCollection<Order>();
            OrdersCollectionView = CollectionViewSource.GetDefaultView(Orders);
        }

        // <<<<<< Methods >>>>>>
        // Method: UpdateOrder
        // Description: Updates the details of the selected order.
        private void UpdateOrder()
        {
            // To be implemented
        }

        // Method: UpdateOrderStatus
        // Description: Updates the status of the selected order.
        private void UpdateOrderStatus()
        {
            // Update Status
            SelectedOrder.Order.Status = SelectedStatus;

            // Save changes via service
            _orderService.UpdateOrder(SelectedOrder.Order);
        }

        // Method: UpdateDeliveryAndPickUp
        // Description: Updates the delivery and pick-up details of the selected order.
        private void UpdateDeliveryAndPickUp()
        {
            // To be implemented
        }

        // Method: AddPayment
        // Description: Adds a payment to the selected order.
        private void AddPayment()
        {
            // Create new Payment
            var newPayment = new Payment(PaymentAmount, PaymentDate, SelectedPaymentMethod.PaymentMethodId)
            {
                OrderId = SelectedOrder.Order.OrderId
            };

            _payments.Add(newPayment);

            // Calculate Order Total
            OrderTotal = SelectedOrder.OrderLines
                .Sum(line => line.Product.Price * line.Quantity);

            // Calculate total paid
            var totalPaid = _payments
                .Where(p => p.OrderId == SelectedOrder.Order.OrderId)
                .Sum(p => p.PaymentAmount);

            // Calculate Outstanding Amount
            OutstandingAmount = OrderTotal - totalPaid;

            // Update OrderStatus if fully paid
            if (OutstandingAmount <= 0)
            {
                SelectedOrder.Order.Status = OrderStatus.FullyPaid;
            }
            else if (totalPaid > 0 && totalPaid < OrderTotal)
            {
                SelectedOrder.Order.Status = OrderStatus.PartiallyPaid;
            }

            // Reset fields
            PaymentAmount = 0;
            PaymentDate = DateTime.Now;
        }

        // Method: UpdateCustomerInfo
        // Description: Updates the customer information associated with the selected order.
        private void UpdateCustomerInfo()
        {
            // Update Phone Number
            SelectedOrder.Customer.PhoneNumber = NewPhoneNumber;
        }

        // Method: FilterOrder
        // Description: Filters through the list of orders based on an inputted search word.
        private void FilterOrder()
        {
            if(!string.IsNullOrEmpty(SearchWord))
            {
                OrdersCollectionView.Filter = o =>
                {
                    if (o is Customer customer)
                    {
                        return customer.FirstName.Contains(SearchWord) ||
                               customer.LastName.Contains(SearchWord) ||
                               customer.PhoneNumber.Contains(SearchWord);
                    }
                    else if (o is Order order)
                    {
                        return order.OrderId.ToString().Contains(SearchWord) ||
                               order.CustomerId.ToString().Contains(SearchWord) ||
                               order.Status.ToString().Contains(SearchWord);
                    }
                        return false;
                };
            }
            else
            {
                OrdersCollectionView.Refresh();
            }
        }
    }
}