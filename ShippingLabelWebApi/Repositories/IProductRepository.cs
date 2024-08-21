using ShippingLabelWebApi.Models;

namespace ShippingLabelWebApi.Repositories
{
    public interface IProductRepository
    {
        Task<Product> InsertAsync(Product product);
    }
}
