using ShippingLabelWebApi.Models;
using ShippingLabelWebApi.Repositories;

namespace ShippingLabelWebApi.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> InsertProductAsync(Product product)
        {
            // You can add any business logic here before inserting the product.
            return await _productRepository.InsertAsync(product);
        }
    }
}
