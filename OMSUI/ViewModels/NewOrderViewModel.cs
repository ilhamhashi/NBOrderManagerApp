using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels;
public class NewOrderViewModel : ViewModel
{
    private readonly IOrderService _orderservice;

    private Product? selectedProduct;
	private ICustomer selectedCustomer;
	private IPaymentMethod? selectedPaymentMethod;
    private OrderStatus orderStatus;
    private int lineNumber;
    private OrderLine selectedOrderLine;
    private string noteText = "newordervm";
    private DateTime collectionDateTime;
    private string collectionNeighborhood;    
    private decimal paymentAmount;	
	private int selectedQuantity;
	private bool isDelivery;    
	private decimal? orderTotal;
	private decimal outstandingAmount;


    private List<IPaymentMethod>? _paymentMethods {  get; set; } = [];
    private List<Payment>? _payments { get; set; } = [];
    private List<OrderLine> _orderLines { get; set; }
    public ObservableCollection<OrderLine>? OrderLines { get; set; } = []; 
    public ObservableCollection<Product>? Products { get; set; }
    public static ICollectionView? ProductsCollectionView { get; set; }

    public ICommand CreateOrderCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
    public ICommand AddPaymentCommand => new RelayCommand(execute => AddPaymentToOrder(), canExecute => CanAddPaymentToOrder());
    public ICommand AddToOrderCommand {  get; private set; }
    public ICommand NavigateToNewOrderRegistrationCommand { get; set; }
    // Navigation Commands - to be implemented
    //public ICommand CancelNewOrderCommand => new RelayCommand(execute => CancelNewOrder(), canExecute => CanCancelNewOrder());
    //public ICommand ContinueToPaymentCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
    //public ICommand GoBackToOrderDetailsCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
    //public ICommand SelectProductCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());

    private bool CanAddNewOrder() => true; // Placeholder for actual logic
    private bool CanCancelNewOrder() => true; // Placeholder for actual logic
    private bool CanContinueToPayment() => true; // Placeholder for actual logic
    private bool CanGoBackToOrderDetails() => true; // Placeholder for actual logic
    private bool CanAddToOrder() => true;
    private bool CanSelectProduct() => true; // Placeholder for actual logic
    private bool CanAddPaymentToOrder() => SelectedPaymentMethod != null && PaymentAmount > 0;


    public Product? SelectedProduct
    {
        get { return selectedProduct; }
        set { selectedProduct = value; OnPropertyChanged(); }
    }
     public ICustomer SelectedCustomer
    {
        get { return selectedCustomer; }
        set { selectedCustomer = value; OnPropertyChanged(); }
    }
	public IPaymentMethod? SelectedPaymentMethod
	{
		get { return selectedPaymentMethod; }
		set { selectedPaymentMethod = value; OnPropertyChanged(); }
	}
    public OrderStatus OrderStatus
    {
        get { return orderStatus; }
        set { orderStatus = value; OnPropertyChanged(); }
    }
    public int LineNumber
    {
        get { return lineNumber; }
        set { lineNumber = value; OnPropertyChanged(); }
    }
    public OrderLine SelectedOrderLine
    {
        get { return selectedOrderLine; }
        set { selectedOrderLine = value; OnPropertyChanged(); }
    }
    public string NoteText
    {
        get { return noteText; }
        set { noteText = value; OnPropertyChanged(); }
    }
    public DateTime CollectionDateTime
    {
        get { return collectionDateTime; }
        set { collectionDateTime = value; OnPropertyChanged(); }
    }
    public string CollectionNeighborhood
    {
        get { return collectionNeighborhood; }
        set { collectionNeighborhood = value; OnPropertyChanged(); }
    }
    public decimal PaymentAmount
	{
		get { return paymentAmount; }
		set { paymentAmount = value; OnPropertyChanged(); }
	}
	public int SelectedQuantity
    {
        get { return selectedQuantity; }
        set { selectedQuantity = value; OnPropertyChanged(); }
    }
    public bool IsDelivery
    {
        get { return isDelivery; }
        set { isDelivery = value; OnPropertyChanged(); }
    }
	public decimal? OrderTotal
	{
		get { return orderTotal; }
		set { orderTotal = value; OnPropertyChanged(); }
	}
   public decimal OutstandingAmount
	{
		get { return outstandingAmount; }
		set { outstandingAmount = value; OnPropertyChanged(); }
	}

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
    public NewOrderViewModel(IOrderService orderService, INavigationService navigationService)
    {
        _orderservice = orderService;
        Navigation = navigationService;
        Navigation.NavigateToNested<NewOrderProductsViewModel>();
        NavigateToNewOrderRegistrationCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<NewOrderDetailsViewModel>(); }, canExecute => true);

        Products = new(_orderservice.ViewProductCatalogue());
        ProductsCollectionView = CollectionViewSource.GetDefaultView(Products);

        AddToOrderCommand = new RelayCommand((param) => AddProductToOrder(param), canExecute => CanAddToOrder());
    }

    private void AddNewOrder()
    {
        Order newOrder = new(DateTime.Now, OrderStatus.Draft, selectedCustomer.CustomerId);
        if (outstandingAmount == 0)
        {
            newOrder.Status = OrderStatus.FullyPaid;
        }
        ICollectionType collection = HandleCollectionType();
        INote orderNote = new Note(NoteText);
        _orderservice.CreateOrder(newOrder, _orderLines, _paymentMethods, _payments, collection, orderNote);

        MessageBox.Show($"Order {newOrder.OrderId} has been saved succesfully");

        //Nulstil felter
        OrderLines.Clear();
    }

    private void AddProductToOrder(object rowData)
    {
        Product product = rowData as Product;
        LineNumber = OrderLines.Count() + 1;
        
        // Create a new OrderLine
        OrderLine newOrderLine = new(product, LineNumber, 1, product.Price, 0);

        // Add the new OrderLine to the collection
        //_orderLines.Add(newOrderLine);
        OrderLines?.Add(newOrderLine);

        // Update the OrderTotal
        OrderTotal = (OrderLines?.Sum(ol => ol.Price * ol.Quantity));
        // Nulstil felter

    }

    private void AddPaymentToOrder()
    {
        // Create a new Payment
        Payment newPayment = new(PaymentAmount, DateTime.Now, SelectedPaymentMethod.PaymentMethodId);

        // Add the new Payment to the list
        _payments.Add(newPayment);
        _paymentMethods.Add(SelectedPaymentMethod);

        // Update the OutstandingAmount
        decimal totalPaid = _payments.Sum(p => p.PaymentAmount);
        OutstandingAmount = (OrderTotal ?? 0) - totalPaid;

        //Nulstil felter
        SelectedPaymentMethod = null;
        PaymentAmount = decimal.Zero;
    }

    private ICollectionType HandleCollectionType()
    {
        ICollectionType delivery;
        ICollectionType pickUp;
        if (IsDelivery) 
        {
            delivery = new Delivery (DateTime.Now, CollectionNeighborhood);
            return delivery;
        }
        else
        {
            pickUp = new PickUp (DateTime.Now);
            return pickUp;
        }
    }

}
