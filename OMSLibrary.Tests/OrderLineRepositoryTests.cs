using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class OrderLineRepositoryTests
{
    private IRepository<OrderLine> _orderLineRepository;
    private IConfiguration _config;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _orderLineRepository = new OrderLineRepository(_config);
    }

    [TestMethod]
    public void InsertOrderLine_ShouldInsertOrderLineSuccesfully()
    {
        // Arrange
        var orderLine = new OrderLine
        (
            1,
            1,
            1,
            10,
            0
        );

        // Act
        _orderLineRepository.Insert(orderLine);

        // Assert
        var retrievedOrderLine = _orderLineRepository.GetById(orderLine.OrderId);
        Assert.IsNotNull(retrievedOrderLine);
        Assert.AreEqual(orderLine.ProductId, retrievedOrderLine.ProductId);
        Assert.AreEqual(orderLine.Quantity, retrievedOrderLine.Quantity);
        Assert.AreEqual(orderLine.Price, retrievedOrderLine.Price);
        Assert.AreEqual(orderLine.Discount, retrievedOrderLine.Discount);
    }

    [TestMethod]
    public void UpdateOrderLine_ShouldUpdateOrderLineSuccesfully()
    {
        // Arrange
        var orderLine = new OrderLine
        (
            1,
            1,
            1,
            10,
            0
        );

        // Act
        _orderLineRepository.Update(orderLine);

        // Assert
        var retrievedOrderLine = _orderLineRepository.GetById(orderLine.OrderId);
        Assert.IsNotNull(retrievedOrderLine);
        Assert.AreEqual(orderLine.ProductId, retrievedOrderLine.ProductId);
        Assert.AreEqual(orderLine.Quantity, retrievedOrderLine.Quantity);
        Assert.AreEqual(orderLine.Price, retrievedOrderLine.Price);
        Assert.AreEqual(orderLine.Discount, retrievedOrderLine.Discount);
    }

    [TestMethod]
    public void DeleteOrderLine_ShouldDeleteOrderLineSuccesfully()
    {
        // Arrange
        var orderLine = new OrderLine
        (
            1,
            1,
            1,
            10,
            0
        );

        // Act
        _orderLineRepository.Delete(1);

        // Assert
        var retrievedOrderLine = _orderLineRepository.GetById(orderLine.OrderId);
        Assert.IsNotNull(retrievedOrderLine);
        Assert.AreEqual(orderLine.ProductId, retrievedOrderLine.ProductId);
        Assert.AreEqual(orderLine.Quantity, retrievedOrderLine.Quantity);
        Assert.AreEqual(orderLine.Price, retrievedOrderLine.Price);
        Assert.AreEqual(orderLine.Discount, retrievedOrderLine.Discount);
    }

    [TestMethod]
    public void GetOrderLineById_ShouldGetOrderLineByIdSuccesfully()
    {
        // Arrange
        var orderLine = new OrderLine
        (
            1,
            1,
            1,
            10,
            0
        );

        // Act
        _orderLineRepository.Update(orderLine);

        // Assert
        var retrievedOrderLine = _orderLineRepository.GetById(orderLine.OrderId);
        Assert.IsNotNull(retrievedOrderLine);
        Assert.AreEqual(orderLine.ProductId, retrievedOrderLine.ProductId);
        Assert.AreEqual(orderLine.Quantity, retrievedOrderLine.Quantity);
        Assert.AreEqual(orderLine.Price, retrievedOrderLine.Price);
        Assert.AreEqual(orderLine.Discount, retrievedOrderLine.Discount);
    }
}
