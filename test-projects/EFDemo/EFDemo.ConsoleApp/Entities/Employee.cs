using System;
using System.Collections.Generic;

#nullable disable

namespace EFDemo.ConsoleApp.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            EmpDetails = new HashSet<EmpDetail>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public int DeptId { get; set; }

        public virtual Department Dept { get; set; }
        public virtual ICollection<EmpDetail> EmpDetails { get; set; }
    }
}
