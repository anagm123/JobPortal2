using JobPortal.Repository;
using JobPortal2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPortal2.Models;
using JobPortal2.Models.DBObjects;
using JobPortal2.Repository;

namespace JobPortal2.Controllers
{
    public class RecruiterController : Controller
    {
        private RecruiterRepository recruiterRepository;
        public RecruiterController(ApplicationDbContext dbContext)
        {
            recruiterRepository = new RecruiterRepository(dbContext);
        }
        // GET: RecruiterController
        public ActionResult Index()
        {
            var list = recruiterRepository.GetAllRecruiters();
            return View(list);
        }

        // GET: RecruiterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecruiterController/Create
        public ActionResult Create()
        {
            return View("CreateRecruiter");
        }

        // POST: RecruiterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new RecruiterModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    recruiterRepository.InsertRecruiter(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateRecruiter");
            }
        }

        // GET: RecruiterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecruiterController/Edit/5
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

        // GET: RecruiterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecruiterController/Delete/5
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
