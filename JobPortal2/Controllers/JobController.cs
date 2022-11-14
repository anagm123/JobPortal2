﻿using JobPortal2.Repository;
using JobPortal2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPortal.Repository;
using JobPortal2.Models;
using System.Security.Claims;
using JobPortal2.Models.DBObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JobPortal2.Controllers
{
    public class JobController : Controller

    {
        private ApplicationDbContext _DbContext;

        private JobRepository jobRepository;

        private RecruiterRepository _recruiterRepository;

        private CandidateRepository _candidateRepository;
        public JobController(ApplicationDbContext dbContext)
        {
            jobRepository = new JobRepository(dbContext);
            _recruiterRepository = new RecruiterRepository(dbContext);
            _DbContext= dbContext;
            _candidateRepository = new CandidateRepository(dbContext);
        }
      
        // GET: JobController
        public ActionResult Index()
        {
            var list = jobRepository.GetAllJobs();
            return View(list);
        }

        // GET: JobController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: JobController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
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
    }
}
