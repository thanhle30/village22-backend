using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
  
        [Display(Name = "Date Start")]
        public DateTime DateStart { get; set; }
        [Display(Name = "Date End")]
        public DateTime DateEnd { get; set; }

        ///////////// these are not in here anymore. May implement them for TeachingAssignment
        //public ICollection<TaRequest> TaRequests { get; set; }
        //public ICollection<TaContract> TaContracts { get; set; }
    }
}
