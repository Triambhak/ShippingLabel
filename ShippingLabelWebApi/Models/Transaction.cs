namespace ShippingLabelWebApi.Models
{
    public class Transaction
    {
        public Shipment shipment { get; set; }
        public string carrier_account { get; set; }
        public string servicelevel_token { get; set; }
        public string label_file_type { get; set; }
    }
}
