using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class TaRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TeachingAssignment")]
        public int TeachingAssignmentId { get; set; }
        public TeachingAssignment TeachingAssignment { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }
        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }
        [ForeignKey("TaRequestStatus")]
        public int StatusId { get; set; }
        public TaRequestStatus Status { get; set; }

        public ICollection<TaMatch> TaMatches { get; set; }
    }
}
