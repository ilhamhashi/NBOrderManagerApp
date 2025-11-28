using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class DeliveryRepositoryTests
{
    private IRepository<Delivery> _deliveryRepository;
    private IConfiguration _config;
    private IDataAccess _db;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _db = new DataAccess.DataAccess(_config);
        _deliveryRepository = new DeliveryRepository(_db);
    }

    [TestMethod]
    public void InsertDelivery_ShouldInsertDeliverySuccesfully()
    {
        // Arrange
        var delivery = new Delivery (DateTime.Now, 1, "Downtown");

        // Act
        delivery.CollectionId = _deliveryRepository.Insert(delivery);

        // Assert
        var retrievedDelivery = _deliveryRepository.GetById(delivery.CollectionId);
        Assert.IsNotNull(retrievedDelivery);
        Assert.AreEqual(delivery.CollectionDate.ToString(), retrievedDelivery.CollectionDate.ToString());
        Assert.AreEqual(delivery.OrderId, retrievedDelivery.OrderId);
        Assert.AreEqual(delivery.Neighborhood, retrievedDelivery.Neighborhood);
    }

    [TestMethod]
    public void GetById_ShouldGetDeliverySuccesfully()
    {
        // Arrange
        var delivery = new Delivery(DateTime.Now, 2, "Downtown");
        int deliveryId = _deliveryRepository.Insert(delivery);

        // Act
        var updatedDelivery = new Delivery(DateTime.Now, 5, "Uptown");
        _deliveryRepository.Update(updatedDelivery);

        // Assert
        var retrievedDelivery = _deliveryRepository.GetById(deliveryId);
        Assert.IsNotNull(retrievedDelivery);
        Assert.AreEqual(updatedDelivery.CollectionDate.ToString(), retrievedDelivery.CollectionDate.ToString());
        Assert.AreEqual(updatedDelivery.OrderId, retrievedDelivery.OrderId);
        Assert.AreEqual(updatedDelivery.Neighborhood, retrievedDelivery.Neighborhood);
    }

    [TestMethod]
    public void GetAll_ShouldGetAllCustomerInfoSuccesfully()
    {
        // Arrange
        var delivery = new Delivery(DateTime.Now, 2, "Downtown");
        int deliveryId = _deliveryRepository.Insert(delivery);
        Assert.IsNotNull(_deliveryRepository.GetById(deliveryId));

        // Act
        _deliveryRepository.Delete(delivery);

        // Assert
        Assert.IsNull(_deliveryRepository.GetById(delivery));
    }

    [TestMethod]
    public void UpdateDelivery_ShouldUpdateDeliverySuccesfully()
    {
        // Arrange
        var delivery = new Delivery(DateTime.Now, 2, "Downtown");
        int deliveryId = _deliveryRepository.Insert(delivery);

        // Act
        var retrievedDelivery = _deliveryRepository.GetById(deliveryId);

        // Assert
        Assert.IsNotNull(retrievedDelivery);
        Assert.AreEqual(delivery.CollectionDate.ToString(), retrievedDelivery.CollectionDate.ToString());
        Assert.AreEqual(delivery.OrderId, retrievedDelivery.OrderId);
        Assert.AreEqual(delivery.Neighborhood, retrievedDelivery.Neighborhood);
    }

    [TestMethod]
    public void DeleteDelivery_ShouldDeleteDeliverySuccesfully()
    {
        // Arrange
        var delivery = new Delivery(DateTime.Now, "Xgade");
        int collectionId = _deliveryRepository.Insert(delivery);

        // Act
        _deliveryRepository.Delete(delivery);

        // Assert
        Assert.IsNull(_deliveryRepository.GetById(delivery));
    }
}
