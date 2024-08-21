using ShippingLabelWebApi.Models;

namespace ShippingLabelWebApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;

        public async Task<Product> InsertAsync(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            return await Task.FromResult(product);
        }
    }
}
