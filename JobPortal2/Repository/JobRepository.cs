using JobPortal2.Data;
using JobPortal2.Models.DBObjects;
using JobPortal2.Models;

namespace JobPortal2.Repository
{
    public class JobRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public JobRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public JobRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private JobModel MapDBObjectToModel(Job dbobject)
        {
            var model = new JobModel();
            if (dbobject != null)
            {
                model.IdJob = dbobject.IdJob;
                model.JobTitle = dbobject.JobTitle;
                model.Description = dbobject.Description;
                model.IdRecruiter = dbobject.IdRecruiter;
                model.DateTimeAdded = dbobject.DateTimeAdded;
            }
            return model;
        }
        private Job MapModelToDBObject(JobModel model)
        {
            var dbobject = new Job();
            if (model != null)
            {
                dbobject.IdJob = model.IdJob;
                dbobject.JobTitle = model.JobTitle;
                dbobject.Description = model.Description;
                dbobject.IdRecruiter = model.IdRecruiter;
                dbobject.DateTimeAdded = model.DateTimeAdded;
            }
            return dbobject;
        }
        public List<JobModel> GetAllJobs()
        {
            var list = new List<JobModel>();
            foreach (var dbobject in _DBContext.Jobs)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public JobModel GetJobId(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Jobs.FirstOrDefault(x => x.IdJob == id));
        }
        public void InsertJob(JobModel model)
        {
            model.IdJob = Guid.NewGuid();
            _DBContext.Jobs.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateJob(JobModel model)
        {
            var dbobject = _DBContext.Jobs.FirstOrDefault(x => x.IdJob == model.IdJob);
            if (dbobject != null)
            {
                dbobject.IdJob = model.IdJob;
                dbobject.JobTitle = model.JobTitle;
                dbobject.Description = model.Description;
                dbobject.IdRecruiter = model.IdRecruiter;
                dbobject.DateTimeAdded = model.DateTimeAdded;
                _DBContext.SaveChanges();

            }
        }
        public void DeleteJob(Guid id)
        {
            var dbobject = _DBContext.Jobs.FirstOrDefault(x => x.IdJob == id);
            if (dbobject != null)
            {
                _DBContext.Jobs.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }
    }
}
