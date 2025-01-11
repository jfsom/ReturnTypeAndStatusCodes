using Microsoft.AspNetCore.Mvc;
using ReturnTypeAndStatusCodes.Models;
namespace ReturnTypeAndStatusCodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //As we are going to return Ok and NotFound Result from this action method
        //So, we are using IActionResult as the return type of this method
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Employee>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllEmployee()
        {
            try
            {
                //In Real-Time, you will get the data from the database
                //Here, we have hardcoded the data
                var listEmployees = new List<Employee>()
                {
                    new Employee(){ Id = 1001, Name = "Anurag", Age = 28, City = "Mumbai", Gender = "Male", Department = "IT" },
                    new Employee(){ Id = 1002, Name = "Pranaya", Age = 28, City = "Delhi", Gender = "Male", Department = "IT" },
                    new Employee(){ Id = 1003, Name = "Priyanka", Age = 27, City = "BBSR", Gender = "Female", Department = "HR"},
                };

                //If at least one Employee is Present return OK status code and the list of employees
                if (listEmployees.Any())
                {
                    return Ok(listEmployees);
                }
                else
                {
                    //If no Employee is Present return Not Found Status Code
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while getting all employees.");

                // Return a 500 Internal Server Error status code
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //As the following method is going to return Ok and NotFound Result
        //So, we are using IActionResult as the return type of this method
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetEmployeeDetails(int Id)
        {
            try
            {
                //In Real-Time, you will get the data from the database
                //Here, we have hardcoded the data
                var listEmployees = new List<Employee>()
                {
                    new Employee(){ Id = 1001, Name = "Anurag", Age = 28, City = "Mumbai", Gender = "Male", Department = "IT" },
                    new Employee(){ Id = 1002, Name = "Pranaya", Age = 28, City = "Delhi", Gender = "Male", Department = "IT" },
                    new Employee(){ Id = 1003, Name = "Priyanka", Age = 27, City = "BBSR", Gender = "Female", Department = "HR"},
                };

                //Fetch the Employee Data based on the Employee Id
                var employee = listEmployees.FirstOrDefault(emp => emp.Id == Id);

                //If Employee Exists Return OK with the Employee Data
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    //If Employee Does Not Exists Return NotFound
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while getting all employees.");

                // Return a 500 Internal Server Error status code
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}