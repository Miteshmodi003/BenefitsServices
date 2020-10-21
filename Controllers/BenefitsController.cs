using System.Collections.Generic;
using System.Linq;
using BenefitsServices.BusinessRulesUtil;
using BenefitsServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BenefitsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitsController : ControllerBase
    {
        private readonly ILogger<BenefitsController> _logger;
        private BenefitsDbRepository db = new BenefitsDbRepository();

        public BenefitsController(ILogger<BenefitsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Employees> Get()
        {
            var employees = db.GetAllEmployees().ToList();

            employees?.ForEach(employee =>
            {
                var dependentsData = db.GetDependentDataForGivenEmployee(employee.Id);
                var benefitData = db.GetBenefitsCostForGivenEmployee(employee.Id);
            });

            return employees;
        }

        // GET api/<OverviewController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OverviewController>
        [HttpPost]
        public IActionResult Create([FromBody] Employees employee)
        {
            Employees objEmployee = employee;

            if (objEmployee != null)
            {
                CalculationUtil.SetDeductionAmounts(objEmployee);
                db.AddEmployee(objEmployee);
            }

            return Ok(new
            {
                success = true,
                returncode = "200"
            });
        }

        // PUT api/<OverviewController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employees employee)
        {
            if (employee == null)
            {
                return BadRequest("employee object is null");
            }
            var existingEmp = db.GetEmployeeData(id);
            if (existingEmp == null)
            {
                return NotFound();
            }
            if (existingEmp != null)
            {
                // Update employee
                existingEmp.Update(employee.FirstName,employee.LastName,employee.AnnualCompensation);
                db.UpdateEmployee(existingEmp);
                // Delete dependents and create new ones.
                db.DeleteDependentRange(id);
                db.AddDependentDataForGivenEmployee(employee.Dependents);

                // Update benefit cost
                existingEmp.BenefitsCost.Update(employee.BenefitsCost.EmployeeId,employee.BenefitsCost.BiWeeklyDeduction,employee.BenefitsCost.YearlyBenefitsCost);
                db.UpdateBenefitCost(existingEmp.BenefitsCost);
                
            }

            return Ok(new
            {
                success = true,
                returncode = "200"
            });
        }

        // DELETE api/<OverviewController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                var existingEmp = db.GetEmployeeData(id);
                if (existingEmp == null)
                {
                    return NotFound();
                }
                db.DeleteEmployee(id);
            }

            return Ok(new
            {
                success = true,
                returncode = "200"
            });
        }
    }
}