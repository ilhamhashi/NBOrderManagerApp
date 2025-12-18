using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;

using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels;
public class NewOrderViewModel : ViewModelBase
{
    private readonly IOrderService _orderservice;

    private ObservableCollection<PaymentMethod> _paymentMethods { get; }
    private ObservableCollection<Payment> _payments { get; } = [];
    private ObservableCollection<OrderLine> OrderLines { get; } = [];
    private ObservableCollection<Product> _products { get; }
    private ObservableCollection<Customer> _customers { get; }
    public ICollectionView PaymentMethodsCollectionView { get; }
    public ICollectionView ProductsCollectionView { get; }
    public ICollectionView CustomersCollectionView { get; }
    public ICollectionView OrderLinesCollectionView { get; }

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

    private Product selectedProduct;
    private Customer selectedCustomer;
    private PaymentMethod? selectedPaymentMethod;
    private DateTime pickUpDateTime = DateTime.Today.AddDays(1);
    private OrderLine? selectedOrderLine;
    private bool isDelivery = false;
    private string location = "Store";
    private string noteContent = string.Empty;
    private decimal runningTotal;
    private decimal discountTotal;
    private decimal paymentAmount;
    private decimal outstandingAmount;

    public ICommand CreateOrderCommand { get; }
    public ICommand AddPaymentCommand {  get; }
    public ICommand AddProductToOrderCommand {  get; }

    public ICommand IncreaseQuantityCommand { get; }
    public ICommand DecreaseQuantityCommand { get; }
    public ICommand AddDiscountCommand { get; }

    public ICommand NavigateToNoteViewCommand { get; }
    public ICommand NavigateToNewOrderDetailsViewCommand { get; }

    private bool CanAddNewOrder() => SelectedCustomer != null && OrderLines.Count() != 0; 
    private bool CanAddPaymentToOrder() => SelectedPaymentMethod != null && PaymentAmount > 0;

    public Product SelectedProduct
    {
        get { return selectedProduct; }
        set { selectedProduct = value; OnPropertyChanged(); }
    }

    public Customer SelectedCustomer
    {
        get { return selectedCustomer; }
        set { selectedCustomer = value; OnPropertyChanged(); }
    }

    public PaymentMethod? SelectedPaymentMethod
    {
        get { return selectedPaymentMethod; }
        set { selectedPaymentMethod = value; OnPropertyChanged(); }
    }

    public DateTime PickUpDateTime
    {
        get { return pickUpDateTime; }
        set { pickUpDateTime = value; OnPropertyChanged(); }
    }

    public DateTime PickUpDate
    {
        get { return PickUpDateTime; }
        set { PickUpDateTime = value.Date.Add(PickUpDateTime.TimeOfDay); OnPropertyChanged(); }
    }

    public DateTime PickUpTime
    {
        get { return PickUpDateTime; }
        set { PickUpDateTime = PickUpDateTime.Date.Add(value.TimeOfDay); OnPropertyChanged(); }
    }

    public OrderLine? SelectedOrderLine
    {
        get { return selectedOrderLine; }
        set { selectedOrderLine = value; OnPropertyChanged(); }
    }

    public bool IsDelivery
    {
        get { return isDelivery; }
        set { isDelivery = value; OnPropertyChanged(); }
    }

    public string Location
    {
        get { return location; }
        set { location = value; OnPropertyChanged(); }
    }
        public string NoteContent
    {
        get { return noteContent; }
        set { noteContent = value; OnPropertyChanged(); }
    }

	public decimal RunningTotal
	{
		get { return runningTotal; }
		set { runningTotal = value; OnPropertyChanged(); }
	}

    public decimal DiscountTotal
    {
        get { return discountTotal; }
        set { discountTotal = value; OnPropertyChanged(); }
    }

    public decimal PaymentAmount
    {
        get { return paymentAmount; }
        set { paymentAmount = value; OnPropertyChanged(); }
    }

    public decimal OutstandingAmount
    {
        get { return outstandingAmount; }
        set { outstandingAmount = value; OnPropertyChanged(); }
    }

    public NewOrderViewModel(IOrderService orderService, IProductService productService, ICustomerService customerService, 
                             IPaymentService paymentService,  INavigationService navigationService)
    {
        _orderservice = orderService;
        _products = new ObservableCollection<Product>(productService.GetAllProducts());
        _customers = new ObservableCollection<Customer>(customerService.GetAllCustomers());
        _paymentMethods = new ObservableCollection<PaymentMethod>(paymentService.GetPaymentMethods());
        ProductsCollectionView = CollectionViewSource.GetDefaultView(_products);
        CustomersCollectionView = CollectionViewSource.GetDefaultView(_customers);
        PaymentMethodsCollectionView = CollectionViewSource.GetDefaultView(_paymentMethods);
        OrderLinesCollectionView = CollectionViewSource.GetDefaultView(OrderLines);
        Navigation = navigationService;
        Navigation.NavigateToNested<NoteViewModel>();

        NavigateToNoteViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<NoteViewModel>(); }, canExecute => true);

        NavigateToNewOrderDetailsViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<NewOrderDetailsViewModel>(); }, canExecute => true);

        CreateOrderCommand = new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
        AddPaymentCommand = new RelayCommand(execute => AddPaymentToOrder(), canExecute => CanAddPaymentToOrder());
        AddProductToOrderCommand = new RelayCommand((param) => AddProductToOrder(param), canExecute => true);
        IncreaseQuantityCommand = new RelayCommand((param) => IncreaseQuantity(param), canExecute => true);
        DecreaseQuantityCommand = new RelayCommand((param) => DecreaseQuantity(param), canExecute => true);
        AddDiscountCommand = new RelayCommand((param) => AddDiscountToOrderLine(param), canExecute => true);
    }

    private void AddNewOrder()
    {
        PickUp pickUp = new PickUp(PickUpDateTime, IsDelivery, Location);
        Note note = new Note(NoteContent);
        Order newOrder = new(DateTime.Now, SelectedCustomer, pickUp, note);
        newOrder.Status = SetOrderStatus();
        
        MessageBoxResult result = MessageBox.Show($"Please confirm order for {SelectedCustomer.FirstName} {SelectedCustomer.LastName}" +
                                                  $"\nPickUp: {PickUpDateTime.Date} {PickUpTime}" +
                                                  $"\nLocation: {Location}" +
                                                  $"\nOutstanding: ${OutstandingAmount} ",
                                                  "OrderManager", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            newOrder = _orderservice.CreateOrder(newOrder, [.. OrderLines], [.. _payments]);

            MessageBox.Show($"Order {newOrder.Id} has been created!", "Completed",
                       MessageBoxButton.OK, MessageBoxImage.Information);

            ResetFieldsAfterOrderCompletion();
        }        
    }

    private OrderStatus SetOrderStatus()
    {
        if (outstandingAmount == 0)
        {
            return OrderStatus.Paid;
        }
        else if (outstandingAmount > 0 && outstandingAmount < RunningTotal)
        {
            return OrderStatus.PartiallyPaid;
        }
        else
        {
            return OrderStatus.Draft;
        }
    }

    private void ResetFieldsAfterOrderCompletion()
    {
        OrderLines.Clear();
        SelectedCustomer = null;
        SelectedOrderLine = null;
        SelectedPaymentMethod = null;
        _payments.Clear();
        PaymentAmount = decimal.Zero;
        NoteContent = string.Empty;
        PickUpDateTime = DateTime.Today.AddDays(1);
        isDelivery = false;
        Location = "Store";      
    }

    private void AddProductToOrder(object rowData)
    {
        if (rowData != null)
        {
            Product product = rowData as Product;
            if (product.Taste != null && product.Size != null)
            {
                OrderLine newOrderLine = new(product, 1, product.Price, 0, product.Taste.Name, product.Size.Name);
                ResetLineNumber();
                newOrderLine.LineNumber = OrderLines.Count + 1;
                OrderLines?.Add(newOrderLine);
                UpdateRunningTotal(); UpdateDiscountTotal(); UpdateOutstandingAmount();
            }
            else
            {
                MessageBox.Show($"Please select a size and taste before adding to order", "OrderManager",
                       MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    private void ResetLineNumber()
    {
        int i = 1;
        foreach (var orderline in OrderLines)
        {
            orderline.LineNumber = i;
            i++;
        }
    }

    private void UpdateRunningTotal()
    {
        RunningTotal = (OrderLines.Sum(ol => ol.Price * ol.Quantity));
    }

    private void UpdateDiscountTotal()
    {
        DiscountTotal = (OrderLines.Sum(ol => ol.Discount * ol.Quantity));
    }

    private void IncreaseQuantity(object rowOrderLine)
    {
        if (rowOrderLine != null)
        {
            (rowOrderLine as OrderLine).IncreaseQuantity();
            UpdateRunningTotal(); UpdateDiscountTotal(); UpdateOutstandingAmount();
            OrderLinesCollectionView.Refresh();
        }
    }

    private void DecreaseQuantity(object rowOrderLine)
    {
        if (rowOrderLine != null)
        {
            OrderLine orderline = rowOrderLine as OrderLine;
            if (orderline.Quantity > 1)
            {
                orderline.DecreaseQuantity();
            }
            else
            {
                OrderLines.Remove(orderline);
                ResetLineNumber();
            }
            UpdateRunningTotal(); UpdateDiscountTotal();
            UpdateOutstandingAmount();
            OrderLinesCollectionView.Refresh();
        }
    }

    private void AddDiscountToOrderLine(object rowOrderLine)
    {
        if (rowOrderLine != null)
        {
            (rowOrderLine as OrderLine).ApplyDiscount();
            UpdateRunningTotal(); UpdateDiscountTotal();
            OrderLinesCollectionView.Refresh();
        }
    }

    private void AddPaymentToOrder()
    {
        if (selectedPaymentMethod != null)
        {
            Payment newPayment = new(PaymentAmount, DateTime.Now, SelectedPaymentMethod);
            _payments.Add(newPayment);
            UpdateOutstandingAmount();

            SelectedPaymentMethod = null;
            PaymentAmount = decimal.Zero;
        }
    }

    private void UpdateOutstandingAmount()
    {
        decimal totalPaid = _payments.Sum(p => p.Amount);
        OutstandingAmount = RunningTotal - totalPaid;
    }

}
