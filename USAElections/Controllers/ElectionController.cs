using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USAElections.Data;

namespace USAElections.Controllers
{
    public class ElectionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ElectionController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var query = (from article in Articles
            //             from category in article.Categories.Where(x => x.Category_ID == category_id)
            //             select article);
            //var episodes = (from constituency in _db.Constituency
            //                from candidate in _db.Candidate.Where(x => x.Id == constituency.Id
            //                select constituency.Name)
            //          .AsNoTracking()
            //          .ToList();
            SqlDataReader rdr = null;
            SqlConnection con = null;
            SqlCommand cmd = null;
            String FirstName = null;
            String LastName;
            string ConnectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=Elections;Trusted_Connection=True;MultipleActiveResultSets=true";
            con = new SqlConnection(ConnectionString);
            con.Open();
            string CommandText = "SELECT c.Name, v.number, k.Username  FROM Constituency c, Candidate k, CandidateConstituency j, Vote v WHERE c.Id = v.ConstituencyId and k.Id = v.CandidateId and c.Id = j.ConstituencyId and k.Id = j.CandidateId; ";
            cmd = new SqlCommand(CommandText);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();
            // Fill the string with the values retrieved
            // Kreiraj objekat ovog tipa, napravi listu i posalji je tamo u view
            while (rdr.Read())
            {
                FirstName = rdr["Name"].ToString(); 
                LastName = rdr["Username"].ToString();
            }
            Console.WriteLine(FirstName);

            return View();
        }
    }
}
