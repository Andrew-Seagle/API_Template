using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Mock_BestBuy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = _repo.GetAllEmployees();
            return Ok(JsonConvert.SerializeObject(employees));
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _repo.GetEmployee(id);
            return Ok(JsonConvert.SerializeObject(employee));
        }

        [HttpPost]
        public void Post(Employee employee)
        {
            employee.EmployeeID = ++_repo.GetAllEmployees().LastOrDefault().EmployeeID;
            _repo.InsertEmployee(employee);
        }

        [HttpPut("{id}")]
        public void Put(int id, Employee employee)
        {
            employee.EmployeeID = id;
            _repo.UpdateEmployee(employee);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var EmployeeToDelete = _repo.GetEmployee(id);
            _repo.DeleteEmployee(EmployeeToDelete);
        }
    }
}
