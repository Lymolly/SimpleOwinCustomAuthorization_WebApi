using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OwinWebApi.Models;

namespace OwinWebApiClient
{
    public class CompanyClient
    {
        Uri _hostUri;
        private string _accessToken;

        public CompanyClient(Uri hostUri,string token)
        {
            _hostUri = new Uri(hostUri + "api/companies/");
            _accessToken = token;
        }

        void SetClientAuthentication(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",_accessToken);
        }
        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.GetAsync(_hostUri);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Company>>(json);
        }


        public async Task<Company> GetCompanyAsync(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.GetAsync(new Uri(_hostUri,id.ToString()));
            }
            var result = await response.Content.ReadAsAsync<Company>();
            return result;
        }


        public async Task<HttpStatusCode> AddCompany(Company company)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.PostAsJsonAsync(_hostUri, company);
            }
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> UpdateCompany(Company company)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.PutAsJsonAsync(_hostUri, company);
            }
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> DeleteCompany(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.DeleteAsync(new Uri(_hostUri, id.ToString()));
            }
            return response.StatusCode;
        }
    }
}
