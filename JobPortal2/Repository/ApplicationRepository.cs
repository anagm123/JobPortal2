using JobPortal2.Data;
using JobPortal2.Models;
using JobPortal2.Models.DBObjects;

namespace JobPortal2.Repository
{
    public class ApplicationRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public ApplicationRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public ApplicationRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private ApplicationModel MapDBObjectToModel(Application dbobject)
        {
            var model = new ApplicationModel();
            if (dbobject != null)
            {
                model.IdApplication = dbobject.IdApplication;
                model.DateTimeAdded = dbobject.DateTimeAdded;
                model.IdJob = dbobject.IdJob;
                model.IdCandidate = dbobject.IdCandidate;
            }
            return model;
        }
        private Application MapModelToDBObject(ApplicationModel model)
        {
            var dbobject = new Application();
            if (model != null)
            {
                dbobject.IdApplication = model.IdApplication;
                dbobject.DateTimeAdded = model.DateTimeAdded;
                dbobject.IdJob = model.IdJob;
                dbobject.IdCandidate = model.IdCandidate;
            }
            return dbobject;
        }
        public List<ApplicationModel> GetAllApplications()
        {
            var list = new List<ApplicationModel>();
            foreach (var dbobject in _DBContext.Applications)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public ApplicationModel GetApplicationId(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Applications.FirstOrDefault(x => x.IdApplication == id));
        }
        public void InsertApplication(ApplicationModel model)
        {
            model.IdApplication = Guid.NewGuid();
            _DBContext.Applications.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateApplication(ApplicationModel model)
        {
            var dbobject = _DBContext.Applications.FirstOrDefault(x => x.IdApplication == model.IdApplication);
            if (dbobject != null)
            {
                dbobject.IdApplication = model.IdApplication;
                dbobject.DateTimeAdded = model.DateTimeAdded;
                dbobject.IdJob = model.IdJob;
                dbobject.IdCandidate = model.IdCandidate;

            }
        }
        public void DeleteApplication(Guid id)
        {
            var dbobject = _DBContext.Applications.FirstOrDefault(x => x.IdApplication == id);
            if (dbobject != null)
            {
                _DBContext.Applications.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }

    }
}
