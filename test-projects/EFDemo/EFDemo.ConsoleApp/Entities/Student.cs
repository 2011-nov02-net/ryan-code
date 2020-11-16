using System;
using System.Collections.Generic;

#nullable disable

namespace EFDemo.ConsoleApp.Entities
{
    public partial class Student
    {
        public Student()
        {
            CourseStudents = new HashSet<CourseStudent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
