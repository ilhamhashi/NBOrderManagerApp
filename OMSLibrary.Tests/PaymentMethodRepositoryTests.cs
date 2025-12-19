using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class PaymentMethodRepositoryTests
{
    private IRepository<PaymentMethod> PaymentMethodRepository;
    private IConfiguration _config;
    private IDataAccess _db;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _db = new DataAccess.DataAccess(_config);
        PaymentMethodRepository = new PaymentMethodRepository(_db);
    }

    [TestMethod]
    public void InsertMobilePaymentMethod_ShouldInsertSuccesfully()
    {
        // Arrange
        var payment = new PaymentMethod (1,"EVC");

        // Act
        payment.Id = PaymentMethodRepository.Insert(payment);

        // Assert
        var retrievedPayment = PaymentMethodRepository.GetById(payment.Id);
        Assert.IsNotNull(retrievedPayment);
        Assert.AreEqual(payment.Id, retrievedPayment.Id);
        Assert.AreEqual(payment.Name, retrievedPayment.Name);
    }

    [TestMethod]
    public void UpdateMobilePaymenMethod_ShouldUpdateSuccessfully()
    {

        // Arrange
        var payment = new PaymentMethod(0, "EVC");
        int id = PaymentMethodRepository.Insert(payment);

        var updatedPayment = new PaymentMethod(id, "Dahabshiil");

        // Act
        PaymentMethodRepository.Update(updatedPayment);

        // Assert
        var retrieved = PaymentMethodRepository.GetById(id);
        Assert.IsNotNull(retrieved);
        Assert.AreEqual("Dahabshiil", retrieved.Name);
    }
    [TestMethod]
    public void DeleteMobilePaymentMethod_ShouldDeleteSuccessfully()
    {
        // Arrange
        var payment = new PaymentMethod(0, "EVC");
        int id = PaymentMethodRepository.Insert(payment);
        Assert.IsNotNull(PaymentMethodRepository.GetById(id));

        // Act
        PaymentMethodRepository.Delete(id);

        // Assert
        Assert.IsNull(PaymentMethodRepository.GetById(id));
    }
    [TestMethod]
    public void GetById_ShouldReturnMobilePaymentMethodSuccessfully()
    {
        // Arrange
        var payment = new PaymentMethod(0, "EVC");
        int id = PaymentMethodRepository.Insert(payment);

        // Act
        var retrieved = PaymentMethodRepository.GetById(id);

        // Assert
        Assert.IsNotNull(retrieved);
        Assert.AreEqual(payment.Name, retrieved.Name);
    }
}
