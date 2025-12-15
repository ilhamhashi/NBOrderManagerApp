using OrderManagerLibrary.Model.Classes;

namespace OrderManagerLibrary.Services.Interfaces;
public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product CreateProduct(Product product);
    void UpdateProduct(Product product);
    void RemoveProduct(int id);
}
