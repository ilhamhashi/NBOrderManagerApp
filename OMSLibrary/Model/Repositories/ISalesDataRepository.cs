namespace OrderManagerLibrary.Model.Repositories;
public interface ISalesDataRepository
{
    int GetMonthlyOrdersCount();
    decimal GetMonthlyRevenue();
    decimal GetWeeklyRevenue();
}