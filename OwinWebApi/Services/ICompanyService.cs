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
        IEnumerable<Company> GetAll(ApplicationDbContext context);
        Company Get(int id, ApplicationDbContext context);
        bool Post(Company company, ApplicationDbContext context);
        bool Put(Company company, ApplicationDbContext context);
        bool Delete(int id, ApplicationDbContext context);
    }
}
