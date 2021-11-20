using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USAElections.Data;
using USAElections.Models;

namespace USAElections.Services
{
    public class VoteService
    {
        private ApplicationDbContext _context;
        public VoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddVote(Vote vote)
        {
            var _vote = new Vote(vote.number, vote.Candidate, vote.Constituency)
            {
                number = vote.number,
                CandidateId = vote.CandidateId,
                ConstituencyId = vote.ConstituencyId,
                Constituency = vote.Constituency,
                Candidate = vote.Candidate
            };
            _context.Vote.Add(_vote);
            _context.SaveChanges();

        }
    }
}
