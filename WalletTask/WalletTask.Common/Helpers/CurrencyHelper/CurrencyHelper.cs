using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WalletTask.Common.Helpers.CurrencyHelper
{
    public class CurrencyHelper
    {
        private const string _url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

        public static decimal Convert(string fromCurrency, string toCurrency, decimal amount)
        {
            var result = SendGetRequest(_url).Result;
            var cube = XmlHelper.FromXml<Cube>(result);
            var crossRate = GetCrossRate(fromCurrency, toCurrency, cube.Cube1);
            return crossRate * amount;
        }

        private static async Task<string> SendGetRequest(string url)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }

        private static decimal GetCrossRate(string fromCurrency, string toCurrency, CubeCube cubeCube)
        {
            var currencyDict = cubeCube.Cube.ToDictionary(k => k.currency, v => v.rate);
            var toCubeRate = currencyDict[toCurrency];
            if (fromCurrency == CurrencyCodes.EUR)
                return toCubeRate;

            var fromCubeRate = currencyDict[fromCurrency];
            return fromCubeRate / toCubeRate;
        }
    }
}