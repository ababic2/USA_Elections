using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using USAElections.DAO;
using USAElections.Data;
using USAElections.Models;
using USAElections.Services;

namespace USAElections.Controllers
{
    public class ElectionController : Controller
    {
        public CandidateService _candidateService;
        public ConstituencyService _constituencyService;
        public VoteService _voteService;
        public CandidateConstituencyService _candidateConstituencyService;
        private readonly INotyfService _notyf;
  

        public ElectionController(CandidateService cs, ConstituencyService cos, VoteService vs, CandidateConstituencyService ccs, INotyfService notyf)
        {
            _candidateService = cs;
            _constituencyService = cos;
            _voteService = vs;
            _candidateConstituencyService = ccs;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            Queries query = new Queries();
            List<ResultHelper> results = query.GetAllResults();
            ViewModel vm = new ViewModel(query.GetAllCities(), results);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hosting)
        {
            #region Upload CSV
            if (file == null)
            {
                _notyf.Warning("Please choose file");
            }
            else
            {
                string fileName = $"{hosting.WebRootPath}\\files\\{file.FileName}";
                using (FileStream fileStream = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                #endregion
                #region Read CSV
                var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + file.FileName;
                string[] lines = System.IO.File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    var values = line.Split(',');

                    // CHECK IF RECORD IS ALREADY IN BASE AND UPDATE
                    // ELSE ADD TO BASE

                    // kako se grad ne bi pokazivao 100x na formi
                    // ispitaj da li se taj grad nalazi i vrati id ako ga pronadjes u bazi
                    // preskoci ponovno dodavanje
                    
                    bool candidateInBase = false;
                    bool constituencyInBase = false;
                    
                    Constituency constituency = new Constituency(values[0]);
                    int conId;
                    conId = _constituencyService.ChechIfCityIsInDatabase(values[0]);
                    if (conId == -1)
                    {
                        // ako nije u bazi, dodaj ga
                        conId = _constituencyService.AddConstituency(constituency);

                    } else
                    {
                        constituencyInBase = true;
                    }
                    constituency.ConstituencyId = conId;

                    
                    for (int i = 1; i < values.Length; i += 2)
                    {
                        Candidate can = new Candidate(values[i + 1]);
                        int canId;
                        canId = _candidateService.ChechIfCandidateIsInDatabase(values[i + 1]);
                        if(canId == -1)
                        {
                            canId = _candidateService.AddCandidate(can, constituency.ConstituencyId);
                            candidateInBase = false;
                        } else
                        {
                            candidateInBase = true;
                        }
                        can.CandidateId = canId;

                        if(candidateInBase && constituencyInBase)
                        {
                            _voteService.UpdateVote(Int32.Parse(values[i]), canId, conId);
                           
                        } else
                        {
                            CandidateConstituency cc = new CandidateConstituency(can, constituency);
                            _candidateConstituencyService.AddCandidateConstituency(cc);

                            Vote vote = new Vote(Int32.Parse(values[i]), can, constituency);
                            _voteService.AddVote(vote);
                        }

                    }
                }
            }
            #endregion
            return Index();
        }

        public IActionResult CheckButtonClicked(string button)
        {
            return View();  
        }
    }
}
