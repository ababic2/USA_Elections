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
        public int Id { get; set; }

        public int number { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int ConstituencyId { get; set; }
        public Constituency Constituency { get; set; }
    }
}
