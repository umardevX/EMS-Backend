using EMS.Data.Entities;
using EMS.Data.Repositories.Interfaces;
using EMS.Services.Services.Interfaces;

namespace EMS.Services.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _unitOfWork.EmployeeRepo.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _unitOfWork.EmployeeRepo.GetByIdAsync(id);
            return employee;
        }

        public async Task CreateEmployee(Employee employee)
        {
            await _unitOfWork.EmployeeRepo.AddAsync(employee);
            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteEmployee(int id)
        {
            var employeeExist = await _unitOfWork.EmployeeRepo.GetByIdAsync(id);

            _unitOfWork.EmployeeRepo.Delete(employeeExist);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var employeeExist = await _unitOfWork.EmployeeRepo.GetByIdAsync(employee.EmployeeId);

            if (employeeExist != null)
            {
                employeeExist.FirstName = employee.FirstName;
                employeeExist.LastName = employee.LastName;
                employeeExist.Email = employee.Email;
                employeeExist.PhoneNumber = employee.PhoneNumber;
                employeeExist.DateOfBirth = employee.DateOfBirth;
                employeeExist.HireDate = employee.HireDate;
                employeeExist.Position = employee.Position;
                employeeExist.Department = employee.Department;
                employeeExist.IsActive = employee.IsActive;

                _unitOfWork.EmployeeRepo.Update(employeeExist);
                await _unitOfWork.SaveChanges();
            }

        }
    }
}
