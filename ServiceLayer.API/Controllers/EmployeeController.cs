using BusinessLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceLayer.API.Controllers
{
    public class EmployeeController : ApiController
    {
        private EmployeeBL ObjEmp;
        public EmployeeController()
        {
            ObjEmp = new EmployeeBL();
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return ObjEmp.GetAllEmployee(); 
        }
    }
}
