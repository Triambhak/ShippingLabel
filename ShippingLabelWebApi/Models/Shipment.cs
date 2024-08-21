namespace ShippingLabelWebApi.Models
{
    public class Shipment
    {
        public Address address_from { get; set; }
        public Address address_to { get; set; }
        public List<Parcel> parcels { get; set; }
        public bool async { get; set; }
        public string object_id { get; set; }
        public string status { get; set; }
        public List<Rate> rates { get; set; }
    }
}
