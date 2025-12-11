using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Services
{
    public interface IProductService
    {  
        IEnumerable<Product> GetAllProducts();
        Product? GetProductById(int id);
        int CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }

}

