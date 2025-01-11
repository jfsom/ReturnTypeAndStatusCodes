using Microsoft.AspNetCore.Mvc;
using ReturnTypeAndStatusCodes.Models;

namespace ReturnTypeAndStatusCodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Static list of employees to simulate a data source
        private static readonly List<Employee> Employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Gender = "Male", City = "New York", Age = 30, Department = "HR" },
            new Employee { Id = 2, Name = "Jane Smith", Gender = "Female", City = "Los Angeles", Age = 25, Department = "Finance" },
            new Employee { Id = 3, Name = "Mike Johnson", Gender = "Male", City = "Chicago", Age = 40, Department = "IT" }
        };

        // Read (GET all employees)
        // This action returns a list of all employees
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            // Return the list of employees with a 200 OK status
            return Ok(Employees); // OkObjectResult with data
        }

        // Read (GET employee by ID)
        // This action returns a single employee based on the provided ID
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            // Find the employee with the specified ID
            var employee = Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                // If the employee is not found, return a 404 Not Found status with a custom message
                return NotFound(new { message = $"No employee found with ID {id}" });  // NotFoundObjectResult with additional info
            }
            // If the employee is found, return it with a 200 OK status
            return Ok(employee); // OkObjectResult with the employee
        }

        // Create (POST new employee)
        // This action creates a new employee
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            // Validate the employee data
            if (employee == null || string.IsNullOrEmpty(employee.Name))
            {
                // If the data is invalid, return a 400 Bad Request status with a custom message
                return BadRequest(new { Message = "Invalid employee data" }); // BadRequestObjectResult with data
            }
            // Assign a new ID to the employee
            employee.Id = Employees.Count + 1;
            // Add the employee to the list
            Employees.Add(employee);
            // Return a 201 Created status with a location header pointing to the newly created employee
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee); // CreatedAtActionResult
        }

        // Update (PUT existing employee)
        // This action updates an existing employee based on the provided ID
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            // Validate the employee data
            if (employee == null || id != employee.Id)
            {
                // If the data is invalid, return a 400 Bad Request status with a custom message
                return BadRequest(new { Message = "Invalid employee data" }); // BadRequestObjectResult with data
            }

            // Find the existing employee with the specified ID
            var existingEmployee = Employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                // If the employee is not found, return a 404 Not Found status
                return NotFound(); // NotFoundResult
            }

            // Update the employee properties
            existingEmployee.Name = employee.Name;
            existingEmployee.Gender = employee.Gender;
            existingEmployee.City = employee.City;
            existingEmployee.Age = employee.Age;
            existingEmployee.Department = employee.Department;

            // Return a 204 No Content status to indicate that the update was successful
            return NoContent(); // NoContentResult
        }

        // Delete (DELETE employee)
        // This action deletes an employee based on the provided ID
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            // Find the employee with the specified ID
            var employee = Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                // If the employee is not found, return a 404 Not Found status
                return NotFound(); // NotFoundResult
            }
            // Remove the employee from the list
            Employees.Remove(employee);
            // Return a 200 OK status with no content
            return Ok(); // OkResult without data
        }

        // Example of a custom status code
        // This action returns a response with the specified status code
        [HttpGet("statuscode/{code}")]
        public IActionResult GetStatusCodeResult(int code)
        {
            // Return a response with the specified status code
            return StatusCode(code); // StatusCodeResult
        }
    }
}