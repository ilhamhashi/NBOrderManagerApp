using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagerLibrary.DataAccess;
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
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _db = new DataAccess.DataAccess(_config);
        _mobilePaymentRepository = new MobilePaymentRepository(_db);
    }

    [TestMethod]
    public void Insert_ShouldInsertMobilePaymentSuccessfully()
    {
        // Arrange
        var payment = new MobilePayment(0, "MobilePay");

        // Act
        payment.PaymentMethodId = _mobilePaymentRepository.Insert(payment);

        // Assert
        var retrievedPayment = _mobilePaymentRepository.GetById(payment.PaymentMethodId);
        Assert.IsNotNull(retrievedPayment);
        Assert.AreEqual(payment.PaymentMethodId, retrievedPayment.PaymentMethodId);
        Assert.AreEqual(payment.Name, retrievedPayment.Name);
    }

    [TestMethod]
    public void GetById_ShouldGetExistingMobilePaymentSuccessfully()
    {
        // Arrange
        var payment = new MobilePayment(0, "MobilePay");
        int id = _mobilePaymentRepository.Insert(payment);

        // Act
        var retrievedPayment = _mobilePaymentRepository.GetById(id);

        // Assert
        Assert.IsNotNull(retrievedPayment);
        Assert.AreEqual(payment.Name, retrievedPayment.Name);
    }

    [TestMethod]
    public void GetAll_ShouldGetAllMobilePaymentsSuccessfully()
    {
        // Arrange
        var payments = new List<MobilePayment>();

        // Act
        payments.AddRange(_mobilePaymentRepository.GetAll());

        // Assert
        Assert.IsNotNull(payments);
        // Hvis du vil være strengere:
        // Assert.IsTrue(payments.Count > 0);
    }

    [TestMethod]
    public void Update_ShouldUpdateMobilePaymentSuccessfully()
    {
        // Arrange
        var payment = new MobilePayment(0, "MobilePay");
        int id = _mobilePaymentRepository.Insert(payment);

        // Act
        var updatedPayment = new MobilePayment(id, "Dahabshiil");
        _mobilePaymentRepository.Update(updatedPayment);

        // Assert
        var retrievedPayment = _mobilePaymentRepository.GetById(id);
        Assert.IsNotNull(retrievedPayment);
        Assert.AreEqual(updatedPayment.PaymentMethodId, retrievedPayment.PaymentMethodId);
        Assert.AreEqual(updatedPayment.Name, retrievedPayment.Name);
    }

    [TestMethod]
    public void Delete_ShouldDeleteMobilePaymentSuccessfully()
    {
        // Arrange
        var payment = new MobilePayment(0, "MobilePay");
        payment.PaymentMethodId = _mobilePaymentRepository.Insert(payment);
        Assert.IsNotNull(_mobilePaymentRepository.GetById(payment.PaymentMethodId));

        // Act
        _mobilePaymentRepository.Delete(payment.PaymentMethodId);

        // Assert
        var retrievedPayment = _mobilePaymentRepository.GetById(payment.PaymentMethodId);
        Assert.IsNull(retrievedPayment);
    }
}
