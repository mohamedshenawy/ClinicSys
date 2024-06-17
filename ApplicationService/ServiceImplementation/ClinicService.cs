using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using DomainService.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServiceImplementation
{
    public class ClinicService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClinicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public int Create(ClinicDTO entity)
        {
            var model = _mapper.Map<Clinic>(entity);
            _unitOfWork.ClinicRepo.Create(model);
            var result = _unitOfWork.Commit();

            return model.Id;
        }

        public int Update(ClinicDTO entity)
        {
            var entityModel = _unitOfWork.ClinicRepo.GetWhere(e => e.Id == entity.Id).FirstOrDefault();
            if(entityModel == null)
            {
                return 0;
            }
            entityModel.Name = entity.Name;
            entityModel.UpdatedDate = DateTime.Now;

            _unitOfWork.ClinicRepo.Update(entityModel);
            var result = _unitOfWork.Commit();
            return result;
        }

        public int Delete(int Id)
        {
            var model = _unitOfWork.ClinicRepo.GetWhere(e => e.Id == Id).SingleOrDefault();
            model.IsDeleted = true;
            _unitOfWork.ClinicRepo.Update(model);
            var result = _unitOfWork.Commit();

            return result;
        }

        public IEnumerable<ClinicDTO> GetWhere(Expression<Func<Clinic, bool>> where)
        {
            var data = _unitOfWork.ClinicRepo.GetWhere(where);
            var mappedData = _mapper.Map<IEnumerable<ClinicDTO>>(data);

            return mappedData;
        }

        public IEnumerable<ClinicDTO> GetAll()
        {
            var data = _unitOfWork.ClinicRepo.GetAll();
            var mappedData = _mapper.Map<IEnumerable<ClinicDTO>>(data);
            return mappedData;
        }
    }
}
