using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using DomainService.UnitOfWork;
using System.Linq.Expressions;

namespace ApplicationService.ServiceImplementation
{
    public class PatientAppointmentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PatientAppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public int Create(PatientAppointmentDTO entity)
        {
            var model = _mapper.Map<PatientAppointment>(entity);
            _unitOfWork.PatientAppointmentRepo.Create(model);
            var result = _unitOfWork.Commit();

            return model.Id;
        }

        public int Update(PatientAppointmentDTO entity)
        {
            var entityModel = _unitOfWork.PatientAppointmentRepo.GetWhere(e => e.Id == entity.Id).FirstOrDefault();
            if (entityModel == null)
            {
                return 0;
            }
            entityModel.UpdatedDate = DateTime.Now;
            entityModel.DoctorId = entity.DoctorId;
            entityModel.PatientId = entity.PatientId;
            entityModel.StartDate = entity.StartDate;
            entityModel.EndDate = entity.EndDate;
            entityModel.Date = entity.Date;

            _unitOfWork.PatientAppointmentRepo.Update(entityModel);
            var result = _unitOfWork.Commit();
            return result;
        }

        public int Delete(int Id)
        {
            var model = _unitOfWork.PatientAppointmentRepo.GetWhere(e => e.Id == Id).SingleOrDefault();
            model.IsDeleted = true;
            _unitOfWork.PatientAppointmentRepo.Update(model);
            var result = _unitOfWork.Commit();

            return result;
        }

        public IEnumerable<PatientAppointmentDTO> GetWhere(Expression<Func<PatientAppointment, bool>> where, int take = 20 , int skip = 0)
        {
            var data = _unitOfWork.PatientAppointmentRepo.GetWhere(where
                ,e=>e.Patient , e => e.Doctor , e => e.Doctor.DoctorWorkingDays , e => e.Doctor.DoctorWorkingDays, e=>e.WorkingDay).Skip(skip).Take(take);
            var mappedData = _mapper.Map<IEnumerable<PatientAppointmentDTO>>(data);

            return mappedData;
        }
        public int GetWhereCount(Expression<Func<PatientAppointment, bool>> where, int take = 0, int skip = 0)
        {
            int data = 0;
            if(take ==0 && skip == 0)
            {
                data = _unitOfWork.PatientAppointmentRepo.GetWhere(where).Count();
            }
            else
            {
                data = _unitOfWork.PatientAppointmentRepo.GetWhere(where).Skip(skip).Take(take).Count();
            }
            return data;
        }

        public IEnumerable<PatientAppointmentDTO> GetAll()
        {
            var data = _unitOfWork.PatientAppointmentRepo.GetAll();
            var mappedData = _mapper.Map<IEnumerable<PatientAppointmentDTO>>(data);
            return mappedData;
        }
    }
}
