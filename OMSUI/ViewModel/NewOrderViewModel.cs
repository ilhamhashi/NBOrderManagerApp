using OrderManagerDesktopUI.ViewModel;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Service;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace OrderManagerLibrary.ViewModel;
public class NewOrderViewModel : ViewModelBase
{
    private readonly OrderService _orderservice;

    private Product? selectedProduct;
	private INote? orderNote;
	private ICustomer selectedCustomer;
	private IPaymentMethod? selectedPaymentMethod;
    private OrderStatus orderStatus;    
    private DateTime collectionDateTime;
    private string collectionNeighborhood;    
    private decimal paymentAmount;	
	private int? selectedQuantity;
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
    public ICommand CancelNewOrderCommand => new RelayCommand(execute => CancelNewOrder(), canExecute => CanCancelNewOrder());
    public ICommand ContinueToPaymentCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
    public ICommand GoBackToOrderDetailsCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
    public ICommand AddToOrderCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());
    public ICommand SelectProductCommand => new RelayCommand(execute => AddNewOrder(), canExecute => CanAddNewOrder());

    private bool CanAddNewOrder() => true; // Placeholder for actual logic
    private bool CanCancelOrder() => true; // Placeholder for actual logic
    private bool CanContinueToPayment() => true; // Placeholder for actual logic
    private bool CanGoBackToOrderDetails() => true; // Placeholder for actual logic
    private bool CanAddToOrder() => true; // Placeholder for actual logic
    private bool CanSelectProduct() => true; // Placeholder for actual logic
    private bool CanAddPaymentToOrder() => true;


    public Product? SelectedProduct
    {
        get { return selectedProduct; }
        set { selectedProduct = value; }
    }
    public INote? OrderNote
    {
        get { return orderNote; }
        set { orderNote = value; }
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
	public int? SelectedQuantity
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
        ICollectionType collection = HandleCollectionType();
        _orderservice.CreateOrder(newOrder, _orderLines, _paymentMethods, _payments, collection, OrderNote);

        MessageBox.Show($"Order {newOrder.OrderId} has been saved succesfully");
    }

    private void AddProductToOrder()
    {
        // opretter et orderline instans
        new orderline
        // opdaterer _orderlines så den inkluderer alle selectedproducts
        _orderlines.add()
    }

    private void AddPaymentToOrder()
    {
        //

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
