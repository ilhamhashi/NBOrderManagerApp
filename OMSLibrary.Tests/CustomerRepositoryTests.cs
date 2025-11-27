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
        var customer = new Customer("John", "Doe", "12345678");
        int customerId = _customerRepository.Insert(customer);

        // Act
        customer = _customerRepository.GetById(customerId);

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
        //customer = _customerRepository.GetAll();

        // Assert
        var retrievedCustomer = _customerRepository.GetAll();
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer, retrievedCustomer);
    }

    [TestMethod]
    public void Update_ShouldUpdateCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer("John", "Doe", "12345678");
        int customerId = _customerRepository.Insert(customer);

        // Act
        var updatedCustomer = new Customer("Anna", "Jensen", "35637899");
        _customerRepository.Update(customer);

        // Assert
        var retrievedCustomer = _customerRepository.GetById(customer);
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(retrievedCustomer.FirstName, updatedCustomer.FirstName);
        Assert.AreEqual(retrievedCustomer.LastName, updatedCustomer.LastName);
        Assert.AreEqual(retrievedCustomer.PhoneNumber, updatedCustomer.PhoneNumber);
    }

    [TestMethod]
    public void Delete_ShouldDeleteCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer("John", "Doe", "12345678");
        int customerId = _customerRepository.Insert(customer);
        Assert.IsNotNull(_customerRepository.GetById(customerId));

        // Act
        _customerRepository.Delete(customer);

        // Assert
        Assert.IsNull(_customerRepository.GetById(customer));
    }
}
