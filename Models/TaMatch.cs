using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class TaMatch
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Request")]
        [ForeignKey("TaRequest")]
        public int TaRequestId { get; set; }
        [Display(Name = "College Student")]
        [ForeignKey("User")]
        public string TaId { get; set; }
        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Message from proposed TA")]
        public string MessageFromTa { get; set; }
        [Display(Name = "Message from the teacher")]
        public string MessageFromTeacher { get; set; }
        [ForeignKey("TaMatchStatus")]
        public int StatusId { get; set; }

        // These are for ease of coding. Not related to structures of the Database
        public User Ta { get; set; }
        public TaRequest TaRequest { get; set; }
        public TaMatchStatus Status { get; set; }
    }
}
