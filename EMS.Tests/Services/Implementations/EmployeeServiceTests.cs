using EMS.Data.Entities;
using EMS.Data.Repositories.Interfaces;
using EMS.Services.Services.Implementations;
using Moq;

namespace EMS.Tests.Services.Implementations
{
    public class EmployeeServiceTests
    {
        public readonly Mock<IUnitOfWork> _mockUnitOfWork;
        public readonly Mock<IEmployeeRepository> _mockEmployeeRepo;
        public readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEmployeeRepo = new Mock<IEmployeeRepository>();

            _mockUnitOfWork.Setup(uow => uow.EmployeeRepo).Returns(_mockEmployeeRepo.Object);
            _employeeService = new EmployeeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAll_ReturnAllEmployees()
        {
            // Arrange

            var expectedEmployees = new List<Employee>
            {
                new() { EmployeeId = 1,FirstName = "Mehran",LastName = "Ayub",Email = "mehran@gmail.com",PhoneNumber = "000111222333",
                    DateOfBirth = new DateOnly(1985, 3, 25),HireDate = new (2010, 6, 1),Department = "R & D",IsActive = true
                },
                new() { EmployeeId = 2,FirstName = "Ahmad",LastName = "Khan",Email = "sara.khan@gmail.com",PhoneNumber = "000222333444",
                    DateOfBirth = new DateOnly(1990, 7, 14),HireDate = new DateOnly(2015, 8, 12),Department = "Marketing",IsActive = true
                },
                new() { EmployeeId = 3,FirstName = "Ali",LastName = "Ahmed",Email = "ali.ahmed@gmail.com",PhoneNumber = "000333444555",
                    DateOfBirth = new DateOnly(1988, 5, 10),HireDate = new DateOnly(2013, 3, 22),Department = "Finance",IsActive = false
                }
            };

            _mockEmployeeRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedEmployees);

            // Act

            var result = await _employeeService.GetAll();

            // Assert

            Assert.NotNull(result);
            Assert.Equal(expectedEmployees, result);
        }

        [Fact]
        public async Task GetEmployeeById_ExistingId_ReturnsEmployee()
        {
            // Arrange

            var empId = 1;
            DateOnly dob = new(2000, 1, 31);
            DateOnly doj = new(2022, 8, 11);

            var expectedEmployee = new Employee
            {
                EmployeeId = empId,
                FirstName = "Mehran",
                LastName = "Ayub",
                Email = "mehran@gmail.com",
                PhoneNumber = "000111222333",
                DateOfBirth = dob,
                HireDate = doj,
                Department = "R & D",
                IsActive = true
            };

            _mockEmployeeRepo.Setup(repo => repo.GetByIdAsync(empId)).ReturnsAsync(expectedEmployee);


            // Act

            var result = await _employeeService.GetEmployeeById(empId);

            // Assert

            Assert.NotNull(result);
            Assert.Equal(expectedEmployee.EmployeeId, result.EmployeeId);
            Assert.Equal(expectedEmployee, result);
        }

        [Fact]
        public async Task CreateEmployee_CallsAddAndSaveChanges()
        {
            // Arrange

            var newEmployee = new Employee
            {
                EmployeeId = 2,
                FirstName = "Ahmad",
                LastName = "Khan",
                Email = "sara.khan@gmail.com",
                PhoneNumber = "000222333444",
                DateOfBirth = new DateOnly(1990, 7, 14),
                HireDate = new DateOnly(2015, 8, 12),
                Department = "Marketing",
                IsActive = true
            };

            // Act

            await _employeeService.CreateEmployee(newEmployee);

            // Assert

            _mockEmployeeRepo.Verify(repo => repo.AddAsync(newEmployee),Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployee_ExistingId_CallsDeleteAddAndSaveChanges()
        {
            // Arrange

            var empId = 1;

            var employee = new Employee { EmployeeId = empId };

            _mockEmployeeRepo.Setup(repo => repo.GetByIdAsync(empId)).ReturnsAsync(employee);

            //_mockEmployeeRepo.Setup(repo => repo.Delete(employee)).Verifiable();

            // Act

            await _employeeService.DeleteEmployee(empId);

            // Assert

            _mockEmployeeRepo.Verify(repo => repo.Delete(employee), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployee_ExistingEmployee_CallsUpdateAndSaveChanges()
        {
            // Arrange
            var employeeId = 1;

            var existingEmployee = new Employee
            {
                EmployeeId = employeeId,
                FirstName = "John",
                LastName = "Smith",
                Email = "johnsmith@gmail.com",
                PhoneNumber = "092752333464",
                DateOfBirth = new DateOnly(1990, 7, 14),
                HireDate = new DateOnly(2015, 8, 12),
                Department = "Marketing",
                IsActive = true
            };

            var updatedEmployee = new Employee
            {
                EmployeeId = employeeId,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jsmith@gmail.com",
                PhoneNumber = "092752333464",
                DateOfBirth = new DateOnly(1990, 7, 14),
                HireDate = new DateOnly(2015, 8, 12),
                Department = "Marketing",
                IsActive = false
            };


            _mockEmployeeRepo.Setup(repo => repo.GetByIdAsync(employeeId)).ReturnsAsync(existingEmployee);

            // Act

            await _employeeService.UpdateEmployee(updatedEmployee);

            // Assert

            Assert.Equal("Jane", existingEmployee.FirstName);
            Assert.Equal("jsmith@gmail.com", existingEmployee.Email);
            _mockEmployeeRepo.Verify(repo => repo.Update(existingEmployee), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChanges(), Times.Once);
        }
    }
}
