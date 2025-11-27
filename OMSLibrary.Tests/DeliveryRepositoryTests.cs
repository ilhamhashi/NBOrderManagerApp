using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public class DeliveryRepositoryTests
{
    private IRepository<Delivery> _deliveryRepository;
    private ISqlDataAccess _dataAccess;
    private IConfigurationRoot _config;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _dataAccess = new SqlDataAccess(_config);
        _deliveryRepository = new DeliveryRepository(_dataAccess);
    }

    [TestMethod]
    public void InsertDelivery_ShouldInsertDeliverySuccesfully()
    {
        // Arrange
        var delivery = new Delivery
        (
            1,
            DateTime.Now,
            1,
            "Downtown"
        );

        // Act
        delivery.CollectionId = _deliveryRepository.Insert(delivery);

        // Assert
        var retrievedDelivery = _deliveryRepository.GetById(delivery.CollectionId);
        Assert.IsNotNull(retrievedDelivery);
        Assert.AreEqual(delivery.OrderId, retrievedDelivery.OrderId);
        Assert.AreEqual(delivery.Neighborhood, retrievedDelivery.Neighborhood);
    }

    [TestMethod]
    public void UpdateDelivery_ShouldUpdateDeliverySuccesfully()
    {
        // Arrange
        var delivery = new Delivery
        (
            1,
            DateTime.Now,
            1,
            "Downtown"
        );

        // Act
        delivery.CollectionId = _deliveryRepository.Insert(delivery);

        // Assert
        var retrievedDelivery = _deliveryRepository.GetById(delivery.CollectionId);
        Assert.IsNotNull(retrievedDelivery);
        Assert.AreEqual(delivery.OrderId, retrievedDelivery.OrderId);
        Assert.AreEqual(delivery.Neighborhood, retrievedDelivery.Neighborhood);
    }

    [TestMethod]
    public void DeleteDelivery_ShouldDeleteDeliverySuccesfully()
    {
        // Arrange
        var delivery = new Delivery
        (
            1,
            DateTime.Now,
            1,
            "Downtown"
        );

        // Act
        delivery.CollectionId = _deliveryRepository.Insert(delivery);

        // Assert
        var retrievedDelivery = _deliveryRepository.GetById(delivery.CollectionId);
        Assert.IsNotNull(retrievedDelivery);
        Assert.AreEqual(delivery.OrderId, retrievedDelivery.OrderId);
        Assert.AreEqual(delivery.Neighborhood, retrievedDelivery.Neighborhood);
    }

    [TestMethod]
    public void GetById_ShouldGetDeliverySuccesfully()
    {
        // Arrange
        var delivery = new Delivery
        (
            1,
            DateTime.Now,
            1,
            "Downtown"
        );

        // Act
        delivery.CollectionId = _deliveryRepository.Insert(delivery);

        // Assert
        var retrievedDelivery = _deliveryRepository.GetById(delivery.CollectionId);
        Assert.IsNotNull(retrievedDelivery);
        Assert.AreEqual(delivery.OrderId, retrievedDelivery.OrderId);
        Assert.AreEqual(delivery.Neighborhood, retrievedDelivery.Neighborhood);
    }
}
