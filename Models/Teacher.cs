using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_LINQ2.Models
{
    internal class Teacher
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }



    }
}
