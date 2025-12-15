using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Services.Interfaces;
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
    private Payment? selectedPayment;
    private int lineNumber;
    private OrderLine? selectedOrderLine;
    private string noteText = string.Empty;
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

    private DateTime pickUpDateTime = DateTime.Today.AddDays(1);
    private string location = "store";    
    private decimal paymentAmount;
	private bool isDelivery = false;    
	private decimal runningTotal;
	private decimal outstandingAmount;
    private decimal? discountTotal;

    public decimal? DiscountTotal
    {
        get { return discountTotal; }
        set { discountTotal = value; OnPropertyChanged(); }
    }



    private ObservableCollection<IPaymentMethod> _paymentMethods { get; } = [];
    private ObservableCollection<Payment> _payments { get; set; } = [];
    private ObservableCollection<OrderLine> OrderLines { get; set; } = [];
    private ObservableCollection<Product> _products { get; }
    public ICollectionView ProductsCollectionView { get;}
    public ICollectionView OrderLinesCollectionView { get; } 
    

    public ICommand CreateOrderCommand { get; }
    public ICommand AddPaymentCommand {  get; }
    public ICommand AddProductToOrderCommand {  get; }

    public ICommand IncreaseQuantityCommand { get; }
    public ICommand DecreaseQuantityCommand { get; }
    public ICommand AddDiscountCommand { get; }
    public ICommand AddPriceIncreaseCommand { get; }

    public ICommand NavigateToNoteViewCommand { get; }
    public ICommand NavigateToPriceChangeViewCommand { get; }
    public ICommand NavigateToPaymentViewCommand { get; }
    public ICommand NavigateToDeliveryViewCommand { get; }

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
    public int LineNumber
    {
        get { return lineNumber; }
        set { lineNumber = value; OnPropertyChanged(); }
    }
    public OrderLine? SelectedOrderLine
    {
        get { return selectedOrderLine; }
        set { selectedOrderLine = value; OnPropertyChanged(); }
    }

    public string NoteText
    {
        get { return noteText; }
        set { noteText = value; OnPropertyChanged(); }
    }
    public DateTime PickUpDateTime
    {
        get { return pickUpDateTime; }
        set { pickUpDateTime = value; OnPropertyChanged(); }
    }
    public string Location
    {
        get { return location; }
        set { location = value; OnPropertyChanged(); }
    }
    public decimal PaymentAmount
	{
		get { return paymentAmount; }
		set { paymentAmount = value; OnPropertyChanged(); }
	}

    public Payment? SelectedPayment
    {
        get { return selectedPayment; }
        set { selectedPayment = value; }
    }

    public bool IsDelivery
    {
        get { return isDelivery; }
        set { isDelivery = value; OnPropertyChanged(); }
    }
	public decimal RunningTotal
	{
		get { return runningTotal; }
		set { runningTotal = value; OnPropertyChanged(); }
	}
   public decimal OutstandingAmount
	{
		get { return outstandingAmount; }
		set { outstandingAmount = value; OnPropertyChanged(); }
	}

    private decimal priceIncreaseAmount;

    public decimal PriceIncreaseAmount
    {
        get { return priceIncreaseAmount; }
        set { priceIncreaseAmount = value; OnPropertyChanged(); }
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
    public NewOrderViewModel(IOrderService orderService, IProductService productService, INavigationService navigationService)
    {
        _orderservice = orderService;
        _products = new ObservableCollection<Product>(productService.GetAllProducts());
        ProductsCollectionView = CollectionViewSource.GetDefaultView(_products);
        OrderLinesCollectionView = CollectionViewSource.GetDefaultView(OrderLines);
        Navigation = navigationService;
        Navigation.NavigateToNested<NoteViewModel>();

        NavigateToNoteViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<NoteViewModel>(); }, canExecute => true);

        NavigateToPriceChangeViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<PriceChangeViewModel>(); }, canExecute => true);


        NavigateToDeliveryViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<DeliveryViewModel>(); }, canExecute => true);

        NavigateToPaymentViewCommand = new RelayCommand(
            execute => { Navigation.NavigateToNested<PaymentViewModel>(); }, canExecute => true);

        
        CreateOrderCommand = new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
        AddPaymentCommand = new RelayCommand(execute => AddPaymentToOrder(), canExecute => CanAddPaymentToOrder());
        AddProductToOrderCommand = new RelayCommand((param) => AddProductToOrder(param), canExecute => CanAddToOrder());
        IncreaseQuantityCommand = new RelayCommand((param) => IncreaseQuantity(param), canExecute => true);
        DecreaseQuantityCommand = new RelayCommand((param) => DecreaseQuantity(param), canExecute => true);
        AddDiscountCommand = new RelayCommand(execute => AddDiscountToOrderLine(), canExecute => true);
        AddPriceIncreaseCommand = new RelayCommand(execute => AddPriceIncreaseToOrderLine(), canExecute => true);
    }

    private void AddNewOrder()
    {
        Customer customer = new Customer(1, "FirstName", "LastName", "Phone");
        ;
        PickUp pickUp = new PickUp(DateTime.Now, IsDelivery, Location);
        Note note = new Note(NoteText);
        Order newOrder = new(DateTime.Now, OrderStatus.Draft, customer, pickUp, note);
        if (outstandingAmount == 0)
        {
            newOrder.Status = OrderStatus.FullyPaid;
        }

        _orderservice.CreateOrder(newOrder, [.. OrderLines], [.. _payments]);

        MessageBox.Show($"Order {newOrder.Id} has been saved succesfully");
        
        ResetFieldsAfterOrderCompletion();
    }

    private void ResetFieldsAfterOrderCompletion()
    {
        OrderLines.Clear();
        SelectedOrderLine = null;
        SelectedPaymentMethod = null;
        _payments.Clear();
        SelectedPayment = null;
        PaymentAmount = decimal.Zero;
        NoteText = string.Empty;
        PickUpDateTime = DateTime.Today.AddDays(1);
        isDelivery = false;
        Location = "store";
        PriceIncreaseAmount = decimal.Zero;        
    }

    private void AddProductToOrder(object rowData)
    {
        Product product = rowData as Product;
        OrderLine newOrderLine = new(product, 1, product.Price, 0, product.Taste.Name, product.Size.Name);
        ResetLineNumber();
        newOrderLine.LineNumber = OrderLines.Count() + 1;
        OrderLines?.Add(newOrderLine);
        UpdateRunningTotal(); UpdateDiscountTotal(); UpdateOutstandingAmount();
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
        DiscountTotal = (OrderLines?.Sum(ol => ol.Discount * ol.Quantity));
    }

    private void IncreaseQuantity(object rowOrderLine)
    {
        (rowOrderLine as OrderLine).IncreaseQuantity();
        UpdateRunningTotal(); UpdateDiscountTotal(); UpdateOutstandingAmount();
        OrderLinesCollectionView.Refresh();
    }

    private void DecreaseQuantity(object rowOrderLine)
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
        OrderLinesCollectionView.Refresh();
    }

    private void AddDiscountToOrderLine()
    {
        SelectedOrderLine.ApplyDiscount();
        UpdateRunningTotal(); UpdateDiscountTotal();
        OrderLinesCollectionView.Refresh();
    }

    private void AddPriceIncreaseToOrderLine()
    {
        SelectedOrderLine.IncreasePrice(PriceIncreaseAmount);
        UpdateRunningTotal();
        OrderLinesCollectionView.Refresh();
    }

    private void AddPaymentToOrder()
    {
        Payment newPayment = new(PaymentAmount, DateTime.Now, SelectedPaymentMethod);
        _payments.Add(newPayment);
        UpdateOutstandingAmount();

        SelectedPaymentMethod = null;
        PaymentAmount = decimal.Zero;
    }

    private void UpdateOutstandingAmount()
    {
        decimal totalPaid = _payments.Sum(p => p.Amount);
        OutstandingAmount = RunningTotal - totalPaid;
    }

}
