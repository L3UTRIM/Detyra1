namespace Bank.Domain;

public class Employee
{
    public string PersonalNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public ICollection<Allocation> Allocations { get; set; } = new List<Allocation>();
}

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string? ManagerPersonalNumber { get; set; }
    public Employee? Manager { get; set; }
    public DateTime? ManagerStartDate { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<DepartmentLocation> DepartmentLocations { get; set; } = new List<DepartmentLocation>();
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public ICollection<ProjectOffice> ProjectOffices { get; set; } = new List<ProjectOffice>();
    public ICollection<Allocation> Allocations { get; set; } = new List<Allocation>();
}

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;

    public ICollection<Office> Offices { get; set; } = new List<Office>();
    public ICollection<DepartmentLocation> DepartmentLocations { get; set; } = new List<DepartmentLocation>();
}

public class Office
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;

    public ICollection<ProjectOffice> ProjectOffices { get; set; } = new List<ProjectOffice>();
}

public class Allocation
{
    public string EmployeePersonalNumber { get; set; } = null!;
    public Employee Employee { get; set; } = null!;

    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public int HoursPerWeek { get; set; }
}

public class DepartmentLocation
{
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
}

public class ProjectOffice
{
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public int OfficeId { get; set; }
    public Office Office { get; set; } = null!;
}
