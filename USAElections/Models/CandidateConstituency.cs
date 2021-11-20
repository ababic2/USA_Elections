using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class CandidateConstituency
    {
        public int Id { get; set; }

        public CandidateConstituency()
        {

        }
        public CandidateConstituency(Candidate candidate, Constituency constituency)
        {
            Candidate = candidate;
            Constituency = constituency;
            CandidateId = candidate.CandidateId;
            ConstituencyId = constituency.ConstituencyId;
        }

        // Navigation properties
        public int CandidateId { get; set; }
        public int ConstituencyId { get; set; }
        public Candidate Candidate { get; set; }
        public Constituency Constituency { get; set; }
    }
}
