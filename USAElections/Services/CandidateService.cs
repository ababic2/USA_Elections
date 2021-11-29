using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USAElections.Data;
using USAElections.Models;

namespace USAElections.Services
{
    public class CandidateService
    {
        private ApplicationDbContext _context;
        public CandidateService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Nakon sto se pozove add nad nekim objektom i spase promjene, promijeni se i objekat
        // stoga mogu vratiti izmijenjene vrijednosti objekta kao sto je Id
        public int AddCandidate(Candidate candidate, int _constituencyId)
        {
            _context.Candidate.Add(candidate);
            _context.SaveChanges();
            return candidate.CandidateId;
          
        }
        public int ChechIfCandidateIsInDatabase(string username)
        {
            var result = from c in _context.Candidate
                         where c.Username == username
                         select c.CandidateId;
            if (result.Any())
            {
                return result.First();
            }
            return -1;

        }

        public int SumOfVotesForCandidates(string username)
        {
            var totalSums =
                from candidate in _context.Candidate
                from votes in _context.Vote
                where candidate.CandidateId == votes.CandidateId && candidate.Username == username
                select votes.number;
        }
    }
}
