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

        //public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        //{
        //    var _author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksVM()
        //    {
        //        FullName = n.FullName,
        //        BookTitles = n.Book_Authors.Select(n => n.Book.Title).ToList()
        //    }).FirstOrDefault();

        //    return _author;
        //}

    }
}
