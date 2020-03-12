using Dapper.GenericRepository.Model;
using Dapper.GenericRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Dapper.GenericRepository.Controllers
{
    public class CustomerController:ApiController
    {
        private CustomerRepository _repo;
        public CustomerController()
        {
            _repo = new CustomerRepository();

        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_repo.GetAll());
        }
        [HttpPost]
        public IHttpActionResult Insert(CustomerModel c)
        {
            return Ok(_repo.Insert(c));
        }
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_repo.GetById(id));
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_repo.Delete(id));
        }
        [HttpPut]
        public IHttpActionResult Update(CustomerModel c, int id)
        {
            return Ok(_repo.Update(c, id));
        }
    }
}