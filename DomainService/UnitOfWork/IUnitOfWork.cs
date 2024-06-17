using Domain.Entities;
using DomainService.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.UnitOfWork
{
    public interface IUnitOfWork 
    {
        public IRepo<Doctor> DoctorRepo { get;}
        public IRepo<Clinic> ClinicRepo { get; }
        public IRepo<DoctorWorkingDay> DoctorWorkingDayRepo { get; }
        public IRepo<WorkingDay> WorkingDayRepo { get; }
        public IRepo<Patient> PatientRepo { get; }
        public IRepo<PatientAppointment> PatientAppointmentRepo { get; }
        public int Commit();

    }
}
