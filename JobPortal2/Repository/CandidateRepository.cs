using JobPortal2.Data;
using JobPortal2.Models.DBObjects;
using JobPortal2.Models;

namespace JobPortal.Repository
{
    public class CandidateRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public CandidateRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public CandidateRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private CandidateModel MapDBObjectToModel(Candidate dbobject)
        {
            var model = new CandidateModel();
            if (dbobject != null)
            {
                model.IdCandidate = dbobject.IdCandidate;
                model.FullName = dbobject.FullName;
                model.Resume = dbobject.Resume;
                model.Skills = dbobject.Skills;
                model.Experience = dbobject.Experience;
                model.PhoneNumber = dbobject.PhoneNumber;
                model.Location = dbobject.Location;
                model.EmailAddress=dbobject.EmailAddress;

            }
            return model;
        }
        private Candidate MapModelToDBObject(CandidateModel model)
        {
            var dbobject = new Candidate();
            if (model != null)
            {
                dbobject.IdCandidate = model.IdCandidate;
                dbobject.FullName = model.FullName;
                dbobject.Resume = model.Resume;
                dbobject.Skills = model.Skills;
                dbobject.Experience = model.Experience;
                dbobject.PhoneNumber = model.PhoneNumber;
                dbobject.Location = model.Location;
                dbobject.EmailAddress= model.EmailAddress;

            }
            return dbobject;
        }
        public List<CandidateModel> GetAllCandidates()
        {
            var list = new List<CandidateModel>();
            foreach (var dbobject in _DBContext.Candidates)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public CandidateModel GetCandidateId(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Candidates.FirstOrDefault(x => x.IdCandidate == id));
        }
        public void InsertCandidate(CandidateModel model)
        {
            model.IdCandidate = Guid.NewGuid();
            _DBContext.Candidates.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateCandidate(CandidateModel model)
        {
            var dbobject = _DBContext.Candidates.FirstOrDefault(x => x.IdCandidate == model.IdCandidate);
            if (dbobject != null)
            {
                dbobject.IdCandidate = model.IdCandidate;
                dbobject.FullName = model.FullName;
                dbobject.Resume = model.Resume;
                dbobject.Skills = model.Skills;
                dbobject.Experience = model.Experience;
                dbobject.PhoneNumber = model.PhoneNumber;
                dbobject.Location = model.Location;
                dbobject.EmailAddress = model.EmailAddress;


                _DBContext.SaveChanges();

            }
        }
        public void DeleteCandidate(Guid id)
        {
            var dbobject = _DBContext.Candidates.FirstOrDefault(x => x.IdCandidate == id);
            if (dbobject != null)
            {
                _DBContext.Candidates.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }

        public CandidateModel GetCandidatebyEmail(string email)
        {
            return MapDBObjectToModel(_DBContext.Candidates.FirstOrDefault(x => x.EmailAddress == email));
        }

    }
}
