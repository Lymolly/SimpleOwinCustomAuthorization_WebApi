using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using OwinWebApi.Models;
using OwinWebApi.Services;

namespace OwinWebApi.Controllers
{
    //[Authorize(Roles = "admin")]
    [Authorize(Roles ="User")]
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

        [Route("api/companies")]
        [HttpGet]
        public IEnumerable<Company> Get()
        {
            return _companyService.GetAll();
        }

        [Route("api/companies/{id}")]
        [HttpGet]
        public Company Get(int? id)
        {
            return _companyService.Get(id.Value);
        }

        [Route("api/companies")]
        [HttpPost]
        public IHttpActionResult Post(Company company)
        {
            if (_companyService.Post(company))
            {
                return Ok();
            }

            return BadRequest("Argument Null");
        }

        [Route("api/companies")]
        [HttpPut]
        public IHttpActionResult Put(Company company)
        {
            if (_companyService.Put(company))
            {
                return Ok();
            }
            return BadRequest("Argument Null");
        }
        [Route("api/companies/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (_companyService.Delete(id.Value))
            {
                return Ok();
            }

            return BadRequest("Argument Null");
        }
    }
}
