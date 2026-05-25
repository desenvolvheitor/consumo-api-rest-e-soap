using CalculatorSoapService;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CotacaoDolarAPICalculadora
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            bool closeApp = false;
            while (true)
            {
                Console.WriteLine("""
                --------------------------- MENU ---------------------------
                [1] Consultar cotação do dólar (API Rest)
                [2] Calculadora (Serviço SOAP)

                [0] Encerrar o programa
                ------------------------------------------------------------

               """);

                int option = ReadInt("Digite o número da opção desejada: ");
                Console.Clear();

                switch (option)
                {
                    case 1:
                        try
                        {
                            Dolar dolarInfo = await MoneySearch.DolarSearch();
                            Console.WriteLine($"Cotação {dolarInfo.QuotName}");
                            Console.WriteLine($"Valor do dólar: R$ {dolarInfo.BuyPrice.ToString("F2")}\n");
                            Console.WriteLine($"O valor máximo que o dólar atingiu hoje foi R$ {dolarInfo.HighPrice.ToString("F2")}, e o valor mínimo foi R$ {dolarInfo.LowPrice.ToString("F2")}");
                            Console.WriteLine($"Última atualização dos dados em: {DateTime.Parse(dolarInfo.UpdateDate)}");
                            returnToMenu();
                        }
                        catch (HttpRequestException ex)
                        {
                            Console.Clear();
                            Console.WriteLine("[ERRO DE CONEXÃO] O servidor de cotações está indisponível no momento. Tente novamente mais tarde.");
                            returnToMenu();
                        }
                        catch (JsonException)
                        {
                            Console.WriteLine("[ERRO DE SISTEMA] Houve uma falha ao ler os dados da moeda. Tente novamente mais tarde.");
                            returnToMenu();
                        }
                        break;

                    case 2:
                        int num1 = ReadInt("Digite um número inteiro: ");
                        int num2 = ReadInt("Digite outro número inteiro: ");

                        int result;
                        string opSignal;
                        bool validSignal;
                        bool operationSuccess = true;

                        Console.Clear();
                        while (true)
                        {
                            validSignal = true;
                            Console.WriteLine("""
                                    OPERAÇÕES:
                                    [+] Adição
                                    [-] Subtração
                                    [*] Multiplicação
                                    [/] Divisão
                                    """);
                            Console.Write("\nDigite o sinal da operação desejada: ");
                            opSignal = Console.ReadLine()?.Trim();
                            result = 0;

                            try
                            {
                                switch (opSignal)
                                {
                                    case "+":
                                        result = await APISoap.soapClient.AddAsync(num1, num2);
                                        break;
                                    case "-":
                                        result = await APISoap.soapClient.SubtractAsync(num1, num2);
                                        break;
                                    case "*":
                                        result = await APISoap.soapClient.MultiplyAsync(num1, num2);
                                        break;
                                    case "/":
                                        if (num2 == 0)
                                        {
                                            operationSuccess = false;
                                            Console.Clear();
                                            Console.WriteLine("Erro! Não é possível realizar divisões por 0!");
                                            returnToMenu();
                                        }
                                        else
                                        {
                                            result = await APISoap.soapClient.DivideAsync(num1, num2);
                                        }
                                        break;
                                    default:
                                        validSignal = false;
                                        break;
                                }

                                Console.Clear();

                                if (validSignal)
                                {
                                    if (operationSuccess)
                                    {
                                        Console.WriteLine($"{num1} {opSignal} {num2} = {result}");
                                        returnToMenu();
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("[ERRO] Digite um sinal válido de acordo com o menu abaixo:\n");
                                }
                            }

                            catch (System.ServiceModel.FaultException)
                            {
                                Console.Clear();
                                Console.WriteLine("[ERRO DE SISTEMA] Houve uma falha ao ler os dados da operação. Tente novamente mais tarde.");
                                returnToMenu();
                                break;
                            }

                            catch (System.ServiceModel.CommunicationException) {
                                Console.Clear();
                                Console.WriteLine("[ERRO DE CONEXÃO] O servidor de operações está indisponível no momento. Tente novamente mais tarde.");
                                returnToMenu();
                                break;
                            }
                        }
                        break;


                    case 0:
                        {
                            Console.WriteLine("""
                                ======================================================
                                               Encerrando o programa...
                                            Até logo e tenha um ótimo dia!

                                  © 2026 Heitor Sales. Todos os direitos reservados.
                                ======================================================
                                """);

                            Thread.Sleep(2000);
                            closeApp = true;
                            break;
                        }

                    default:
                        Console.WriteLine("[ERRO] Selecione uma opção válida de acordo com o menu abaixo: \n");
                        break;
                }

                if (closeApp)
                {
                    break;
                }
            }
        }

        public static int ReadInt(string question)
        {
            Console.Write(question);
            string inputInt = Console.ReadLine()?.Trim();

            bool isIntValid = int.TryParse(inputInt, out int integer);

            while (!isIntValid)
            {
                Console.WriteLine("[ERRO] Número inválido, tente novamente abaixo.");
                Console.WriteLine();
                Console.Write(question);
                inputInt = Console.ReadLine()?.Trim();
                isIntValid = int.TryParse(inputInt, out integer);
            }

            return integer;
        }

        public static void returnToMenu()
        {
            Console.Write("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
