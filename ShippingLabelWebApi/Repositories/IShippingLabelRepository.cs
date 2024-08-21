using ShippingLabelWebApi.Models;

namespace ShippingLabelWebApi.Repositories
{
    public interface IShippingLabelRepository
    {
        Task<IEnumerable<Shipment>> GetAllShipments();
        Task<string> CreateTransactionAsync(string shipping_token, string rate_object_id);
        Task<string> CreateTransactionAsync(string shipping_token, string carrier_account, string servicelevel_token);
        Task<string> CreateShipmentAsync(string shipping_token);
    }
}
