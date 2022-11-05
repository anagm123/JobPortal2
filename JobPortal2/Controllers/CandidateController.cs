using JobPortal.Repository;
using JobPortal2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPortal2.Models;
using JobPortal2.Models.DBObjects;

namespace JobPortal2.Controllers
{
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
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CandidateController/Edit/5
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

        // GET: CandidateController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CandidateController/Delete/5
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
    }
}
