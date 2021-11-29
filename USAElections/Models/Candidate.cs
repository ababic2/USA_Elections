using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public Candidate(String Username, String FullName)
        {
            this.Username = Username;
            this.FullName = FullName;
        }

    }
}
