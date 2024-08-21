namespace ShippingLabelWebApi.Models
{
    public class Parcel
    {
        public int length { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string distance_unit { get; set; }
        public int weight { get; set; }
        public string mass_unit { get; set; }
    }
}
