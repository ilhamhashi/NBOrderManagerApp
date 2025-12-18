using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class MobilePaymentRepositoryTests
{
    private IRepository<PaymentMethod> _mobilePaymentRepository;
    private IConfiguration _config;
    private IDataAccess _db;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _db = new DataAccess.DataAccess(_config);
        _mobilePaymentRepository = new PaymentMethodRepository(_db);
    }

    [TestMethod]
    public void InsertMobilePaymentMethod_ShouldInsertSuccesfully()
    {
        // Arrange
        var payment = new PaymentMethod (1,"EVC");

        // Act
        payment.Id = _mobilePaymentRepository.Insert(payment);

        // Assert
        var retrievedPayment = _mobilePaymentRepository.GetById(payment.Id);
        Assert.IsNotNull(retrievedPayment);
        Assert.AreEqual(payment.Id, retrievedPayment.Id);
        Assert.AreEqual(payment.Name, retrievedPayment.Name);
    }

    [TestMethod]
    public void UpdateMobilePaymenMethod_ShouldUpdateSuccessfully()
    {

        // Arrange
        var payment = new PaymentMethod(0, "EVC");
        int id = _mobilePaymentRepository.Insert(payment);

        var updatedPayment = new PaymentMethod(id, "Dahabshiil");

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
        var payment = new PaymentMethod(0, "EVC");
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
        var payment = new PaymentMethod(0, "EVC");
        int id = _mobilePaymentRepository.Insert(payment);

        // Act
        var retrieved = _mobilePaymentRepository.GetById(id);

        // Assert
        Assert.IsNotNull(retrieved);
        Assert.AreEqual(payment.Name, retrieved.Name);
    }
}
