using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Mapping.MappingHelper;

namespace Infrastructure.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Clinic, ClinicDTO>()
                .ReverseMap();
            CreateMap<Patient, PatientDTO>()
                .ReverseMap();

            CreateMap<WorkingDay, WorkingDayDTO>()
                .ReverseMap();
            CreateMap<DoctorWorkingDay, DoctorWorkingDayDTO>()
                .ReverseMap();

            CreateMap<DoctorWorkingDay, DoctorWorkingDayDTO>()
                .ReverseMap();

            CreateMap<PatientAppointment, PatientAppointmentDTO>()
                .ReverseMap();

            CreateMap<PatientAppointmentDTO, PatientAppointmentManagerDTO>()
                .ForMember(dest => dest.DoctorName, z=>z.MapFrom(x=>x.Doctor.Name))
                .ForMember(dest => dest.PatientName, z=>z.MapFrom(x=>x.Patient.Name))
                .ForMember(dest => dest.StartDate, z=>z.MapFrom(x=>x.StartDate))
                .ForMember(dest => dest.EndDate, z=>z.MapFrom(x=>x.EndDate))
                .ForMember(dest => dest.WorkingDayName, z=>z.MapFrom(x=>x.WorkingDay.Name))
                .ForMember(dest => dest.Date, z=>z.MapFrom(x=>x.Date))
                .ReverseMap();
            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dest => dest.StartDate, z=>z.MapFrom(x=>x.StartDate))
                .ForMember(dest => dest.EndDate, z=>z.MapFrom(x=>x.EndDate))
            //.ForMember(dest => dest.StartDate, opt => opt.ConvertUsing(new TimeSpanToStringConverter(), src => src.StartDate))
            //.ForMember(dest => dest.EndDate, opt => opt.ConvertUsing(new TimeSpanToStringConverter(), src => src.EndDate))
            .ReverseMap();
            //.ForMember(dest => dest.StartDate, opt => opt.ConvertUsing(new StringToTimeSpanConverter(), src => src.StartDate))
            //.ForMember(dest => dest.EndDate, opt => opt.ConvertUsing(new StringToTimeSpanConverter(), src => src.EndDate));


        }
    }
}
