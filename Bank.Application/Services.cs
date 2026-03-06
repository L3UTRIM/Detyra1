using Bank.Domain;

namespace Bank.Application;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
    {
        return _employeeRepository.GetAllAsync(cancellationToken);
    }

    public Task<Employee?> GetEmployeeAsync(string personalNumber, CancellationToken cancellationToken = default)
    {
        return _employeeRepository.GetByPersonalNumberAsync(personalNumber, cancellationToken);
    }
}

