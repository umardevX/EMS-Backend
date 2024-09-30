using EMS.Data.Entities;
using EMS.Data.Repositories.Interfaces;

namespace EMS.Data.Repositories.Implementations
{
    public class EmployeeRepository(EmsDbContext emsDbContext) : GenericRepository<Employee>(emsDbContext), IEmployeeRepository
    {
        //public EmployeeRepository(EmsDbContext emsDbContext) : base(emsDbContext) { }
    }
}
