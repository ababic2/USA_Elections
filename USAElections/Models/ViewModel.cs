using System;
using System.Collections.Generic;
using USAElections.Services;

namespace USAElections.Models
{
    public class ViewModel
    {
        public CandidateService _candidateService;
        private Dictionary<string, string> Legend { get; set; }
        public List<String> Cities { get; set; }

        // constituency name, name, Votes, percentage, error
        public List<Tuple<string, string, string, string, string>> ViewContainer { get; set; }

        public ViewModel(List<String> cities, List<Tuple<string, string, string, string, string>> results, Dictionary<string, string> legend)
        {
            this.Cities = cities;
            this.Legend = legend;
            setResults(results);
        }

        private void setResults(List<Tuple<string, string, string, string, string>> results)
        {
            Dictionary<string, int> sums = new Dictionary<string, int>();
            CalculatePercentage(ref sums, results);

            this.ViewContainer = new List<Tuple<string, string, string, string, string>>();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Item5.Equals("-"))
                {
                    // If there is no error in record, calculate percentage
                    var percentage = Math.Round((Convert.ToDouble(results[i].Item2) / Convert.ToDouble(sums[results[i].Item3])) * 100.0);
                    this.ViewContainer.Add(new Tuple<string, string, string, string, string>(results[i].Item1, results[i].Item4, results[i].Item2, percentage.ToString() + "%", results[i].Item5));
                }
                else
                {
                    // Set percentage as N/A
                    this.ViewContainer.Add(new Tuple<string, string, string, string, string>(results[i].Item1, results[i].Item4, results[i].Item2, "N/A", results[i].Item5));
                }
            }

        }

        private void CalculatePercentage(ref Dictionary<string, int> sums, List<Tuple<string, string, string, string, string>> results)
        {
            foreach (var item in Legend)
            {
                int sum = 0;
                for (int j = 0; j < results.Count; j++)
                {
                    //if record with no error, count sum
                    if (results[j].Item5.Equals("-"))
                    {
                        if (results[j].Item3.Equals(item.Key))
                        {
                            sum += Int32.Parse(results[j].Item2);
                        }
                    }
                }
                sums.Add(item.Key, sum);
            }

        }

    }
}
