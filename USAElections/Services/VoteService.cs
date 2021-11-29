using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var _vote = new Vote()
            {
                Number = vote.Number,
                CandidateId = vote.CandidateId,
                ConstituencyId = vote.ConstituencyId
            };
            _context.Vote.Add(_vote);
            _context.SaveChanges();
        }

        public int FindVote(int candidateId, int constituencyId)
        {
            var query = (from vote in _context.Vote
                         where vote.ConstituencyId == constituencyId && vote.CandidateId == candidateId
                         select vote.VoteId).FirstOrDefault();
            return query;
        }



        public void UpdateVote(int number, int id)
        {
            var vote = _context.Vote.Where(x => x.VoteId == id).FirstOrDefault();
            vote.Number = number;
            _context.Entry(vote).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
