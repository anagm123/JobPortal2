using JobPortal2.Repository;
using JobPortal2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPortal.Repository;
using JobPortal2.Models;

namespace JobPortal2.Controllers
{
    public class JobController : Controller

    {
        private JobRepository jobRepository;
        public JobController(ApplicationDbContext dbContext)
        {
            jobRepository = new JobRepository(dbContext);
        }
        // GET: JobController
        public ActionResult Index()
        {
            //var list = jobRepository.GetAllJobs();
            //return View(list);
            return View("Index");
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
    }
}
