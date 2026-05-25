using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CotacaoDolarAPICalculadora
{
    public class MoneySearch
    {
        private static readonly JsonSerializerOptions readOptions = new()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public static async Task<Dolar> DolarSearch()
        {
            using HttpResponseMessage response = await APIRest.sharedClient.GetAsync("/json/last/USD-BRL");
            response.EnsureSuccessStatusCode();

            string jsonPureText = await response.Content.ReadAsStringAsync();

            Dictionary<string, Dolar> resultAPI = JsonSerializer.Deserialize<Dictionary<string, Dolar>>(jsonPureText, readOptions);
            Dolar dolarResults = resultAPI["USDBRL"];

            return dolarResults;
        }
    }
}
