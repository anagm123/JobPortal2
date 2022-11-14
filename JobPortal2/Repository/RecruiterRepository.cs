using JobPortal2.Data;
using JobPortal2.Models.DBObjects;
using JobPortal2.Models;

namespace JobPortal2.Repository
{
    public class RecruiterRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public RecruiterRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public RecruiterRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private RecruiterModel MapDBObjectToModel(Recruiter dbobject)
        {
            var model = new RecruiterModel();
            if (dbobject != null)
            {
                model.IdRecruiter = dbobject.IdRecruiter;
                model.CompanyName = dbobject.CompanyName;
                model.Location = dbobject.Location;
                model.ContactNumber = dbobject.ContactNumber;
                model.EmailAddress= dbobject.EmailAddress;
            }
            return model;
        }
        private Recruiter MapModelToDBObject(RecruiterModel model)
        {
            var dbobject = new Recruiter();
            if (model != null)
            {
                dbobject.IdRecruiter = model.IdRecruiter;
                dbobject.CompanyName = model.CompanyName;
                dbobject.Location = model.Location;
                dbobject.ContactNumber = model.ContactNumber;
                dbobject.EmailAddress = model.EmailAddress;

            }
            return dbobject;
        }
        public List<RecruiterModel> GetAllRecruiters()
        {
            var list = new List<RecruiterModel>();
            foreach (var dbobject in _DBContext.Recruiters)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public RecruiterModel GetRecruiterId(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Recruiters.FirstOrDefault(x => x.IdRecruiter == id));
        }
        public void InsertRecruiter(RecruiterModel model)
        {
            model.IdRecruiter = Guid.NewGuid();
            _DBContext.Recruiters.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateRecruiter(RecruiterModel model)
        {
            var dbobject = _DBContext.Recruiters.FirstOrDefault(x => x.IdRecruiter == model.IdRecruiter);
            if (dbobject != null)
            {
                dbobject.IdRecruiter = model.IdRecruiter;
                dbobject.CompanyName = model.CompanyName;
                dbobject.Location = model.Location;
                dbobject.ContactNumber = model.ContactNumber;
                dbobject.EmailAddress = model.EmailAddress;

                _DBContext.SaveChanges();

            }
        }
        public void DeleteRecruiter(Guid id)
        {
            var dbobject = _DBContext.Recruiters.FirstOrDefault(x => x.IdRecruiter == id);
            if (dbobject != null)
            {
                _DBContext.Recruiters.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }
        public RecruiterModel GetRecruiterEmail(string email)
        {
            return MapDBObjectToModel(_DBContext.Recruiters.FirstOrDefault(x => x.EmailAddress == email));

        }
    }
}
