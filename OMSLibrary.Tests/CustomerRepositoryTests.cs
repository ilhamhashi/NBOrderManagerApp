using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class CustomerRepositoryTests
{
    private IRepository<Customer> _customerRepository;
    private IConfiguration _config;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _customerRepository = new CustomerRepository(_config);
    }

    [TestMethod]
    public void Insert_ShouldInsertCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer
        (
            "John",
            "Doe",
            "12345678"
        );

        // Act
        customer.CustomerId = _customerRepository.Insert(customer);

        // Assert
        var retrievedCustomer = _customerRepository.GetById(customer.CustomerId);
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer.FirstName, retrievedCustomer.FirstName);
        Assert.AreEqual(customer.LastName, retrievedCustomer.LastName);
        Assert.AreEqual(customer.PhoneNumber, retrievedCustomer.PhoneNumber);
    }

    [TestMethod]
    public void GetById_ShouldGetExistingCustomerSuccesfully()
    {
        // Arrange
        // Customer exist in Database with ID 7
        Customer customer;

        // Act
        customer = _customerRepository.GetById(7);

        // Assert
        var retrievedCustomer = _customerRepository.GetById(customer.CustomerId);
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer.FirstName, retrievedCustomer.FirstName);
        Assert.AreEqual(customer.LastName, retrievedCustomer.LastName);
        Assert.AreEqual(customer.PhoneNumber, retrievedCustomer.PhoneNumber);
    }

    [TestMethod]
    public void GetAll_ShouldGetAllCustomerInfoSuccesfully()
    {
        // Arrange
        var customer = new List<Customer>();

        // Act
        customer = _customerRepository.GetAll();

        // Assert
        var retrievedCustomer = _customerRepository.GetAll();
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer, retrievedCustomer);
        
    }

    [TestMethod]
    public void Update_ShouldUpdateCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer
        (
            "Anna",
            "Jensen",
            "87654321"
        );

        // Act
        //customer.CustomerId = _customerRepository.Update(customer);

        // Assert
        
    }

    [TestMethod]
    public void Delete_ShouldDeleteCustomerSuccesfully()
    {
        // Arrange

        // Act

        // Assert
        
    }
}
