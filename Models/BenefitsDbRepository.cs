using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BenefitsServices.Models
{
    public class BenefitsDbRepository
    {
        private readonly EmployeeBenefitsDbContext dbContextRepository;

        public BenefitsDbRepository()
        {
            dbContextRepository = new EmployeeBenefitsDbContext();
        }

        // Get all Employees
        public IEnumerable<Employees> GetAllEmployees()
        {
            try
            {
                return dbContextRepository.Employees.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        // Add a new Employee record
        public int AddEmployee(Employees employee)
        {
            try
            {
                dbContextRepository.Employees.Add(employee);
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
        public void SaveChanges()
        {
            dbContextRepository.SaveChanges();
        }
        // Update Employee record for a particular employee
        public int UpdateEmployee(Employees employee)
        {
            try
            {
                dbContextRepository.Entry(employee).State = EntityState.Modified;
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
        public int UpdateBenefitCost(BenefitsCost vnew)
        {
            try
            {
                dbContextRepository.Entry(vnew).State = EntityState.Modified;
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
        public int UpdateEmployee(Employees vexisting, Employees vnew)
        {
            try
            {
                dbContextRepository.Entry(vexisting).CurrentValues.SetValues(vnew);
               
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public Employees GetEmployeeData(int id)
        {
            try
            {
                Employees employee = dbContextRepository.Employees.Where(p => p.Id == id)
                    .Include(p => p.BenefitsCost)
                    .Include(p=>p.Dependents).SingleOrDefault();
                //Employees employee = dbContextRepository.Employees.Find(id);
                return employee;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public int DeleteEmployee(int id)
        {
            try
            {
                Employees employee = dbContextRepository.Employees.Find(id);
                dbContextRepository.BenefitsCost.Remove(employee.BenefitsCost);
                dbContextRepository.Dependents.RemoveRange(employee.Dependents);
                dbContextRepository.Employees.Remove(employee);
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public int AddDependent(Dependents dependent)
        {
            try
            {
                dbContextRepository.Dependents.Add(dependent);
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public int DeleteDependent(int id)
        {
            try
            {
                Dependents dependent = dbContextRepository.Dependents.Find(id);
                dbContextRepository.Dependents.Remove(dependent);
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
        public int DeleteDependentRange(int empid)
        {
            try
            {
                List<Dependents> dependents = dbContextRepository.Dependents.Where(x=>x.EmployeeId == empid).ToList();
                dbContextRepository.Dependents.RemoveRange(dependents);
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
        // Update Dependents record for a particular employee
        public int UpdateDependent(Dependents dependent)
        {
            try
            {
                dbContextRepository.Entry(dependent).State = EntityState.Modified;
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public List<Dependents> GetDependentDataForGivenEmployee(int employeeId)
        {
            try
            {
                var listOfDependentsForAnEmployee = dbContextRepository.Dependents.Where(dependent => dependent.EmployeeId == employeeId).ToList();
                return listOfDependentsForAnEmployee;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public int AddDependentDataForGivenEmployee(IEnumerable<Dependents> listOfDependents)
        {
            try
            {
                dbContextRepository.Dependents.AddRange(listOfDependents);
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public int AddBenefitsCost(BenefitsCost benefits)
        {
            try
            {
                dbContextRepository.BenefitsCost.Add(benefits);
                dbContextRepository.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public BenefitsCost GetBenefitsCostForGivenEmployee(int employeeId)
        {
            try
            {
                BenefitsCost benefits = dbContextRepository.BenefitsCost.FirstOrDefault(benefit => benefit.EmployeeId == employeeId);
                return benefits;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
