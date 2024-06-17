using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using DomainService.UnitOfWork;
using System.Linq.Expressions;

namespace ApplicationService.ServiceImplementation
{
    public class DoctorService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public int Create(DoctorDTO entity)
        {
            var model = _mapper.Map<Doctor>(entity);
            _unitOfWork.DoctorRepo.Create(model);
            var result = _unitOfWork.Commit();

            return model.Id;
        }

        public int Update(DoctorDTO entity)
        {
            var entityModel = _unitOfWork.DoctorRepo.GetWhere(e => e.Id == entity.Id).FirstOrDefault();
            if (entityModel == null)
            {
                return 0;
            }
            entityModel.Name = entity.Name;
            entityModel.StartDate = entity.StartDate;
            entityModel.EndDate = entity.EndDate;
            entityModel.ClinicId = entity.ClinicId;
            entityModel.UpdatedDate = DateTime.Now;

            _unitOfWork.DoctorRepo.Update(entityModel);
            var result = _unitOfWork.Commit();
            return result;
        }

        public int Delete(int Id)
        {
            var model = _unitOfWork.DoctorRepo.GetWhere(e => e.Id == Id).SingleOrDefault();
            model.IsDeleted = true;
            _unitOfWork.DoctorRepo.Update(model);
            var result = _unitOfWork.Commit();

            return result;
        }

        public IEnumerable<DoctorDTO> GetWhere(Expression<Func<Doctor, bool>> where)
        {
            var data = _unitOfWork.DoctorRepo.GetWhere(where, e=>e.Clinic);
            var mappedData = _mapper.Map<IEnumerable<DoctorDTO>>(data);

            return mappedData;
        }

        public IEnumerable<DoctorDTO> GetWhere(Expression<Func<Doctor, bool>> where, Expression<Func<Doctor, object>> properties)
        {
            var data = _unitOfWork.DoctorRepo.GetWhere(where , properties);
            var mappedData = _mapper.Map<IEnumerable<DoctorDTO>>(data);

            return mappedData;
        }
        public int GetWhereCount(Expression<Func<Doctor, bool>> where)
        {
            var count = _unitOfWork.DoctorRepo.GetWhere(where).Count();
            return count;
        }
    }
}
