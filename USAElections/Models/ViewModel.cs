using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class ViewModel
    {
        public List<String> cities { get; set; }
        public List<ResultHelper> results { get; set; }
        public ViewModel(List<String> cities, List<ResultHelper> results)
        {
            this.cities = cities;
            this.results = results;
        }
     
    }
}
