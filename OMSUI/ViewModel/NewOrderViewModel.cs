using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Service;
using System.Collections.ObjectModel;

namespace OrderManagerLibrary.ViewModel;
public class NewOrderViewModel
{
    private readonly OrderService _orderservice;

    private Product? selectedProduct;
	private Order? newOrder;
	private INote? orderNote;
	private ICustomer? selectedCustomer;
	private ICollectionType? collection;
	private IPaymentMethod? selectedPaymentMethod;    
	private decimal paymentAmount;	
	private int? selectedQuantity;
	private bool isDelivery;    
	private decimal? orderTotal;
	private decimal outstandingAmount;	

	private List<IPaymentMethod>? _paymentMethods {  get; set; }
	private List<Payment>? _payments { get; set; }
    private List<OrderLine> _orderLines { get; set; }
    public ObservableCollection<OrderLine>? OrderLines { get; set; }    
    public ObservableCollection<Product>? Products;

    public Product? SelectedProduct
    {
        get { return selectedProduct; }
        set { selectedProduct = value; }
    }
    public Order? NewOrder
    {
        get { return newOrder; }
        set { newOrder = value; }
    }
    public INote? OrderNote
    {
        get { return orderNote; }
        set { orderNote = value; }
    }
     public ICustomer? SelectedCustomer
    {
        get { return selectedCustomer; }
        set { selectedCustomer = value; }
    }
	public ICollectionType? CollectionType
    {
        get { return collection; }
        set { collection = value; }
    }
	public IPaymentMethod? SelectedPaymentMethod
	{
		get { return selectedPaymentMethod; }
		set { selectedPaymentMethod = value; }
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
        _orderservice.CreateOrder(NewOrder, _orderLines, _paymentMethods, _payments, CollectionType, OrderNote);
    }

}
