using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using OwinWebApi.Models;

namespace OwinWebApi.Services
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAll();
        Company Get(int id);
        bool Post(Company company);
        bool Put(Company company);
        bool Delete(int id);
    }
}
