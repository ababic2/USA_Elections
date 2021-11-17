using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class Constituency
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

        public ICollection<Candidate> Candidate { get; set; }
    }
}
