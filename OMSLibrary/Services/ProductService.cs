using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerLibrary.Services
{
    public class ProductService : IProductService
    {

        private readonly IRepository<Product> _productRepository;


        public ProductService(IRepository<Product> productRepository)
        {

            _productRepository = productRepository;
        }
    
    public IEnumerable<Product> GetAllProducts()
            => _productRepository.GetAll();
        public Product? GetProductById(int id)
        => _productRepository.GetById(id);

        public int CreateProduct(Product product)
        => _productRepository.Insert(product);
        public void UpdateProduct(Product product)
        => _productRepository.Update(product);
        public void DeleteProduct(int id)
       => _productRepository.Delete(id);

    }
}

