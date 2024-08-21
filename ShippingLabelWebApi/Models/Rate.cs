namespace ShippingLabelWebApi.Models
{
    public class Rate
    {
        public string rate { get; set; }
        public string label_file_type { get; set; }
        public bool async { get; set; }
        public string object_id { get; set; }
        public string amount { get; set; }
        public string provider { get; set; }
        public ServiceLevel serviceLevel { get; set; }
        public string carrier_account { get; set; }
    }
}
