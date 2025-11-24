using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels;
public class NewOrderViewModel : ViewModelBase
{
    private readonly OrderService _orderservice;

    private Product? selectedProduct;
	private ICustomer selectedCustomer;
	private IPaymentMethod? selectedPaymentMethod;
    private OrderStatus orderStatus;
    private string noteText;
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
    public ObservableCollection<OrderLine>? OrderLines { get; set; }    
    public ObservableCollection<Product>? Products;

    public ICommand CreateOrderCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
    public ICommand AddPaymentCommand => new RelayCommand(execute => AddPaymentToOrder(), canExecute => CanAddPaymentToOrder());
    public ICommand AddToOrderCommand => new RelayCommand(execute => AddProductToOrder(), canExecute => CanAddToOrder());

    // Navigation Commands - to be implemented
    //public ICommand CancelNewOrderCommand => new RelayCommand(execute => CancelNewOrder(), canExecute => CanCancelNewOrder());
    //public ICommand ContinueToPaymentCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
    //public ICommand GoBackToOrderDetailsCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
    //public ICommand SelectProductCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());

    private bool CanAddNewOrder() => true; // Placeholder for actual logic
    private bool CanCancelNewOrder() => true; // Placeholder for actual logic
    private bool CanContinueToPayment() => true; // Placeholder for actual logic
    private bool CanGoBackToOrderDetails() => true; // Placeholder for actual logic
    private bool CanAddToOrder() => SelectedQuantity > 1 && SelectedProduct != null;
    private bool CanSelectProduct() => true; // Placeholder for actual logic
    private bool CanAddPaymentToOrder() => SelectedPaymentMethod != null && PaymentAmount > 0;


    public Product? SelectedProduct
    {
        get { return selectedProduct; }
        set { selectedProduct = value; }
    }
     public ICustomer SelectedCustomer
    {
        get { return selectedCustomer; }
        set { selectedCustomer = value; }
    }
	public IPaymentMethod? SelectedPaymentMethod
	{
		get { return selectedPaymentMethod; }
		set { selectedPaymentMethod = value; }
	}
    public OrderStatus OrderStatus
    {
        get { return orderStatus; }
        set { orderStatus = value; }
    }
    public string NoteText
    {
        get { return noteText; }
        set { noteText = value; }
    }
    public DateTime CollectionDateTime
    {
        get { return collectionDateTime; }
        set { collectionDateTime = value; }
    }
    public string CollectionNeighborhood
    {
        get { return collectionNeighborhood; }
        set { collectionNeighborhood = value; }
    }
    public decimal PaymentAmount
	{
		get { return paymentAmount; }
		set { paymentAmount = value; }
	}
	public int SelectedQuantity
    {
        get { return selectedQuantity; }
        set { selectedQuantity = value; }
    }
    public bool IsDelivery
    {
        get { return isDelivery; }
        set { isDelivery = value; }
    }
	public decimal? OrderTotal
	{
		get { return orderTotal; }
		set { orderTotal = value; }
	}
   public decimal OutstandingAmount
	{
		get { return outstandingAmount; }
		set { outstandingAmount = value; }
	}

    public NewOrderViewModel(OrderService orderservice)
    {
        _orderservice = orderservice;
    }

    private void AddNewOrder()
    {
        Order newOrder = new(DateTime.Now, OrderStatus.Draft, selectedCustomer.CustomerId);
        if (outstandingAmount == 0)
        {
            newOrder.Status = OrderStatus.Confirmed;
        }
        ICollectionType collection = HandleCollectionType();
        INote orderNote = new Note(NoteText);
        _orderservice.CreateOrder(newOrder, _orderLines, _paymentMethods, _payments, collection, orderNote);

        MessageBox.Show($"Order {newOrder.OrderId} has been saved succesfully");

        //Nulstil felter
    }

    private void AddProductToOrder()
    {
        // Create a new OrderLine
        OrderLine newOrderLine = new(SelectedProduct.ProductId, SelectedQuantity, SelectedProduct.Price);
        newOrderLine.ReducePrice();
        // Add the new OrderLine to the collection
        _orderLines.Add(newOrderLine);
        OrderLines?.Add(newOrderLine);

        // Update the OrderTotal
        OrderTotal = (_orderLines.Sum(ol => ol.Price * ol.Quantity));

        // Nulstil felter
        SelectedProduct = null;
        SelectedQuantity = 1;
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
