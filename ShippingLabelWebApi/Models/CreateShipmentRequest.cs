namespace ShippingLabelWebApi.Models
{
    public class CreateShipmentRequest
    {
        public string ShippingToken { get; set; }
        public Address FromAddress { get; set; }
        public Address ToAddress { get; set; }
        public Parcel Parcel { get; set; }
    }
}
