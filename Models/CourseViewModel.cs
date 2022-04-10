using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Date Start")]
        public DateTime DateStart { get; set; }
        [Display(Name = "Date End")]
        public DateTime DateEnd { get; set; }

        // just one course, exactly one teacher for now. Extend this if have time
        [Display(Name = "Teacher")]
        public string TeacherId { get; set; }
        [Display(Name = "Teacher's name")]
        public string TeacherName { get; set; }
    }
}
