using ShippingLabelWebApi.Models;
using System.Data;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Mime;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ShippingLabelWebApi.Services
{


    public class MGShippingLabelServices 
    {

        #region Readonly parameters
        private readonly string Transaction_EndPointURL = "https://api.goshippo.com/transactions";
        private readonly string Shipments_EndPointURL = "https://api.goshippo.com/shipments/";
        private readonly string PdfType = "PDF_4x6";
        private readonly string Authorization = "Authorization";
        private readonly string ContentType_Label = "Content-Type";
        private readonly string ContentType = "application/json";
        private readonly string ShippoToken = "ShippoToken";
        #endregion


        private Address fromAddress;
        private Address toAddress;
        private Parcel parcel;
        private Shipment shipment;
        private Transaction transaction;
        private Rate rate;

        public void From_Address(string company, string name, string street1, string street2, string city, string state, string zip, string country, string phone, string email)
        {
            fromAddress = new Address
            {
                company = company,
                name = name,
                street1 = street1,
                street2 = street2,
                city = city,
                state = state,
                country = country,
                zip = zip,
                phone = phone,
                email = email
            };
        }

        public void To_Address(string company, string name, string street1, string street2, string city, string state, string zip, string country, string phone, string email)
        {
            toAddress = new Address
            {
                company = company,
                name = name,
                street1 = street1,
                street2 = street2,
                city = city,
                state = state,
                country = country,
                zip = zip,
                phone = phone,
                email = email
            };
        }

        public void Parcel(int length, int width, int height, int weight, string distance_unit, string mass_unit)
        {
            parcel = new Parcel
            {
                length = length,
                width = width,
                height = height,
                weight = weight,
                distance_unit = distance_unit,
                mass_unit = mass_unit
            };
        }





        public async System.Threading.Tasks.Task<string> CreateTransactionAsync(string shipping_token, string carrier_account, string servicelevel_token)
        {
            string result = string.Empty;
            try
            {
                shipment = new Shipment
                {
                    address_from = fromAddress,
                    address_to = toAddress,
                    parcels = new List<Parcel>()
                };
                shipment.parcels.Add(parcel);

                transaction = new Transaction
                {
                    shipment = shipment,
                    servicelevel_token = servicelevel_token,
                    carrier_account = carrier_account,
                    label_file_type = PdfType,
                };

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                var JsonData = JsonConvert.SerializeObject(transaction, settings);

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, Transaction_EndPointURL)
                {
                    Content = new StringContent(JsonData),
                };
                httpRequestMessage.Headers.Add(Authorization, ShippoToken + " " + shipping_token);
                httpRequestMessage.Content.Headers.Clear();
                httpRequestMessage.Content.Headers.Add(ContentType_Label, ContentType);
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                result = "Error Message: " + ex.Message + "; Stack Trace: " + ex.StackTrace;
            }
            return result;
        }





        public async System.Threading.Tasks.Task<string> CreateTransactionAsync(string shipping_token, string rate_object_id)
        {
            string result = string.Empty;
            try
            {
                rate = new Rate
                {
                    rate = rate_object_id,
                    label_file_type = PdfType,
                    async = false
                };

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                var JsonData = JsonConvert.SerializeObject(rate, settings);

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, Transaction_EndPointURL)
                {
                    Content = new StringContent(JsonData),
                };
                httpRequestMessage.Headers.Add(Authorization, ShippoToken + " " + shipping_token);
                httpRequestMessage.Content.Headers.Clear();
                httpRequestMessage.Content.Headers.Add(ContentType_Label, ContentType);
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                result = "Error Message: " + ex.Message + "; Stack Trace: " + ex.StackTrace;
            }
            return result;
        }

        public async System.Threading.Tasks.Task<string> CreateShipmentAsync(string shipping_token)
        {
            string result = string.Empty;
            try
            {
                shipment = new Shipment
                {
                    address_from = fromAddress,
                    address_to = toAddress,
                    parcels = new List<Parcel>(),
                    async = false
                };
                shipment.parcels.Add(parcel);

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                var JsonData = JsonConvert.SerializeObject(shipment, settings);

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, Shipments_EndPointURL)
                {
                    Content = new StringContent(JsonData),
                };
                httpRequestMessage.Headers.Add(Authorization, ShippoToken + " " + shipping_token);
                httpRequestMessage.Content.Headers.Clear();
                httpRequestMessage.Content.Headers.Add(ContentType_Label, ContentType);
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                result = "Error Message: " + ex.Message + "; Stack Trace: " + ex.StackTrace;
            }
            return result;
        }

    }




}


