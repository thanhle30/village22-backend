using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class TaMatchStatus
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Status description")]
        public string Name { get; set; }

        public ICollection<TaMatch> TaMatches { get; set; }
    }
}
