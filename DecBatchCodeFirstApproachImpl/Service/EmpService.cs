using DecBatchCodeFirstApproachImpl.Data;
using DecBatchCodeFirstApproachImpl.Models;
using DecBatchCodeFirstApproachImpl.Repository;

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

        public void deleteById(int id)
        {
            var emp = db.emp.Find(id);
            db.emp.Remove(emp);
            db.SaveChanges();
        }
    }
}
