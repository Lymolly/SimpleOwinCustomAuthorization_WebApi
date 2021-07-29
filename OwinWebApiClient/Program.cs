using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OwinWebApi.Models;

namespace OwinWebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
            Console.WriteLine("");
            Console.WriteLine("Done! Press the Enter key to Exit...");
            Console.ReadLine();
            return;
        }
        private static async Task Run()
        {
            string hostUriString = "http://localhost:8080";
            var provider = new ApiClientProvider(hostUriString);
            string _accessToken;
            Dictionary<string, string> _tokenDictionary;
            try
            {
                _tokenDictionary = await provider.GetTokenDictionary(
                    "john@example.com", "JohnsPassword");
                _accessToken = _tokenDictionary["access_token"];

                foreach (var kvp in _tokenDictionary)
                {
                    Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
                    Console.WriteLine("");
                }

                var baseUri = new Uri(hostUriString);
                var companyClient = new CompanyClient(baseUri, _accessToken);

                foreach (var company in await companyClient.GetCompaniesAsync())
                {
                    Console.WriteLine(company.Name);
                }

            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions[0].Message);
                Console.WriteLine("Press the Enter key to Exit...");
                Console.ReadLine();
                return;
            }
            catch (Exception ex)
            {
                // Something else happened:
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press the Enter key to Exit...");
                Console.ReadLine();
                return;
            }
        }
        static void WriteCompaniesList(IEnumerable<Company> companies)
        {
            foreach (var company in companies)
            {
                Console.WriteLine("Id: {0} Name: {1}", company.Id, company.Name);
            }
            Console.WriteLine("");
        }

        static void WriteStatusCodeResult(System.Net.HttpStatusCode statusCode)
        {
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Opreation Succeeded - status code {0}", statusCode);
            }
            else
            {
                Console.WriteLine("Opreation Failed - status code {0}", statusCode);
            }
            Console.WriteLine("");
        }
    }
}
