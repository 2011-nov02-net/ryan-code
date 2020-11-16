using System;
using System.Collections.Generic;

#nullable disable

namespace EFDemo.ConsoleApp.Entities
{
    public partial class CourseStudent
    {
        public string CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
