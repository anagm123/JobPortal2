using JobPortal.Repository;
using JobPortal2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPortal2.Models;
using JobPortal2.Models.DBObjects;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace JobPortal2.Controllers
{
    [Authorize(Roles = "Candidate")]
    public class CandidateController : Controller
    {
        private CandidateRepository candidateRepository;
        public CandidateController(ApplicationDbContext dbContext)
        {
            candidateRepository = new CandidateRepository(dbContext);
        }
        // GET: CandidateController
        public ActionResult Index()
        {
            var list = candidateRepository.GetAllCandidates();
            return View(list);
        }

        // GET: CandidateController/Details/5
        [Authorize(Roles = "Candidate, Recruiter")]
        public ActionResult Details(Guid id)
        {
            var model = candidateRepository.GetCandidateId(id);
            return View("CandidateDetails", model);
        }

        // GET: CandidateController/Create
        public ActionResult Create()
        {
            return View("CreateCandidate");
        }

        // POST: CandidateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new CandidateModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    candidateRepository.InsertCandidate(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateCandidate");
            }
        }
        // GET: CandidateController/Edit/5
        [Authorize(Roles = "Candidate")]
        public ActionResult Edit(Guid id)
        {
            var model = candidateRepository.GetCandidateId(id);
            return View("EditCandidate", model);
        }

        // POST: CandidateController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new CandidateModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    candidateRepository.UpdateCandidate(model);
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

        // GET: CandidateController/Delete/5
        [Authorize(Roles = "Candidate")]
        public ActionResult Delete(Guid id)
        {
            var model = candidateRepository.GetCandidateId(id);
            return View("DeleteCandidate", model);
        }

        // POST: CandidateController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                candidateRepository.DeleteCandidate(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteCandidate", id);
            }
        }
    }
}
