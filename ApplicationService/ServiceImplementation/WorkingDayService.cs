using AutoMapper;
using Domain.DTO;
using DomainService.UnitOfWork;

namespace ApplicationService.ServiceImplementation
{
    public class WorkingDayService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WorkingDayService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<WorkingDayDTO> GetAll()
        {
            var data = _unitOfWork.WorkingDayRepo.GetAll();
            var mappedData = _mapper.Map<IEnumerable<WorkingDayDTO>>(data);
            return mappedData;
        }

    }

    public class DoctorWorkingDayService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DoctorWorkingDayService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DoctorWorkingDayDTO> GetByDoctorId(int doctorId)
        {
            var data = _unitOfWork.DoctorWorkingDayRepo.GetWhere(e=>e.DoctorId == doctorId , x=>x.WorkingDay).ToList();
            var mappedData = _mapper.Map<IEnumerable<DoctorWorkingDayDTO>>(data);
            return mappedData;
        }

    }
}
