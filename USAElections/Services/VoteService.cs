using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USAElections.Data;
using USAElections.Models;

namespace USAElections.Services
{
    public class VoteService
    {
        private ApplicationDbContext _context;
        public SqlDataReader rdr { get; set; }
        public SqlConnection con { get; set; }
        public SqlCommand cmd { get; set; }
        public VoteService(ApplicationDbContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            this.rdr = null;
            this.con = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;Database=Elections;Trusted_Connection=True;MultipleActiveResultSets=true");
            this.cmd = null;
        }

        public void AddVote(Vote vote)
        {
            //            INSERT INTO table_name(column1, column2, column3, ...)
            //VALUES(value1, value2, value3, ...);
            //var _vote = new Vote(vote.number, vote.Candidate, vote.Constituency)
            //{
            //    number = vote.number,
            //    CandidateId = vote.CandidateId,
            //    ConstituencyId = vote.ConstituencyId,
            //    Constituency = vote.Constituency,
            //    Candidate = vote.Candidate
            //};
            //_context.Vote.Add(_vote);
            //_context.SaveChanges();
            con.Open();
            string CommandText = "INSERT INTO Vote(number, CandidateId, ConstituencyId) VALUES (" +  vote.number +  ", " + vote.CandidateId + ", " + vote.ConstituencyId + ");";
            cmd = new SqlCommand(CommandText);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();
            _context.SaveChanges();
            con.Close();

        }

        public int FindVote(int candidateId, int constituencyId)
        {
            con.Open();
            string CommandText = "SELECT v.VoteId FROM Vote v WHERE v.CandidateId = " + candidateId + " and v.ConstituencyId = " + constituencyId + ";";
            cmd = new SqlCommand(CommandText);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();

            List<String> cities = new List<String>();
            while (rdr.Read())
            {
                cities.Add(rdr["VoteId"].ToString());
            }
            con.Close();
            int res;
            bool success = int.TryParse(cities[0], out res);
            return res;
        }


        public void UpdateVote(int number, int candidateId, int constituencyId)
        {
            int id = FindVote(candidateId, constituencyId);
            con.Open();
            string CommandText = "UPDATE Vote SET number = '" + number + "' WHERE VoteId = " + id + ";";
            cmd = new SqlCommand(CommandText);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();
            _context.SaveChanges();
            con.Close();
            //_voteService.UpdateVote(Int32.Parse(values[i]), canId, conId);
            //            UPDATE Customers
            //SET ContactName = 'Alfred Schmidt', City = 'Frankfurt'
            //WHERE CustomerID = 1;
        }
    }
}
