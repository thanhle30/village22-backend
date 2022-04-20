using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class TeachingAssignment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string TeacherId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        // These are for ease of coding. Not related to structures of the Database
        public Course Course { get; set; }
        public User Teacher { get; set; }

        [NotMapped]
        public string CourseName { get; }
    }
}
