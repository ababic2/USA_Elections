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
            // OVO BACI STRESS IZUZETAK
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
            string CommandText = "INSERT INTO Vote(number, CandidateId, ConstituencyId) VALUES (" + vote.number + ", " + vote.CandidateId + ", " + vote.ConstituencyId + ");";
            cmd = new SqlCommand(CommandText);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();
            _context.SaveChanges();
            con.Close();

        }

        public int FindVote(int candidateId, int constituencyId)
        {
            var query = (from vote in _context.Vote
                         where vote.ConstituencyId == constituencyId && vote.CandidateId == candidateId
                         select vote.VoteId).First();
            return query;
        }


        public void UpdateVote(int number, int candidateId, int constituencyId)
        {
            int id = FindVote(candidateId, constituencyId);

            // OVO NE RADI
            //_context.Vote.Where(vote => vote.VoteId == id).First().number = number;
            //_context.SaveChanges();


            con.Open();
            string CommandText = "UPDATE Vote SET number = '" + number + "' WHERE VoteId = " + id + ";";
            cmd = new SqlCommand(CommandText);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();
            _context.SaveChanges();
            con.Close();
        }
    }
}
