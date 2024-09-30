using EMS.Data.Repositories.Interfaces;

namespace EMS.Data.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmsDbContext _emsDbContext;
        public UnitOfWork(EmsDbContext emsDbContext, IEmployeeRepository EmployeeRepository)
        {
            _emsDbContext = emsDbContext;
            EmployeeRepo = EmployeeRepository;
        }

        public IEmployeeRepository EmployeeRepo { get; private set; }

        public async Task Rollback()
        {
            await _emsDbContext.DisposeAsync();
        }

        public async Task SaveChanges()
        {
            await _emsDbContext.SaveChangesAsync();
        }
    }
}
