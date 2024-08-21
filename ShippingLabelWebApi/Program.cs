using System;
using System.Collections.Generic;
using System.Net.Http;
using ShippingLabelWebApi.Services;
using Newtonsoft.Json;
using ShippingLabelWebApi;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//namespace ShippingLabelConsole
//{
//    class Program
//    {
//        static async System.Threading.Tasks.Task Main(string[] args)
//        {
//            string rate_object_id = string.Empty;
//            string amount = string.Empty;
//            string ShippoAPI = "shippo_test_1d74ac3876ecca790f69e11c915a91133cd76a92";
//            string Carrier_Account = "0e02eb005a814b0ab27c03a41825cb6a";
//            string Service_Level_Token = "usps_priority";


//            MGShippingLabelServices sl = new MGShippingLabelServices();
//            sl.From_Address("Test", "Mr Hippo", "965 Mission St #572", "", "San Francisco", "CA", "94103", "US", "4151234567", "mrshippo@shippo.com");
//            sl.To_Address("Test", "Mrs Hippo", "1092 Indian Summer Ct", "", "San Jose", "CA", "95122", "US", "4151234568", "mrsshippo@shippo.com");
//            sl.Parcel(10, 5, 3, 2, "in", "lb");
//            string sr = await sl.CreateShipmentAsync(ShippoAPI);
//            Shipment s = JsonConvert.DeserializeObject<Shipment>(sr);
//            foreach (Rate r in s.rates)
//            {
//                if (r.carrier_account == Carrier_Account && r.serviceLevel.token == Service_Level_Token)
//                {
//                    amount = r.amount;
//                    rate_object_id = r.object_id;
//                }
//            }
//            string t = await sl.CreateTransactionAsync(ShippoAPI, rate_object_id);
//            Console.ReadLine();

//        }
//    }

//    class Shipment
//    {
//        public string object_id { get; set; }
//        public string status { get; set; }
//        public List<Rate> rates { get; set; }
//    }
//    class Rate
//    {
//        public string object_id { get; set; }
//        public string amount { get; set; }
//        public string provider { get; set; }
//        public ServiceLevel serviceLevel { get; set; }
//        public string carrier_account { get; set; }
//    }
//    class ServiceLevel
//    {
//        public string name { get; set; }
//        public string token { get; set; }
//    }

//}
