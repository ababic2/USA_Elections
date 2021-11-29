using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class ViewModel
    {
        public List<String> cities { get; set; }
        public List<Tuple<string, string, string, string, string>> results { get; set; }

        public ViewModel(List<String> cities, List<Tuple<string, string, string, string, string>> results)
        {
            this.cities = cities;
            this.results = results;
        }


        private void CalculatePercentage()
        {
            for(int i = 0; i < results.Count; i++)
            {

            }
        }
    }
}
