using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccessNS;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class ProductRepositoryTests
{
    private IRepository<Product> _productRepository;
    private IConfiguration _config;
    private IDataAccess _db;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        _db = new DataAccess(_config);
        _productRepository = new ProductRepository(_db);
    }

    [TestMethod]
    public void InsertOrder_ShouldInsertProductSuccesfully()
    {
        // Arrange
        var product = new Product("test","test",10);

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
