using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using OwinWebApi.Models;

namespace OwinWebApi.Services
{
    class CompanyService : ICompanyService
    {
        public IEnumerable<Company> GetAll(ApplicationDbContext context)
        {
            return context.Companies;
        }

        public Company Get(int id, ApplicationDbContext context)
        {
            return context.Companies.FirstOrDefault(c => c.Id == id);
        }

        public bool Post(Company company, ApplicationDbContext context)
        {
            if (company == null)
            {
                return false;
            }

            var companyExists = context.Companies.Any(c => c.Id == company.Id);
            if (companyExists)
            {
                return false;
            }

            context.Companies.Add(company);
            //Savechanges
            return true;

        }

        public bool Put(Company company, ApplicationDbContext context)
        {
            if (company == null)
            {
                return false;
            }

            var existing = context.Companies.FirstOrDefault(c => c.Id == company.Id);
            if (existing == null)
            {
                return false;
            }

            existing.Name = company.Name;
            //Savechanges
            return true;
        }

        public bool Delete(int id, ApplicationDbContext context)
        {
            var company = context.Companies.FirstOrDefault(c => c.Id == id);
            if (company == null)
            {
                return false;
            }
            context.Companies.Remove(company);
            return true;
        }
    }
}
