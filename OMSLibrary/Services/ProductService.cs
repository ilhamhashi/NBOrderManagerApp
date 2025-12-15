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

    public ProductService(IRepository<Product> productRepository, 
                          IRepository<Size> sizeRepository, IRepository<Taste> tasteRepository)
    {
        _productRepository = productRepository;
        _sizeRepository = sizeRepository;
        _tasteRepository = tasteRepository;
    }

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
        return product;
    }

    public void UpdateProduct(Product product) => _productRepository.Update(product);
    public void RemoveProduct(int id) => _productRepository.Delete(id);
}
