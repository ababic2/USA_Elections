using System.ComponentModel.DataAnnotations;

namespace USAElections.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }

        public int Number { get; set; }

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
            this.Number = number;
            this.CandidateId = candidate.CandidateId;
            this.ConstituencyId = constituency.ConstituencyId;
            this.Candidate = candidate;
            this.Constituency = constituency;
        }
    }
}
