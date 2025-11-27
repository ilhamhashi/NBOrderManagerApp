using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class ProductRepositoryTests
{
    private IRepository<Product> _productRepository;
    private ISqlDataAccess _dataAccess;
    private IConfigurationRoot _config;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _dataAccess = new SqlDataAccess(_config);
        _productRepository = new ProductRepository(_dataAccess);
    }

    [TestMethod]
    public void InsertProduct_ShouldInsertProductSuccesfully()
    {
        // Arrange
        var product = new Product
        (
            "Product Name",
            "Product Description",
            10
        );

        // Act
        product.ProductId = _productRepository.Insert(product);

        // Assert
        var retrievedProduct = _productRepository.GetById(product.ProductId);
        Assert.IsNotNull(retrievedProduct);
        Assert.AreEqual(product.Name, retrievedProduct.Name);
        Assert.AreEqual(product.Description, retrievedProduct.Description);
        Assert.AreEqual(product.Price, retrievedProduct.Price);
    }

    [TestMethod]
    public void UpdateProduct_ShouldUpdateProductSuccesfully()
    {
        // Arrange
        var product = new Product
        (
            DateTime.Now,
            OrderStatus.Confirmed,
            1
        );

        // Act
        product.OrderId = _productRepository.Insert(product);

        // Assert
        var retrievedProduct = _productRepository.GetById(product.OrderId);
        Assert.IsNotNull(retrievedProduct);
        Assert.AreEqual(product.CustomerId, retrievedProduct.CustomerId);
        Assert.AreEqual(product.Status, retrievedProduct.Status);
    }

    [TestMethod]
    public void DeleteProduct_ShouldDeleteProductSuccesfully()
    {
        // Arrange
        var product = new Product
        (
            DateTime.Now,
            OrderStatus.Confirmed,
            1
        );

        // Act
        product.OrderId = _productRepository.Insert(product);

        // Assert
        var retrievedOrder = _productRepository.GetById(product.OrderId);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(product.CustomerId, retrievedOrder.CustomerId);
        Assert.AreEqual(product.Status, retrievedOrder.Status);
    }

    [TestMethod]
    public void GetById_ShouldGetProductByIdSuccesfully()
    {
        // Arrange
        var order = new Order
        (
            DateTime.Now,
            OrderStatus.Confirmed,
            1
        );

        // Act
        order.OrderId = _productRepository.Insert(order);

        // Assert
        var retrievedOrder = _productRepository.GetById(order.OrderId);
        Assert.IsNotNull(retrievedOrder);
        Assert.AreEqual(order.CustomerId, retrievedOrder.CustomerId);
        Assert.AreEqual(order.Status, retrievedOrder.Status);
    }
}
