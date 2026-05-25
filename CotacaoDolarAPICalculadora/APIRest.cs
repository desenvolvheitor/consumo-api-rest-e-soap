namespace CotacaoDolarAPICalculadora
{
    public class APIRest
    {
        public static readonly HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://economia.awesomeapi.com.br") // Documentação: https://docs.awesomeapi.com.br/api-de-moedas
        };
    }
}
