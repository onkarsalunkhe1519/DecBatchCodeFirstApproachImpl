using DecBatchCodeFirstApproachImpl.Models;

namespace DecBatchCodeFirstApproachImpl.Repository
{
    public interface IEmpService
    {
        List<Employee> FetchEmployes();
        void DeleteById(int id);
        Employee FindEmpById(int id);
        void UpdateEmp(Employee em);
        List<Employee> SearchEmployees(string str,string dept,int no,string sort);
            
        

    }
}
