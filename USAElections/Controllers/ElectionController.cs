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
using USAElections.Data;
using USAElections.Models;
using USAElections.Services;

namespace USAElections.Controllers
{
    public class ElectionController : Controller
    {
        public Dictionary<string, string> legend { get; set; }
        public CandidateService _candidateService;
        public ConstituencyService _constituencyService;
        public VoteService _voteService;
        public CandidateConstituencyService _candidateConstituencyService;
        public FileService _fileService;
        public DataAccessService _dataAccessService;
        private readonly INotyfService _notyf;
  

        public ElectionController(CandidateService cs, ConstituencyService cos, VoteService vs, CandidateConstituencyService ccs, DataAccessService dataAccessService, INotyfService notyf)
        {
            setCandidateLegend();
            _candidateService = cs;
            _constituencyService = cos;
            _voteService = vs;
            _candidateConstituencyService = ccs;
            _dataAccessService = dataAccessService;
            _notyf = notyf;
            _fileService = new FileService();
        }

        public IActionResult Index()
        {
            string errorPath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\errorLog"}" + "\\" + "errors.txt";
            List<Tuple<string, string, string, string, string>> results = _dataAccessService.GetAllResults();
            
            // add results from error file and show on view
            results.AddRange(_fileService.ReadErrorFile(errorPath, legend));

            ViewModel vm = new ViewModel(_constituencyService.GetAllCities(), results, legend);

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
                string[] lines = _fileService.UploadAndReadCSVFile(file, hosting);

                foreach (string line in lines)
                {
                    var values = line.Split(',');
                    bool candidateInBase = false;
                    bool constituencyInBase = false;

                    #region Get Constituency ID and/or add to database

                    Constituency constituency = new Constituency(values[0]);
                    _constituencyService.SetOrAddConstituency(ref constituency, ref constituencyInBase);
                   
                    #endregion
                    
                    for (int i = 1; i < values.Length; i += 2)
                    {
                        if (isNumber(values[i]))
                        {
                            #region Get Candidate ID and/or add candidate to database
          
                            Candidate candidate = new Candidate(values[i + 1], legend[values[i + 1]]);
                            _candidateService.SetOrAddCandidate(ref candidate, ref candidateInBase);

                            #endregion

                            int voteInDatabase = _voteService.FindVote(candidate.CandidateId, constituency.ConstituencyId);
                            if (candidateInBase && constituencyInBase && voteInDatabase != 0)
                            {
                                _voteService.UpdateVote(Int32.Parse(values[i]), voteInDatabase);                             
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
                            string deleteLine = constituency.Name + "," + values[i + 1];
                            checkIfRecordExistsInErrorLog(deleteLine, file, hosting);
                        } 
                        else
                        {
                            string writeLine = constituency.Name + "," + values[i + 1] + "," + values[i] + "," + "Format Error";
                            _fileService.CreateOrFillErrorLogFile(writeLine);
                        }

                    }
                }
            }
            return Index();
        }

        private void checkIfRecordExistsInErrorLog(string deleteLine, IFormFile file, IHostingEnvironment hosting)
        {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\errorLog"}" + "\\" + "errors.txt";

            if (System.IO.File.Exists(path))
            {
                List<String> lst = System.IO.File.ReadAllLines(path).Where(line => !line.Contains(deleteLine)).ToList();
                System.IO.File.WriteAllLines(path, lst);
            }
            
        }

        private Boolean isNumber(string voteValue)
        {
            return Regex.IsMatch(voteValue, @"^ [0-9][0-9]*$");
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
