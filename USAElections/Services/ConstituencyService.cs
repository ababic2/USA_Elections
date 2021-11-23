using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USAElections.Data;
using USAElections.Models;

namespace USAElections.Services
{
    public class ConstituencyService
    {
        private ApplicationDbContext _context;
        public ConstituencyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int AddConstituency(Constituency constituency)
        {
            _context.Constituency.Add(constituency);
            _context.SaveChanges();
            return constituency.ConstituencyId;
        }

        public int ChechIfCityIsInDatabase(string name)
        {
            var result = from c in _context.Constituency
                         where c.Name == name
                         select c.ConstituencyId;
          
            if(result.Any())
            {
                return result.First();
            }
            return -1;
        }
    }
}
