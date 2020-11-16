using System;
using System.Collections.Generic;

#nullable disable

namespace EFDemo.ConsoleApp.Entities
{
    public partial class Teacher
    {
        public Teacher()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
