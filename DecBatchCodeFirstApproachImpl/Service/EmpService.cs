using DecBatchCodeFirstApproachImpl.Data;
using DecBatchCodeFirstApproachImpl.Models;
using DecBatchCodeFirstApproachImpl.Repository;
using System.Diagnostics.CodeAnalysis;

namespace DecBatchCodeFirstApproachImpl.Service
{
    public class EmpService: IEmpService
    {
        public readonly ApplicationDbContext db;
        public EmpService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Employee> FetchEmployes()
        {
            return db.emp.ToList();
        }

        public void DeleteById(int id)
        {
            var emp = db.emp.Find(id);
            db.emp.Remove(emp);
            db.SaveChanges();
        }
      
        public void UpdateEmp(Employee em)
        {
            db.emp.Update(em);
            db.SaveChanges();
        }

        public Employee FindEmpById(int id)
        {
            var d = db.emp.Find(id);
            if (d != null)
            {
                return d;
            }
            else
            {
                return null;
            }
        }

        public List<Employee> SearchEmployees(string str, string dept, int no, string sort)
        {
            var data = new List<Employee>();
            if (str == null && no == 0 && sort == "def")
            {
                data = db.emp.Where(x => x.Department.Equals(dept)).ToList();
            }
            else if (dept == "def" && no == 0 && sort == "def")
            {
                data = db.emp.Where(x => x.Name.Contains(str) || x.Email.Contains(str) || x.Department.Contains(str) || x.Salary.ToString().Contains(str)).ToList();
            }
            else if (dept == "def" && str == null && sort == "def")
            {
                data = db.emp.Take(no).ToList();
            }
            else if (sort == "asc")
            {
                data = db.emp.Where(x => x.Department.Equals(dept)).OrderBy(x => x.Salary).Take(no).ToList();
            }
            else
            {
                data = db.emp.Where(x => x.Department.Equals(dept)).OrderByDescending(x => x.Salary).Take(no).ToList();
            }
            return data;
        }

    }
}
