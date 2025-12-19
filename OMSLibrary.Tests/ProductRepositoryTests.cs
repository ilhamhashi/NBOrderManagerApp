using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
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
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _db = new DataAccess.DataAccess(_config);
        _productRepository = new ProductRepository(_db);
    }

    [TestMethod]
    public void InsertOrder_ShouldInsertProductSuccesfully()
    {
        // Arrange
        var product = new Product(1, "test","test",10,false);

        // Act
        product.Id = _productRepository.Insert(product);

        // Assert
        var retrievedProduct = _productRepository.GetById(product.Id);
        Assert.IsNotNull(retrievedProduct);
        Assert.AreEqual(product.Name, retrievedProduct.Name);
        Assert.AreEqual(product.Description, retrievedProduct.Description);
        Assert.AreEqual(product.Price, retrievedProduct.Price);
    }
}
