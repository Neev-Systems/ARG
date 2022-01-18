using BusinessLayer;
using DataAccessLayer.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity.Infrastructure;
using System.Web.Http;
using System.Web.Http.Description;

namespace ServiceLayer.API.Controllers
{
    public class EmployeeController : ApiController
    {
        private EmployeeBL ObjEmp;
        public EmployeeController()
        {
            ObjEmp = new EmployeeBL();
        }
        public IEnumerable<Employee> GetEmp()
        {
            return ObjEmp.GetAllEmployee(); 
        }
        [ResponseType(typeof(Employee))]
       public IHttpActionResult GetEmp(int id)
       {
            Employee emp = ObjEmp.GetEmpById(id);
            return Ok(emp);
       }
        [HttpPost]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult Update(Employee emp)
        {
            ObjEmp.Update(emp);
            return CreatedAtRoute("DefaultApi", new { id = emp.employeeId }, emp);
        }
        [HttpPut]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult Insert(Employee emp)
        {
            ObjEmp.Insert(emp);
            return Ok(emp);
        }
        [HttpDelete]
        //[ResponseType(typeof(Employee))]
        public  void Delete(int id)
        {
            ObjEmp.Delete(id);
        }
    }
}
