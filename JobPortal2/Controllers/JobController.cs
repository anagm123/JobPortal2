using JobPortal2.Repository;
using JobPortal2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPortal.Repository;
using JobPortal2.Models;
using System.Security.Claims;
using JobPortal2.Models.DBObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace JobPortal2.Controllers
{
    public class JobController : Controller

    {
        private ApplicationDbContext _DbContext;

        private JobRepository jobRepository;

        private RecruiterRepository _recruiterRepository;

        private CandidateRepository _candidateRepository;

        private ApplicationRepository _applicationRepository;
        public JobController(ApplicationDbContext dbContext)
        {
            jobRepository = new JobRepository(dbContext);
            _recruiterRepository = new RecruiterRepository(dbContext);
            _DbContext= dbContext;
            _candidateRepository = new CandidateRepository(dbContext);
            _applicationRepository = new ApplicationRepository(dbContext);
        }
      
        // GET: JobController
        public ActionResult Index()
        {
            var list = jobRepository.GetAllJobs();
            return View(list);
        }

        // GET: JobController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = jobRepository.GetJobId(id);
            return View("JobDetails", model);
        }

        // GET: JobController/Create
        public ActionResult Create()
        {
            return View("CreateJob");
        }

        // POST: JobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new JobModel();
                if (User.Identity.IsAuthenticated)
                {
                    var email = User.Identity.Name;
                    var user = _recruiterRepository.GetRecruiterEmail(email);
                    model.IdRecruiter = user.IdRecruiter;

                }
                

                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    jobRepository.InsertJob(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateJob");
            }
        }

        // GET: JobController/Edit/5
        [Authorize(Roles = "Recruiter")]
        public ActionResult Edit(Guid id)
        {
            var model = jobRepository.GetJobId(id);
            return View("EditJob", model);
        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var user = _recruiterRepository.GetRecruiterEmail(User.Identity.Name);
                var model = new JobModel();
                var task = TryUpdateModelAsync(model);
                model.IdJob = id;
                model.IdRecruiter= user.IdRecruiter;
                task.Wait();
                if (task.Result)
                {
                    jobRepository.UpdateJob(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", id);
                }
            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }

        // GET: JobController/Delete/5
        [Authorize(Roles = "Recruiter")]
        public ActionResult Delete(Guid id)
        {
            var model = jobRepository.GetJobId(id);
            return View("DeleteJob", model);
        }

        // POST: JobController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                jobRepository.DeleteJob(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteJob", id);
            }
        }
        [Authorize(Roles = "Candidate")]
        public ActionResult Save(Guid id)
        {
            if(User.Identity.IsAuthenticated)
            {
                var list = _DbContext.Saveds.ToList();
                var saved = new Saved();
                var email = User.Identity.Name;
                var candidate = _candidateRepository.GetCandidatebyEmail(email); 
                bool alreadySaved = false;
                foreach(var item in list)
                {
                    if(item.IdJob==id && item.IdCandidate== candidate.IdCandidate)
                    {
                        alreadySaved = true;
                    }
                }
                if(alreadySaved == false)
                {
                    
                    saved.IdJob = id;
                    saved.IdCandidate = candidate.IdCandidate;
                    saved.IdSaved = Guid.NewGuid();
                    _DbContext.Saveds.Add(saved);
                    _DbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Candidate")]
        public ActionResult SavedIndex()
        {
            try
            {
                var listofJobs = new List<JobModel>();  
                if(User.Identity.IsAuthenticated)
                {
                    var email = User.Identity.Name;
                    var candidate = _candidateRepository.GetCandidatebyEmail(email);
                    var list = _DbContext.Saveds.ToList();
                    foreach(var item in list)
                    {
                        if(item.IdCandidate == candidate.IdCandidate)
                        {
                            var list2 = new List<Saved>();
                            list2.Add(item);
                            foreach(var item2 in list2)
                            {
                                listofJobs.Add(jobRepository.GetJobId(item2.IdJob));
                            }
                        }
                    }
                }
                return View(listofJobs);
            }
            catch
            {
                return View("Index");
            }
            
        }
        public ActionResult Unsave(Guid id)
        {
            if(User.Identity.IsAuthenticated)
            {
                var saved = _DbContext.Saveds.ToList();
                var candidate = _candidateRepository.GetCandidatebyEmail(User.Identity.Name);
                foreach(Saved item in saved)
                {
                    if(candidate.IdCandidate == item.IdCandidate && id == item.IdJob)
                    {
                        _DbContext.Saveds.Remove(item);
                        _DbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("SavedIndex");
        }
        public ActionResult Apply(Guid Id)
        {
            if(User.Identity.IsAuthenticated)
            {
                var application = new ApplicationModel();
                var candidate= _candidateRepository.GetCandidatebyEmail(User.Identity.Name);
                application.IdJob = Id;
                application.IdCandidate = candidate.IdCandidate;
                application.DateTimeAdded = DateTime.Now;
                _applicationRepository.InsertApplication(application);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Recruiter")]
        public ActionResult ApplicationIndex(Guid id)
        {
            var indexList = new List<ApplicationModel>();
            if (User.Identity.IsAuthenticated)
            {
                var job = jobRepository.GetJobId(id);
                var recruiter = _recruiterRepository.GetRecruiterEmail(User.Identity.Name);
                var list= _applicationRepository.GetAllApplications();
                
                foreach(var application in list)
                {
                    if(application.IdJob==id && recruiter.IdRecruiter == job.IdRecruiter)
                    {
                         indexList.Add(application);
                    }

                }

                
            }
            return View(indexList);
        }
    }
}
