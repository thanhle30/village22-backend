using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class TaRequestViewModel
    {
        public int CourseId { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public int StatusId { get; set; }
    }
}
