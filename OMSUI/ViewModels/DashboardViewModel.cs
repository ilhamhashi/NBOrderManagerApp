using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;

namespace OrderManagerDesktopUI.ViewModels;
public class DashboardViewModel : ViewModelBase
{
    private readonly IOrderService _orderService;
    public ObservableCollection<Order> UpcomingOrders { get; } = [];
    public ObservableCollection<Order> PendingPaymentOrders { get; } = [];

    private decimal weeklyRevenue;
    public decimal WeeklyRevenue
    {
        get { return weeklyRevenue; }
        set { weeklyRevenue = value; OnPropertyChanged(); }
    }

    private decimal monthlyRevenue;
    public decimal MonthlyRevenue
    {
        get { return monthlyRevenue; }
        set { monthlyRevenue = value; OnPropertyChanged(); }
    }

    private Product mostPopularProduct;
    public Product MostPopularProduct
    {
        get { return mostPopularProduct; }
        set { mostPopularProduct = value; OnPropertyChanged(); }
    }

    private int monthlyOrdersCount;
    public int MonthlyOrdersCount
    {
        get { return monthlyOrdersCount; }
        set { monthlyOrdersCount = value; OnPropertyChanged(); }
    }

    public DashboardViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        UpcomingOrders = new ObservableCollection<Order>(_orderService.GetUpcomingOrders());
        PendingPaymentOrders = new ObservableCollection<Order>(_orderService.GetPendingPaymentOrders());
        WeeklyRevenue = _orderService.GetWeeklyRevenue();
        MonthlyRevenue = _orderService.GetMonthlyRevenue();
        MonthlyOrdersCount = _orderService.GetMonthlyOrdersCount();
    }
}
