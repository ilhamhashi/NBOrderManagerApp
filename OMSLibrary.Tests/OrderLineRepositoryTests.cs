using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
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
        _db = new DataAccess.DataAccess(_config);
        _orderLineRepository = new OrderLineRepository(_db);
    }

    [TestMethod]
    public void InsertOrder_ShouldInsertOrderLineSuccesfully()
    {
        // Arrange
        var orderLine = new OrderLine(new Product(1),new Order(1),1,5,1,1,"standard","standard");

        // Act
        _orderLineRepository.Insert(orderLine);

        // Assert
        var retrievedOrderLine = _orderLineRepository.GetById(orderLine.Product.Id, orderLine.Order.Id);
        Assert.IsNotNull(retrievedOrderLine);
        Assert.AreEqual(orderLine.Product.Id, retrievedOrderLine.Product.Id);
        Assert.AreEqual(orderLine.Order.Id, retrievedOrderLine.Order.Id);
        Assert.AreEqual(orderLine.Quantity, retrievedOrderLine.Quantity);
        Assert.AreEqual(orderLine.Price, retrievedOrderLine.Price);
        Assert.AreEqual(orderLine.Discount, retrievedOrderLine.Discount);
        Assert.AreEqual(orderLine.Size, retrievedOrderLine.Size);
        Assert.AreEqual(orderLine.Taste, retrievedOrderLine.Taste);
    }

}
