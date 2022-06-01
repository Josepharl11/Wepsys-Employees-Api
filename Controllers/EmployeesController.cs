using Microsoft.AspNetCore.Mvc;
using WepsysEmployees.Models;
using WepsysEmployees.Crud;

namespace WepsysEmployees.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeCrud employeeCrud;

        public EmployeesController(IEmployeeCrud employeeCrud)
        { 
            this.employeeCrud = employeeCrud;
        }

        // GET Employees
        [HttpGet(Name = "GET")]
        public async Task<ActionResult<Employees>> GetAllEmployees()
        {
            try
            {
                return Ok(await employeeCrud.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was an error retrieving data from the database.");
            }
        }

        // GET Employees by Id
        [HttpGet]
        public async Task<ActionResult<Employees>> GetEmployeesById(Guid id)
        {
            try
            {
                var result = await employeeCrud.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was an error retrieving data from the database.");
            }
        }

        // POST Employees
        [HttpPost]
        public async Task<ActionResult<Employees>> CreateEmployees([FromBody]PostTheEmployees postemp)
        {
            if (postemp == null)
            {
                return BadRequest();
            }

            // Email validation, if the email exists, it means that it is already in use.
            if (employeeCrud.GetEmployeesByEmail(postemp.Email!))
            {
                ModelState.AddModelError("Email", "Sorry, that Employee's email is already in use.");
                return BadRequest(ModelState);
            }

            // Employee validation, if the lastname already exists, it means that an employee exists.
            if (employeeCrud.EmployeesExists(postemp.LastName!))
            {
                ModelState.AddModelError("LastName", "Sorry, that Employee already exists.");
                return BadRequest();
            }

            return await employeeCrud.PostEmployees(postemp);
        }

        // UPDATE Employees by Id (Email and Position)
        [HttpPut("{id}")]
        public async Task<ActionResult<Employees>> UpdateEmployee([FromRoute] Guid id, [FromBody] UpdateEmployees updateemp)
        {
                var employeeUpdate = await employeeCrud.GetById(id);

                if (employeeUpdate == null)
                    return NotFound($"Employee with Id = {id} wasn't found in the database.");

            await employeeCrud.UpdateEmployees(id, updateemp);
            return Ok();
        }

        // DELETE Employees by Id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employees>> DeleteEmployees(Guid id)
        {
            try
            {
                var employeeDelete = await employeeCrud.GetById(id);

                if (employeeDelete == null)
                {
                    return NotFound($"Employee with Id = {id} wasn't found.");
                }

                return await employeeCrud.DeleteEmployees(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Sorry, There was an error trying to delete the employee.");
            }
        }
    }
}
