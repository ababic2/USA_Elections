using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class ResultHelper
    {
        public ResultHelper(String ConstituencyName, String Number, String CandidateName )
        {
            this.ConstituencyName = ConstituencyName;
            this.NumberOfVotes = Number;
            this.CandidateUsername = CandidateName;
        }

        public String ConstituencyName { get; set; }
        public String NumberOfVotes { get; set; }
        public String CandidateUsername { get; set; }
    }
}
