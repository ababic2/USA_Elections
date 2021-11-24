using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Models
{
    public class ViewModel
    {
        public List<String> cities { get; set; }
        public List<Tuple<string, string, string, string>> results { get; set; }

        public Dictionary<string, string> legend { get; set; }

        public ViewModel(List<String> cities, List<Tuple<string, string, string, string>> results)
        {
            this.cities = cities;
            this.results = results;
            setCandidateLegend();
            setCandidateNameInResults();
        }

        private void setCandidateNameInResults()
        {
            for (int i = 0; i < results.Count; i++)
            {
                Tuple<string, string, string, string> currentTuple = results[i];
                results[i] = new Tuple<string, string, string, string>(currentTuple.Item1, currentTuple.Item2, legend[currentTuple.Item3], currentTuple.Item4);
            }
        }

        private void setCandidateLegend()
        {
            this.legend = new Dictionary<string, string>();
            legend.Add(" DT", "Donald Trump");
            legend.Add(" HC", "Hilary Clinton");
            legend.Add(" JB", "Joe Biden");
            legend.Add(" JFK", "John F.Kennedy");
            legend.Add(" JR", "Jack Randall");
        }

    }
}
