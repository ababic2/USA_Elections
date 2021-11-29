using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USAElections.Data;

namespace USAElections.Services
{
    public class DataAccessService
    {
        private ApplicationDbContext _context;
        public DataAccessService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Tuple<string, string, string, string, string>> GetAllResults()
        {
            var query = (from c in _context.Constituency
                         from v in _context.Vote
                         from k in _context.Candidate
                         from j in _context.CandidateConstituency
                         where c.ConstituencyId == v.ConstituencyId && k.CandidateId == v.CandidateId && c.ConstituencyId == j.ConstituencyId && k.CandidateId == j.CandidateId
                         select new { c.Name, v.number, k.Username, k.FullName }).ToList();
            List<Tuple<string, string, string, string, string>> results = new List<Tuple<string, string, string, string, string>>();
            for (int i = 0; i < query.Count; i++)
                results.Add(new Tuple<string, string, string, string, string>(query[i].Name.ToString(), query[i].number.ToString(), query[i].Username.ToString(), query[i].FullName.ToString(), "-"));
            return results;
        }
    }
}
