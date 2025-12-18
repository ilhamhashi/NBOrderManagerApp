using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class CustomerRepositoryTests
{
    private IRepository<Customer> _customerRepository;
    private IConfiguration _config;
    private IDataAccess _db;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _db = new DataAccess.DataAccess(_config);
        _customerRepository = new CustomerRepository(_db);
    }

    [TestMethod]
    public void Insert_ShouldInsertCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer ("John", "Doe", "12345678");

        // Act
        customer.Id = _customerRepository.Insert(customer);

        // Assert
        var retrievedCustomer = _customerRepository.GetById(customer.Id);
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer.FirstName, retrievedCustomer.FirstName);
        Assert.AreEqual(customer.LastName, retrievedCustomer.LastName);
        Assert.AreEqual(customer.PhoneNumber, retrievedCustomer.PhoneNumber);
    }

    [TestMethod]
    public void GetById_ShouldGetExistingCustomerSuccesfully()
    {
        //Arrange
        var customer = new Customer("John", "Doe", "12345678");
        int customerId = _customerRepository.Insert(customer);

        // Act
        var retrievedCustomer = _customerRepository.GetById(customerId);

        // Assert
        Assert.IsNotNull(retrievedCustomer);
        Assert.AreEqual(customer.FirstName, retrievedCustomer.FirstName);
        Assert.AreEqual(customer.LastName, retrievedCustomer.LastName);
        Assert.AreEqual(customer.PhoneNumber, retrievedCustomer.PhoneNumber);
    }

    [TestMethod]
    public void GetAll_ShouldGetAllCustomersSuccesfully()
    {
        // Arrange
        var customers = new List<Customer> ();

        // Act
        customers.AddRange(_customerRepository.GetAll());

        // Assert
        Assert.IsNotNull(customers);
    }

    [TestMethod]
    public void Update_ShouldUpdateCustomerSuccesfully()
    {
        // Arrange
        var customer = new Customer ("John", "Doe", "12345678");
        int customerId = _customerRepository.Insert(customer);

        // Act
        var updatedCustomer = new Customer(customerId, "Jane", "Doe", "87654321");
        _customerRepository.Update(updatedCustomer);

        // Assert
        var retrievedCustomer = _customerRepository.GetById(customerId);
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
        customer.Id = _customerRepository.Insert(customer);
        Assert.IsNotNull(_customerRepository.GetById(customer.Id));

        // Act
        _customerRepository.Delete(customer);

        // Assert
        Assert.IsNull(_customerRepository.GetById(customer.Id));
    }
}
