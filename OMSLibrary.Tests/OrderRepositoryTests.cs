using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
namespace OMSLibrary.Tests;

[TestClass]
public sealed class OrderRepositoryTests
{
    private IRepository<Order> _orderRepository;
    private ISqlDataAccess _dataAccess;
    private IConfigurationRoot _config;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _dataAccess = new SqlDataAccess(_config);
        _orderRepository = new OrderRepository(_dataAccess);
    }

    [TestMethod]
    public void InsertOrder_ShouldInsertOrderSuccesfully()
    {
        // Arrange
        var order = new Order
        (
            DateTime.Now,
            OrderStatus.Confirmed,
            1
        );

        // Act
        order.OrderId = _orderRepository.Insert(order);

        // Assert
        var retrievedOrder = _orderRepository.GetById(order.OrderId);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
        Assert.AreEqual(order.Status, retrievedOrder.Status);
    }

    [TestMethod]
    public void UpdateOrder_ShouldUpdateOrderSuccesfully()
    {
        // Arrange
        var order = new Order
        (
            DateTime.Now,
            OrderStatus.Confirmed,
            1
        );

        // Act
        order.OrderId = _orderRepository.Insert(order);

        // Assert
        var retrievedOrder = _orderRepository.GetById(order.OrderId);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
        Assert.AreEqual(order.Status, retrievedOrder.Status);
    }

    [TestMethod]
    public void DeleteOrder_ShouldDeleteOrderSuccesfully()
    {
        // Arrange
        var order = new Order
        (
            DateTime.Now,
            OrderStatus.Confirmed,
            1
        );

        // Act
        order.OrderId = _orderRepository.Insert(order);

        // Assert
        var retrievedOrder = _orderRepository.GetById(order.OrderId);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
        Assert.AreEqual(order.Status, retrievedOrder.Status);
    }

    [TestMethod]
    public void GetById_ShouldGetOrderSuccesfully()
    {
        // Arrange
        var order = new Order
        (
            DateTime.Now,
            OrderStatus.Confirmed,
            1
        );

        // Act
        order.OrderId = _orderRepository.Insert(order);

        // Assert
        var retrievedOrder = _orderRepository.GetById(order.OrderId);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
        Assert.AreEqual(order.Status, retrievedOrder.Status);
    }
}
