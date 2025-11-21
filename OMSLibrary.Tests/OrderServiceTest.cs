using Microsoft.Data.SqlClient;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public class OrderRepositoryTests
{
    private SqlConnection _connection;
    private OrderRepository _orderRepository;

    [TestInitialize]
    public void Setup()
    {
        _connection = new SqlConnection("DinConnectionString");
        _orderRepository = new OrderRepository(_connection);
        _connection.Open();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _connection.Close();
    }

    [TestMethod]
    public void AddOrder_ShouldAddOrderSuccessfully()
    {
        // Arrange
        var order = new Order
        {
            OrderId = 1,
            OrderDate = DateTime.Now,
            IsDraft = default,
            CustomerId = 1
        };

        // Act
        _orderRepository.Insert(order);

        // Assert
        var retrievedOrder = _orderRepository.GetById(order.OrderId);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
    }
}
