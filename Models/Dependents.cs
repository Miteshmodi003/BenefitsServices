using System;
using System.Collections.Generic;

namespace BenefitsServices.Models
{
    public partial class Dependents
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }

        public virtual Employees Employee { get; set; }

        public static Dependents Create(int vEmployeeId, string vFirstName, string vLastName, string vRelationship)
        {
            Dependents result = new Dependents();
            result.EmployeeId = vEmployeeId;
            result.FirstName = vFirstName;
            result.LastName = vLastName;
            result.Relationship = vRelationship;

            return result;

        }
        public void Update(int vEmployeeId, string vFirstName, string vLastName, string vRelationship)
        {
            FirstName = vFirstName;
            LastName = vLastName;
            EmployeeId = vEmployeeId;
            Relationship = vRelationship;

        }
    }
}
