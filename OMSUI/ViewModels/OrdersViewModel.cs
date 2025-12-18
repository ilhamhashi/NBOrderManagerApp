using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
namespace OrderManagerDesktopUI.ViewModels;

public class OrdersViewModel : ViewModelBase
{

    private readonly IOrderService _orderService;
    public ObservableCollection<Order> Orders { get; } = [];

    private Order _selectedOrder;
    public Order SelectedOrder
    {
        get { return _selectedOrder; }
        set { _selectedOrder = value; OnPropertyChanged(); }
    }

    public OrdersViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        Orders = new ObservableCollection<Order>(_orderService.GetAllOrders());
    }
}