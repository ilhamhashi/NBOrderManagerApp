using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class ProductRepositoryTests
{
    private IRepository<Product> _productRepository;
    private IConfiguration _config;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _productRepository = new ProductRepository(_config);
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
    public void DeleteProduct_ShouldDeleteProductSuccesfully()
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
    public void GetById_ShouldGetProductByIdSuccesfully()
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
}
