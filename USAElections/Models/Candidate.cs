using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Username { get; set; }
        public String FullName { get; set; }

        public ICollection<Constituency> Constituency { get; set; }
        public ICollection<Vote> Vote { get; set; }
    }
}
