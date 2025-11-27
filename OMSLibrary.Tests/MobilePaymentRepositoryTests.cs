using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccessNS;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class MobilePaymentRepositoryTests
{
    private IRepository<MobilePayment> _mobilePaymentRepository;
    private IConfiguration _config;
    private IDataAccess _db;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _db = new DataAccess(_config);
        _mobilePaymentRepository = new MobilePaymentRepository(_db);
    }

    [TestMethod]
    public void InsertMobilePaymentMethod_ShouldInsertSuccesfully()
    {
        // Arrange
        var payment = new MobilePayment (1,"EVC");

        // Act
        payment.PaymentMethodId = _mobilePaymentRepository.Insert(payment);

        // Assert
        var retrievedPayment = _mobilePaymentRepository.GetById(payment.PaymentMethodId);
        Assert.IsNotNull(retrievedPayment);
        Assert.AreEqual(payment.PaymentMethodId, retrievedPayment.PaymentMethodId);
        Assert.AreEqual(payment.Name, retrievedPayment.Name);
    }

    [TestMethod]
    public void UpdateMobilePaymenMethod_ShouldUpdateSuccessfully()
    {

        // Arrange
        var payment = new MobilePayment(0, "EVC");
        int id = _mobilePaymentRepository.Insert(payment);

        var updatedPayment = new MobilePayment(id, "Dahabshiil");

        // Act
        _mobilePaymentRepository.Update(updatedPayment);

        // Assert
        var retrieved = _mobilePaymentRepository.GetById(id);
        Assert.IsNotNull(retrieved);
        Assert.AreEqual("Dahabshiil", retrieved.Name);
    }
    [TestMethod]
    public void DeleteMobilePaymentMethod_ShouldDeleteSuccessfully()
    {
        // Arrange
        var payment = new MobilePayment(0, "EVC");
        int id = _mobilePaymentRepository.Insert(payment);
        Assert.IsNotNull(_mobilePaymentRepository.GetById(id));

        // Act
        _mobilePaymentRepository.Delete(id);

        // Assert
        Assert.IsNull(_mobilePaymentRepository.GetById(id));
    }
    [TestMethod]
    public void GetById_ShouldReturnMobilePaymentMethodSuccessfully()
    {
        // Arrange
        var payment = new MobilePayment(0, "EVC");
        int id = _mobilePaymentRepository.Insert(payment);

        // Act
        var retrieved = _mobilePaymentRepository.GetById(id);

        // Assert
        Assert.IsNotNull(retrieved);
        Assert.AreEqual(payment.Name, retrieved.Name);
    }
}
