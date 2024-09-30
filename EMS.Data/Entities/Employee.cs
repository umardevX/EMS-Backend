using System.ComponentModel.DataAnnotations;

namespace EMS.Data.Entities;

public partial class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [StringLength(50, ErrorMessage = "FirstName cannot exceed {1} characters.")]
    public string FirstName { get; set; } = null!;

    [StringLength(50, ErrorMessage = "LastName cannot exceed {1} characters.")]
    public string LastName { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    [DataType(DataType.Date)]
    public DateOnly HireDate { get; set; }

    public string? Position { get; set; }

    public string? Department { get; set; }

    public bool IsActive { get; set; }

}
