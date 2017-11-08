using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS397FinalProject.Models
{
    
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public double salary { get; set; }
        public string gender { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string performance { get; set; }
        public virtual ICollection<Employee> employees { get; set; }
    }
}