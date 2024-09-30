using EMS.Data.Entities;

namespace EMS.Services.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetAll();
        Task CreateEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int id);
    }
}
