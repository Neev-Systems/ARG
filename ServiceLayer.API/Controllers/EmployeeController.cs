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

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//using System.Web.Mvc;

namespace ServiceLayer.API.Controllers
{
    public class EmployeeController : ApiController
    {



        [HttpGet]
        public Object Token()
        {
            string key = "my_secret_key_12345";
            var issuer = "http://mysite.com";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));

            var token = new JwtSecurityToken(issuer,
                            issuer,
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };


        }

        [HttpPost]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }

       
        [HttpPost]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };

            }

            return null;
        }





        private EmployeeBL ObjEmp;
        public EmployeeController()
        {
            ObjEmp = new EmployeeBL();
        }


        [Authorize]

        public IEnumerable<Employee> GetEmp()
        {
            return ObjEmp.GetAllEmployee(); 
        }

        [Authorize]
        [ResponseType(typeof(Employee))]
       public IHttpActionResult GetEmp(int id)
       {
            Employee emp = ObjEmp.GetEmpById(id);
            if(emp == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No Product Found with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found !"
                };
                throw new HttpResponseException(resp);
            }
            return Ok(emp);
       }

        [Authorize]
        [HttpPost]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult Update(Employee emp)
        {
            if (emp == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Please input data")),
                    ReasonPhrase = "Please input data !"
                };
                throw new HttpResponseException(resp);
            }
            ObjEmp.Update(emp);
            return CreatedAtRoute("DefaultApi", new { id = emp.employeeId }, emp);
        }

        [Authorize]
        [HttpPut]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult Insert(Employee emp)
        {
            if (emp == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Please input data")),
                    ReasonPhrase = "Please input data !"
                };
                throw new HttpResponseException(resp);
            }
            ObjEmp.Insert(emp);
            return Ok(emp);
        }


        [Authorize]
        [HttpDelete]
        //[ResponseType(typeof(Employee))]
        public  void Delete(int id)
        {
            Employee emp = ObjEmp.GetEmpById(id);
            if (emp == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No Product Found with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found !"
                };
                throw new HttpResponseException(resp);
            }
            ObjEmp.Delete(id);
        }
    }
}
