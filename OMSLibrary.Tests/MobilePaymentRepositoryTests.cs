using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]


public sealed class MobilePaymentRepositoryTests
{
    private IRepository<MobilePaymentMethod> _mobilePaymentMethodRepository;
    private ISqlDataAccess _dataAccess;
    private IConfigurationRoot _config;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _dataAccess = new SqlDataAccess(_config);
        _mobilePaymentMethodRepository = new MobilePaymentMethodRepository(_dataAccess);
    }
    [TestMethod]

    public void InsertMobilePaymentMethod_ShouldInsertSuccesfully()
    {
        // Arrange
        var payment = new MobilePaymentMethod
        (
            1,
            "EVC"
        );

        // Act
        payment.PaymentMethodId = _mobilePaymentMethodRepository.Insert(payment);


        // Assert
        var retrievedPayment = _mobilePaymentMethodRepository.GetById(payment.PaymentMethodId);
        Assert.IsNotNull(retrievedPayment);
        Assert.AreEqual(payment.PaymentMethodId, retrievedPayment.PaymentMethodId);
    }
    [TestMethod]
    public void UpdateMobilePaymenMethod_ShouldUpdateSuccessfully()
    {
       
        // Arrange
        var payment = new MobilePaymentMethod(0, "EVC");
        int id = _mobilePaymentMethodRepository.Insert(payment);

        var updatedPayment = new MobilePaymentMethod(id, "Dahabshiil");

        // Act
        _mobilePaymentMethodRepository.Update(updatedPayment);

        // Assert
        var retrieved = _mobilePaymentMethodRepository.GetById(id);
        Assert.IsNotNull(retrieved);
        Assert.AreEqual("Dahabshiil", retrieved.Name);
    }
    [TestMethod]
    public void DeleteMobilePaymentMethod_ShouldDeleteSuccessfully()
    {
        // Arrange
        var payment = new MobilePaymentMethod(0, "EVC");
        int id = _mobilePaymentMethodRepository.Insert(payment);
        Assert.IsNotNull(_mobilePaymentMethodRepository.GetById(id));

        // Act
        _mobilePaymentMethodRepository.Delete(id);

        // Assert
        Assert.IsNull(_mobilePaymentMethodRepository.GetById(id));
    }
    [TestMethod]
    public void GetById_ShouldReturnMobilePaymentMethodSuccessfully()
    {
        // Arrange
        var payment = new MobilePaymentMethod(0, "EVC");
        int id = _mobilePaymentMethodRepository.Insert(payment);

        // Act
        var retrieved = _mobilePaymentMethodRepository.GetById(id);

        // Assert
        Assert.IsNotNull(retrieved);
        Assert.AreEqual(payment.Name, retrieved.Name);
    }

}

