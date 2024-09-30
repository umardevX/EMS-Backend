using System;
namespace EMS.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepo { get; }
        Task SaveChanges();
        Task Rollback();
    }
}
