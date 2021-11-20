using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class Constituency
    {
        [Key]
        public int ConstituencyId { get; set; }

        [Required]
        public String Name { get; set; }

        //Navigation properties
        public List<CandidateConstituency> CandidateConstituency { get; set; }
        public ICollection<Vote> Vote { get; set; }

        public Constituency(String Name)
        {
            this.Name = Name;
        }
    }
}
