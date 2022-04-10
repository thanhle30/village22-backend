using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class TaContract
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TeachingAssignment")]
        public int TeachingAssignmentId { get; set; }
        [ForeignKey("User")]
        public int TaId { get; set; }
        [Display(Name = "Date created")]
        public DateTime dateCreated { get; set; }

        // These are for ease of coding. Not related to structures of the Database
        public TeachingAssignment TeachingAssignment { get; set; }
        public User Ta { get; set; }

    }
}
