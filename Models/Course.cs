using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_LINQ2.Models
{
    internal class Course
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
        
    }
}
