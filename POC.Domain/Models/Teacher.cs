using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCNT.Domain.Models
{
    public class Teacher
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        public int AdminId { get; set; }

        public Admin Admin { get; set; }
    }
}

