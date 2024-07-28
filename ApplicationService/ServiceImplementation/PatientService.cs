using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using DomainService.UnitOfWork;
using System.Linq.Expressions;

namespace ApplicationService.ServiceImplementation
{
    public class PatientService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public int Create(PatientDTO entity)
        {
            var model = _mapper.Map<Patient>(entity);
            _unitOfWork.PatientRepo.Create(model);
            var result = _unitOfWork.Commit();

            return model.Id;
        }

        public int Update(PatientDTO entity)
        {
            var entityModel = _unitOfWork.PatientRepo.GetWhere(e => e.Id == entity.Id).FirstOrDefault();
            if (entityModel == null)
            {
                return 0;
            }
            entityModel.Name = entity.Name;
            entityModel.BirthDate = entity.BirthDate;
            //entityModel.UpdatedDate = DateTime.Now;

            _unitOfWork.PatientRepo.Update(entityModel);
            var result = _unitOfWork.Commit();
            return result;
        }

        public int Delete(int Id)
        {
            var model = _unitOfWork.PatientRepo.GetWhere(e => e.Id == Id).SingleOrDefault();
           // model.IsDeleted = true;
            _unitOfWork.PatientRepo.Update(model);
            var result = _unitOfWork.Commit();

            return result;
        }

        public IEnumerable<PatientDTO> GetWhere(Expression<Func<Patient, bool>> where)
        {
            var data = _unitOfWork.PatientRepo.GetWhere(where);
            var mappedData = _mapper.Map<IEnumerable<PatientDTO>>(data);

            return mappedData;
        }

        public int GetWhereCount(Expression<Func<Patient, bool>> where)
        {
            var count = _unitOfWork.PatientRepo.GetWhere(where).Count();
            return count;
        }
    }
}
