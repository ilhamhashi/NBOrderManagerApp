using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;
namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class OrderRepositoryTests
{
    private IRepository<Order> _orderRepository;
    private IConfiguration _config;
    private IDataAccess _db;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _db = new DataAccess.DataAccess(_config);
        _orderRepository = new OrderRepository(_db);
    }

    [TestMethod]
    public void InsertOrder_ShouldInsertOrderSuccesfully()
    {

        // Arrange
        var order = new Order (DateTime.Now, new Customer(1), new PickUp(1), new Note(1));
       
        // Act
        order.Id = _orderRepository.Insert(order);

        // Assert
        var retrievedOrder = _orderRepository.GetById(order.Id);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(order.Date.ToString(), retrievedOrder.Date.ToString());
        Assert.AreEqual(order.Status, retrievedOrder.Status);
        Assert.AreEqual(order.Customer.Id, retrievedOrder.Customer.Id);
    }

    [TestMethod]
    public void UpdateOrder_ShouldUpdateOrderSuccesfully()
    {
        // Arrange
        var order = new Order(DateTime.Now, new Customer(1), new PickUp(1), new Note(1));
        int orderId = _orderRepository.Insert(order);

        // Act
        var updatedOrder = new Order(DateTime.Now, new Customer(2), new PickUp(1), new Note(1));
        _orderRepository.Update(updatedOrder);

        // Assert
        var retrievedOrder = _orderRepository.GetById(orderId);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(retrievedOrder.Date.ToString(), updatedOrder.Date.ToString());
        Assert.AreEqual(retrievedOrder.Status, updatedOrder.Status);
        Assert.AreNotEqual(retrievedOrder.Customer.Id, updatedOrder.Customer.Id);
    }

    [TestMethod]
    public void DeleteOrder_ShouldDeleteOrderSuccesfully()
    {
        // Arrange
        var order = new Order(DateTime.Now, new Customer(1), new PickUp(1), new Note(1));
        int orderId = _orderRepository.Insert(order);
        Assert.IsNotNull(_orderRepository.GetById(orderId));

        // Act
        _orderRepository.Delete(orderId);

        // Assert
        Assert.IsNull(_orderRepository.GetById(orderId));
    }

    [TestMethod]
    public void GetById_ShouldGetOrderByIdSuccesfully()
    {
        // Arrange
        var order = new Order(DateTime.Now, new Customer(1), new PickUp(1), new Note(1));
        int orderId = _orderRepository.Insert(order);

        // Act
        var retrievedOrder = _orderRepository.GetById(orderId);

        // Assert
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(retrievedOrder.Date.ToString(), order.Date.ToString());
        Assert.AreEqual(retrievedOrder.Status, order.Status);
        Assert.AreEqual(retrievedOrder.Customer, order.Customer);
    }
}
