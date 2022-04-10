using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Village22.Models
{
    public class User : IdentityUser
    {
        //inherits properties from IdentityUser class
        
        public String FirstName { get; set; }
        public String LastName { get; set; }

        [NotMapped]
        public IList<string> RoleNames { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<TaMatch> TaMatches { get; set; }
        public ICollection<TaContract> TaContracts { get; set; }
        [NotMapped]
        public String Name {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}
