using CalculatorSoapService;

namespace CotacaoDolarAPICalculadora
{
    public static class APISoap
    {
        public static readonly CalculatorSoapClient soapClient = new CalculatorSoapClient(CalculatorSoapClient.EndpointConfiguration.CalculatorSoap);
    }
}
