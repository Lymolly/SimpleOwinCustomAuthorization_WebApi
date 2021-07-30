using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using OwinWebApi.Models;
using OwinWebApi.Services;

namespace OwinWebApi.Controllers
{
    //[Authorize(Roles = "admin")]
    //[Authorize(Roles ="User")]
    public class CompaniesController : ApiController
    {
        private readonly ICompanyService _companyService;
        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public CompaniesController()
        {
            _companyService = new CompanyService();
        }

        private ApplicationDbContext context
        {
            get => Request.GetOwinContext().Get<ApplicationDbContext>();
        }

        [Route("api/companies")]
        [HttpGet]
        public IEnumerable<Company> Get()
        {
            return _companyService.GetAll(context);
        }

        [Route("api/companies/{id}")]
        [HttpGet]
        public Company Get(int? id)
        {
            return _companyService.Get(id.Value,context);
        }

        [Route("api/companies")]
        [HttpPost]
        public IHttpActionResult Post(Company company)
        {
            if (_companyService.Post(company,context))
            {
                context.SaveChanges();
                return Ok();
            }

            return BadRequest("Argument Null");
        }

        [Route("api/companies")]
        [HttpPut]
        public IHttpActionResult Put(Company company)
        {
            if (_companyService.Put(company,context))
            {
                context.SaveChanges();
                return Ok();
            }
            return BadRequest("Argument Null");
        }
        [Route("api/companies/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (_companyService.Delete(id.Value,context))
            {
                context.SaveChanges();
                return Ok();
            }

            return BadRequest("Argument Null");
        }
    }
}
