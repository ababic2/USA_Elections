using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }

        public int number { get; set; }

        //Navigation properties
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int ConstituencyId { get; set; }
        public Constituency Constituency { get; set; }
        public Vote()
        {

        }
        public Vote(int number, Candidate candidate, Constituency constituency)
        {
            this.number = number;
            this.CandidateId = candidate.CandidateId;
            this.ConstituencyId = constituency.ConstituencyId;
            this.Candidate = candidate;
            this.Constituency = constituency;
        }
    }
}
