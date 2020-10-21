using System;
using System.Collections.Generic;

namespace BenefitsServices.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Dependents = new HashSet<Dependents>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float AnnualCompensation { get; set; }

        public virtual BenefitsCost BenefitsCost { get; set; }
        public virtual ICollection<Dependents> Dependents { get; set; }


        public static Employees Create(string vFirstName, string vLastName, float vAnnualCompensation)
        {
            Employees result = new Employees();
            result.FirstName = vFirstName;
            result.LastName = vLastName;
            result.AnnualCompensation = vAnnualCompensation;

            return result;

        }
        public void Update(string vFirstName, string vLastName, float vAnnualCompensation)
        {
            FirstName = vFirstName;
            LastName = vLastName;
            AnnualCompensation = vAnnualCompensation;

        }
    }
    

   
    }

