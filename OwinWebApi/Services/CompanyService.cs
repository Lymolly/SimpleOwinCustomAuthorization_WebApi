using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using OwinWebApi.Models;

namespace OwinWebApi.Services
{
    class CompanyService : ICompanyService
    {
        private static List<Company> _db = new List<Company>
        {
            new Company { Id = 1, Name = "Microsoft" },
            new Company { Id = 2, Name = "Google" },
            new Company { Id = 3, Name = "Apple" }
        };
        public IEnumerable<Company> GetAll()
        {
            return _db;
        }

        public Company Get(int id)
        {
            return _db.FirstOrDefault(c => c.Id == id);
        }

        public bool Post(Company company)
        {
            if (company == null)
            {
                return false;
            }

            var companyExists = _db.Any(c => c.Id == company.Id);
            if (companyExists)
            {
                return false;
            }

            _db.Add(company);
            //Savechanges
            return true;

        }

        public bool Put(Company company)
        {
            if (company == null)
            {
                return false;
            }

            var existing = _db.FirstOrDefault(c => c.Id == company.Id);
            if (existing == null)
            {
                return false;
            }

            existing.Name = company.Name;
            //Savechanges
            return true;
        }

        public bool Delete(int id)
        {
            var company = _db.FirstOrDefault(c => c.Id == id);
            if (company == null)
            {
                return false;
            }
            _db.Remove(company);
            return true;
        }
    }
}
