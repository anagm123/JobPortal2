using JobPortal2.Repository;
using JobPortal2.Data;
using JobPortal2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPortal2.Models.DBObjects;

namespace JobPortal2.Controllers
{
    public class ApplicationController : Controller
    {
        private ApplicationRepository applicationRepository;
        public ApplicationController(ApplicationDbContext dbContext)
        {
            applicationRepository = new ApplicationRepository(dbContext);
        }

        // GET: ApplicationController
        public ActionResult Index()
        {
            return View("Index");
        }

        // GET: ApplicationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationController/Create
        public ActionResult Create()
        {
            return View("CreateApplication");
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new ApplicationModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    applicationRepository.InsertApplication(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateApplication");
            }
        }

        // GET: ApplicationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplicationController/Edit/5
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

        // GET: ApplicationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplicationController/Delete/5
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
