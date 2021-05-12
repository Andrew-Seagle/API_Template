using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Mock_BestBuy_API
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _connection;

        public EmployeeRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _connection.Query<Employee>("SELECT * FROM bestbuy.employees;");
        }

        public Employee GetEmployee(int id)
        {
            return _connection.QuerySingleOrDefault<Employee>("SELECT * FROM bestbuy.employees WHERE EmployeeID = @id;", 
                new { id = id });
        }

        public void InsertEmployee(Employee employee)
        {
            _connection.Execute("INSERT INTO bestbuy.employees (FirstName, MiddleInitial, LastName, EmailAddress, PhoneNumber, Title, DateOfBirth)" +
                                " VALUES (@fName, @mInitial, @lName, @email, @pNumber, @title, @DoB);",
                new { fName = employee.FirstName, mInitial = employee.MiddleInitial, lName = employee.LastName, email = employee.EmailAddress, 
                      pNumber = employee.PhoneNumber, title = employee.Title, DoB = employee.DateOfBirth });
        }

        public void UpdateEmployee(Employee employee)
        {
            _connection.Execute("UPDATE bestbuy.employees SET EmailAddress = @email, PhoneNumber = @pNumber, Title = @title WHERE EmployeeID = @id",
                new { id = employee.EmployeeID, email = employee.EmailAddress, pNumber = employee.PhoneNumber, title = employee.Title });
        }

        public void DeleteEmployee(Employee employee)
        {
            _connection.Execute("DELETE from bestbuy.employees WHERE EmployeeID = @id", 
                new { id = employee.EmployeeID });
        }
    }
}
