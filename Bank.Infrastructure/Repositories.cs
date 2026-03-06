using Bank.Application;
using Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly BankDbContext _context;

    public EmployeeRepository(BankDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByPersonalNumberAsync(string personalNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.PersonalNumber == personalNumber, cancellationToken);
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await _context.Employees.AddAsync(employee, cancellationToken);
    }
}

public class DepartmentRepository : IDepartmentRepository
{
    private readonly BankDbContext _context;

    public DepartmentRepository(BankDbContext context)
    {
        _context = context;
    }

    public async Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Departments.FindAsync(new object[] { id }, cancellationToken);
    }
}

public class UnitOfWork : IUnitOfWork
{
    private readonly BankDbContext _context;

    public UnitOfWork(BankDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}

