using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeDb
    {
        private AGREntities db;
        public EmployeeDb()
        {
            db = new AGREntities();
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return db.Employees.ToList();
        }
        public Employee GetEmpById(int id)
        {
            return db.Employees.Find(id);
        }
        public void Insert(Employee emp)
        {
            db.Employees.Add(emp);
            Save();
        }
        public void Update(Employee emp)
        {
            db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
        public void Delete(int id)
        {
            Employee emp = db.Employees.Find(id);
            db.Employees.Remove(emp);
            Save();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
