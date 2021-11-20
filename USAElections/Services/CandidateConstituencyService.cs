using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USAElections.Data;
using USAElections.Models;

namespace USAElections.Services
{
    public class CandidateConstituencyService
    {
        private ApplicationDbContext _context;
        public CandidateConstituencyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCandidateConstituency(CandidateConstituency candidateConstituency)
        {
            var _candidate_constituency = new CandidateConstituency()
            {
                CandidateId = candidateConstituency.CandidateId,
                ConstituencyId = candidateConstituency.ConstituencyId
            };
            _context.CandidateConstituency.Add(_candidate_constituency);
            _context.SaveChanges();
        }
    }
}
