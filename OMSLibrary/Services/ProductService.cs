using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
using OrderManagerLibrary.Services.Interfaces;

namespace OrderManagerLibrary.Services;
public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Size> _sizeRepository;
    private readonly IRepository<Taste> _tasteRepository;
    private readonly IRepository<ProductSize> _productSizeRepository;
    private readonly IRepository<ProductTaste> _productTasteRepository;

    public ProductService(IRepository<Product> productRepository,
                          IRepository<Size> sizeRepository, IRepository<Taste> tasteRepository, 
                          IRepository<ProductSize> productSizeRepository, IRepository<ProductTaste> productTasteRepository)
    {
        _productRepository = productRepository;
        _sizeRepository = sizeRepository;
        _tasteRepository = tasteRepository;
        _productSizeRepository = productSizeRepository;
        _productTasteRepository = productTasteRepository;
    }

    public IEnumerable<Size> GetAllSizes() => _sizeRepository.GetAll();
    public IEnumerable<Taste> GetAllTastes() => _tasteRepository.GetAll();

    public IEnumerable<Product> GetAllProducts()
    { 
        var products = _productRepository.GetAll();

        foreach (var product in products)
        {
            product.SizeOptions.AddRange((_sizeRepository as SizeRepository).GetByProductId(product.Id));
            product.Size = (product.SizeOptions.Count > 0) ? product.SizeOptions[0] : null;
            product.TasteOptions.AddRange((_tasteRepository as TasteRepository).GetByProductId(product.Id));
            product.Taste = (product.TasteOptions.Count > 0) ? product.TasteOptions[0] : null;            
        }
        return products;
    }

    public Product? GetProductById(int id) => _productRepository.GetById(id);

    public Product CreateProduct(Product product)
    {
        product.Id = _productRepository.Insert(product);

        foreach (var size in product.SizeOptions)
        {
            _productSizeRepository.Insert(new ProductSize(size.Id, product.Id));
        }
        foreach (var taste in product.TasteOptions)
        {
            _productTasteRepository.Insert(new ProductTaste(taste.Id, product.Id));
        }

        return product;
    }

    public void UpdateProduct(Product product)
    {
        _productRepository.Update(product);

        var oldSizes = _productSizeRepository.GetAll().Where(ps => ps.ProductId == product.Id);
        var oldTastes = _productTasteRepository.GetAll().Where(pt => pt.ProductId == product.Id);

        foreach (var oldSize in oldSizes)
        {
            _productSizeRepository.Delete(oldSize.SizeId, product.Id);
        }

        foreach (var oldTaste in oldTastes)
        {
            _productTasteRepository.Delete(oldTaste.TasteId, product.Id);
        }

        foreach (var size in product.SizeOptions)
        {
            _productSizeRepository.Insert(new ProductSize(size.Id, product.Id));
        }

        foreach (var taste in product.TasteOptions)
        {
            _productTasteRepository.Insert(new ProductTaste(taste.Id, product.Id));
        }
    }
    public void RemoveProduct(int id) => _productRepository.Delete(id);
}
