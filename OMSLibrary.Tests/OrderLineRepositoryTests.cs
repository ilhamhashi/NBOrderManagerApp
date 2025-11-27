using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccessNS;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class OrderLineRepositoryTests
{
    private IRepository<OrderLine> _orderLineRepository;
    private IConfiguration _config;
    private IDataAccess _db;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _db = new DataAccess(_config);
        _orderLineRepository = new OrderLineRepository(_db);
    }

    [TestMethod]
    public void InsertOrder_ShouldInsertOrderLineSuccesfully()
    {
        // Arrange
        var orderLine = new OrderLine(1,5,1,1,10,0);

        // Act
        _orderLineRepository.Insert(orderLine);

        // Assert
        var retrievedOrderLine = _orderLineRepository.GetById(orderLine.ProductId, orderLine.OrderId);
        Assert.IsNotNull(retrievedOrderLine);
        Assert.AreEqual(orderLine.ProductId, retrievedOrderLine.ProductId);
        Assert.AreEqual(orderLine.OrderId, retrievedOrderLine.OrderId);
        Assert.AreEqual(orderLine.Quantity, retrievedOrderLine.Quantity);
        Assert.AreEqual(orderLine.Price, retrievedOrderLine.Price);
        Assert.AreEqual(orderLine.Discount, retrievedOrderLine.Discount);
    }

}
