using Bank.Domain;

namespace Bank.Application;

public interface IEmployeeRepository
{
    Task<Employee?> GetByPersonalNumberAsync(string personalNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
}

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IEmployeeService
{
    Task<IReadOnlyList<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);
    Task<Employee?> GetEmployeeAsync(string personalNumber, CancellationToken cancellationToken = default);
}

