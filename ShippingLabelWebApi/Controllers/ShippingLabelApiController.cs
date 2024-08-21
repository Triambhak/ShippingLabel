using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShippingLabelWebApi.Models;
using ShippingLabelWebApi.Repositories;
using ShippingLabelWebApi.Services;
using System.Net;

namespace ShippingLabelWebApi.Controllers
{
    [ApiController]
    [Route("api/shippinglabel")]
    public class ShippingLabelApiController : ControllerBase
    {
        private readonly MGShippingLabelServices _mgshippinglabelservices;


        //public ShippingLabelApiController(MGShippingLabelServices mgShippingLabelServices)
        //{
        //    _mgshippinglabelservices = mgShippingLabelServices;
        //}

        //[HttpPost]
        //[Route("createtransaction")]
        //public async Task<IActionResult> CreateTransactionAsync(string shipping_token, string carrier_account, string servicelevel_token)
        //{
        //    try
        //    {
        //        var _mgshippinglabelservices = new MGShippingLabelServices();
        //        var result = await _mgshippinglabelservices.CreateTransactionAsync(shipping_token, carrier_account, servicelevel_token);

        //        // Return a success response
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        // logger.LogError(ex, "An error occurred while creating a transaction");

        //        // Return an error response
        //        return StatusCode(500, "An error occurred while creating a transaction");
        //    }
        //}

        //[HttpPost]
        //[Route("createtransactionrate")]
        //public async Task<IActionResult> CreateTransactionAsync(string shipping_token, string rate_object_id)
        //{
        //    try
        //    {
        //        var _mgshippinglabelservices = new MGShippingLabelServices();
        //        //Shipment s = JsonConvert.DeserializeObject<Shipment>(sr);
        //        //foreach (Rate r in s.rates)
        //        //{
        //        //    if (r.carrier_account == Carrier_Account && r.serviceLevel.token == Service_Level_Token)
        //        //    {
        //        //        amount = r.amount;
        //        //        rate_object_id = r.object_id;
        //        //    }
        //        //}

        //        var result = await _mgshippinglabelservices.CreateTransactionAsync(shipping_token, rate_object_id);

        //        // Return a success response
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        // logger.LogError(ex, "An error occurred while creating a transaction");

        //        // Return an error response
        //        return StatusCode(500, "An error occurred while creating a transaction");
        //    }
        //}

        //[HttpPost]
        //[Route("shipment")]
        //public async Task<IActionResult> CreateShipmentAsync(string shipping_token)
        //{
        //    try
        //    {
        //        var _mgshippinglabelservices = new MGShippingLabelServices();
        //        _mgshippinglabelservices.From_Address("Test", "Mr Hippo", "965 Mission St #572", "", "San Francisco", "CA", "94103", "US", "4151234567", "mrshippo@shippo.com");
        //        _mgshippinglabelservices.To_Address("Test", "Mrs Hippo", "1092 Indian Summer Ct", "", "San Jose", "CA", "95122", "US", "4151234568", "mrsshippo@shippo.com");
        //        _mgshippinglabelservices.Parcel(10, 5, 3, 2, "in", "lb");
        //        var result = await _mgshippinglabelservices.CreateShipmentAsync(shipping_token);

        //        // Return a success response
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        // logger.LogError(ex, "An error occurred while creating a transaction");

        //        // Return an error response
        //        return StatusCode(500, "An error occurred while creating a shipment");
        //    }


        //}

        [HttpPost]
        [Route("createshipment")]
        public async Task<IActionResult> CreateShipmentAsync([FromBody] string shippingToken)
        {
            if (string.IsNullOrEmpty(shippingToken))
            {
                return BadRequest("Shipping token cannot be null or empty.");
            }
            var _mgshippinglabelservices = new MGShippingLabelServices();
            _mgshippinglabelservices.From_Address("Test", "Mr Hippo", "965 Mission St #572", "", "San Francisco", "CA", "94103", "US", "4151234567", "mrshippo@shippo.com");
            _mgshippinglabelservices.To_Address("Test", "Mrs Hippo", "1092 Indian Summer Ct", "", "San Jose", "CA", "95122", "US", "4151234568", "mrsshippo@shippo.com");
            _mgshippinglabelservices.Parcel(10, 5, 3, 2, "in", "lb");

            var result = await _mgshippinglabelservices.CreateShipmentAsync(shippingToken);

            if (result.StartsWith("Error Message:"))
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("createtransaction")]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequest request)
        {
            if (request == null) 
            {
                return BadRequest("Request cannot be null.");
            }
            var _mgshippinglabelservices = new MGShippingLabelServices();
            string result;
            if (!string.IsNullOrEmpty(request.CarrierAccount) && !string.IsNullOrEmpty(request.ServiceLevelToken))
            {
                result = await _mgshippinglabelservices.CreateTransactionAsync(request.ShippingToken, request.CarrierAccount, request.ServiceLevelToken);
            }
            else if (!string.IsNullOrEmpty(request.RateObjectId))
            {
                result = await _mgshippinglabelservices.CreateTransactionAsync(request.ShippingToken, request.RateObjectId);
            }
            else
            {
                return BadRequest("Invalid request parameters.");
            }

            if (result.StartsWith("Error Message:"))
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }
  
    }
}



