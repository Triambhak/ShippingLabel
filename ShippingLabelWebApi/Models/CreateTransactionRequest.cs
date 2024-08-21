namespace ShippingLabelWebApi.Models
{
    public class CreateTransactionRequest
    {
        public string ShippingToken { get; set; }
        public string CarrierAccount { get; set; }
        public string ServiceLevelToken { get; set; }
        public string RateObjectId { get; set; }
    }
}
