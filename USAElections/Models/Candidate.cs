using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }

        [Required]
        public String Username { get; set; }
        public String FullName { get; set; }

        // Navigation properties
        public List<CandidateConstituency> CandidateConstituency { get; set; } //many to many
        public List<Vote> Vote { get; set; } // one to many

        public Candidate(String Username)
        {
            this.Username = Username;
        }
        
    }
}
