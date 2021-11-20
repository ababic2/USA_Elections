using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USAElections.Models;

namespace USAElections.DAO
{
    public class Queries
    {
        public SqlDataReader rdr { get; set; }
        public SqlConnection con { get; set; }
        public SqlCommand cmd { get; set; }
        public Queries()
        {
            this.rdr = null;
            this.con = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;Database=Elections;Trusted_Connection=True;MultipleActiveResultSets=true");
            this.cmd = null;
        }
        public List<ResultHelper> GetAllResults()
        {
            con.Open();
            string CommandText = "SELECT c.Name, v.number, k.Username  FROM Constituency c, Candidate k, CandidateConstituency j, Vote v WHERE c.ConstituencyId = v.ConstituencyId and k.CandidateId = v.CandidateId and c.ConstituencyId = j.ConstituencyId and k.CandidateId = j.CandidateId; ";
            cmd = new SqlCommand(CommandText);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();
   
            List<ResultHelper> results = new List<ResultHelper>();
            while (rdr.Read())
            {
                ResultHelper result = new ResultHelper(rdr["Name"].ToString(), rdr["number"].ToString(), rdr["Username"].ToString());
                results.Add(result);
            }
            con.Close();
            return results;
        }

        public List<String> GetAllCities()
        {
            con.Open();
            string CommandText = "SELECT DISTINCT c.Name FROM Constituency c; ";
            cmd = new SqlCommand(CommandText);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();

            List<String> cities = new List<String>();
            while (rdr.Read())
            {
                cities.Add(rdr["Name"].ToString());
            }
            con.Close();
            return cities;
        }
    }
}
