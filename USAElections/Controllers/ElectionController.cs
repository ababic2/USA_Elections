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
using System.Text.RegularExpressions;
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
        public FileService _fileService;
        private readonly INotyfService _notyf;
  

        public ElectionController(CandidateService cs, ConstituencyService cos, VoteService vs, CandidateConstituencyService ccs, INotyfService notyf)
        {
            _candidateService = cs;
            _constituencyService = cos;
            _voteService = vs;
            _candidateConstituencyService = ccs;
            _notyf = notyf;
            _fileService = new FileService();
        }

        public IActionResult Index()
        {
            string errorPath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\errorLog"}" + "\\" + "errors.txt";
            Queries query = new Queries();
            List<Tuple<string, string, string, string>> results = query.GetAllResults();
            if (System.IO.File.Exists(errorPath))
            {
                using (StreamReader sr = System.IO.File.OpenText(errorPath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        var values = s.Split(',');
                        results.Add(new Tuple<string, string, string, string>(values[0], values[2], values[1], values[3]));
                    }
                }
            }

            ViewModel vm = new ViewModel(query.GetAllCities(), results);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hosting)
        {
            if (file == null)
            {
                _notyf.Warning("Please choose file");
            }
            else
            {
                string[] lines = _fileService.uploadAndReadCSVFile(file, hosting);

                foreach (string line in lines)
                {
                    var values = line.Split(',');
                    bool candidateInBase = false;
                    bool constituencyInBase = false;

                    #region Get Constituency ID and/or add to database
                    Constituency constituency = new Constituency(values[0]);
                    int constituencyId = _constituencyService.ChechIfCityIsInDatabase(values[0]);
                    if (constituencyId == -1)
                    {
                        // ako nije u bazi, dodaj ga
                        constituencyId = _constituencyService.AddConstituency(constituency);

                    } else
                    {
                        constituencyInBase = true;
                    }
                    constituency.ConstituencyId = constituencyId;
                    #endregion


                    for (int i = 1; i < values.Length; i += 2)
                    {
                        if (isNumber(values[i]))
                        {
                            #region Get Candidate ID and/or add candidate to database
                            // if candidate is already in database -> get  Id
                            // otherwise, add candidate ->  get Id

                            Candidate candidate = new Candidate(values[i + 1]);
                            int candidateId = _candidateService.ChechIfCandidateIsInDatabase(values[i + 1]);
                            if (candidateId == -1)
                            {
                                candidateId = _candidateService.AddCandidate(candidate, constituency.ConstituencyId);
                                candidateInBase = false;
                            }
                            else
                            {
                                candidateInBase = true;
                            }
                            candidate.CandidateId = candidateId;

                            #endregion

                            if (candidateInBase && constituencyInBase)
                            {
                                _voteService.UpdateVote(Int32.Parse(values[i]), candidateId, constituencyId);

                            }
                            else
                            {
                                #region Add to Junction and Vote table

                                CandidateConstituency cc = new CandidateConstituency(candidate, constituency);
                                _candidateConstituencyService.AddCandidateConstituency(cc);

                                Vote vote = new Vote(Int32.Parse(values[i]), candidate, constituency);
                                _voteService.AddVote(vote);
                                #endregion
                            }
                        } 
                        else
                        {
                            string writeLine = constituency.Name + "," + values[i + 1] + "," + values[i] + "," + "Format Error";
                            _fileService.createOrFillErrorLogFile(writeLine);
                        }

                    }
                }
            }
            return Index();
        }

        private Boolean isNumber(string voteValue)
        {
            return Regex.IsMatch(voteValue, @"^ [0-9][0-9]*$");
        }
    }
}
