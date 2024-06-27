using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Travelcy.App.DataObjects.Responses;
using Travelcy.App.Interfaces;

namespace Travelcy.App
{
    public class BusinessRepository : IBusinessRepository
    {
        public async Task<HandleConversionResponse> HandleConversionAsync(float amount, string currency)
        {
            //https://api.freecurrencyapi.com/v1/latest?apikey=fca_live_Z3UdfEGVpKBpPJcqH4G64Amst3Z5q5a0EwUPUr6h&currencies=EUR&base_currency=GBP
            HandleConversionResponse response = new HandleConversionResponse();

            try
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.freecurrencyapi.com/v1/latest?apikey=fca_live_Z3UdfEGVpKBpPJcqH4G64Amst3Z5q5a0EwUPUr6h&currencies={currency}&base_currency=GBP");

                HttpClient client = new HttpClient();
                HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);

                httpResponse.EnsureSuccessStatusCode();

                var content = await httpResponse.Content.ReadAsStringAsync();
                var webResponse = JsonConvert.DeserializeObject<FreeCurrencyResponse>(content);

                switch (currency)
                {
                    case "EUR":
                        response.ConvertedAmount = amount * webResponse.Data.EUR;
                        break;
                    case "USD":
                        response.ConvertedAmount = amount * webResponse.Data.USD;
                        break;
                    case "JPY":
                        response.ConvertedAmount = amount * webResponse.Data.JPY;
                        break;
                    case "AUD":
                        response.ConvertedAmount = amount * webResponse.Data.AUD;
                        break;
                    case "BRL":
                        response.ConvertedAmount = amount * webResponse.Data.BRL;
                        break;
                    case "TRY":
                        response.ConvertedAmount = amount * webResponse.Data.TRY;
                        break;
                    default:
                        break;
                }

                if (amount <= 300)
                {
                    response.ConversionCharge = float.Parse((response.ConvertedAmount * 0.035).ToString());
                    response.ConvertedAmount = float.Parse((response.ConvertedAmount * 0.965).ToString());
                }

                else if (amount > 300 && amount <= 750)
                {
                    response.ConversionCharge = float.Parse((response.ConvertedAmount * 0.03).ToString());
                    response.ConvertedAmount = float.Parse((response.ConvertedAmount * 0.97).ToString());
                }

                else if (amount >750 && amount <= 1000)
                {
                    response.ConversionCharge = float.Parse((response.ConvertedAmount * 0.025).ToString());
                    response.ConvertedAmount = float.Parse((response.ConvertedAmount * 0.975).ToString());
                }

                else if (amount > 1000 && amount <= 2000)
                {
                    response.ConversionCharge = float.Parse((response.ConvertedAmount * 0.02).ToString());
                    response.ConvertedAmount = float.Parse((response.ConvertedAmount * 0.98).ToString());
                }

                else if (amount > 2000)
                {
                    response.ConversionCharge = float.Parse((response.ConvertedAmount * 0.015).ToString());
                    response.ConvertedAmount = float.Parse((response.ConvertedAmount * 0.985).ToString());
                }

                if (response.ConvertedAmount > 0 && response.ConversionCharge > 0)
                {
                    double floor = Math.Floor(response.ConversionCharge * 100) / 100;
                    response.ConversionCharge = float.Parse(floor.ToString());

                    floor = Math.Floor(response.ConvertedAmount * 100) / 100;
                    response.ConvertedAmount = float.Parse(floor.ToString());

                    response.IsSuccessful = true;
                }

                else
                {
                    response.IsSuccessful = false;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}