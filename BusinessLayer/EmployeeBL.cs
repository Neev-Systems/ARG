using DataAccessLayer.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class EmployeeBL
    {
        private EmployeeDb objDb;
        public EmployeeBL()
        {
            objDb = new EmployeeDb();
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return objDb.GetAllEmployee();
        }
        public Employee GetEmpById(int id)
        {
            return objDb.GetEmpById(id);
        }
        public void Inser(Employee emp)
        {
            objDb.Insert(emp);
        }
        public void Update(Employee emp)
        {
            objDb.Update(emp);
        }
        public void Delete(int id)
        {
            objDb.Delete(id);
        }
    }
}
