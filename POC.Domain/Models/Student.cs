using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCNT.Domain.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(5, 100)]
        public int Age { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
