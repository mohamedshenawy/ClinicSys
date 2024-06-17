using Domain.Entities;
using DomainService.Repo;
using DomainService.UnitOfWork;
using Infrastructure.Context;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        IRepo<Doctor> _doctorRepo;
        IRepo<Clinic> _clinicRepo;
        IRepo<DoctorWorkingDay> _doctorWorkingDayRepo;
        IRepo<WorkingDay> _workingDayRepo;
        IRepo<Patient> _patientRepo;
        IRepo<PatientAppointment> _patientAppointmentRepo;
        protected Context.Context _context { get; }


        public UnitOfWork(Context.Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IRepo<Doctor> DoctorRepo
        {
            get
            {
                if (_doctorRepo == null)
                {
                    _doctorRepo = new RepoImplementation<Doctor>(_context);
                }
                return _doctorRepo;
            }
        }

        public IRepo<Clinic> ClinicRepo
        {
            get
            {
                if (_clinicRepo == null)
                {
                    _clinicRepo = new RepoImplementation<Clinic>(_context);
                }
                return _clinicRepo;
            }
        }

        public IRepo<DoctorWorkingDay> DoctorWorkingDayRepo
        {
            get
            {
                if (_doctorWorkingDayRepo == null)
                {
                    _doctorWorkingDayRepo = new RepoImplementation<DoctorWorkingDay>(_context);
                }
                return _doctorWorkingDayRepo;
            }
        }

        public IRepo<WorkingDay> WorkingDayRepo
        {
            get
            {
                if (_workingDayRepo == null)
                {
                    _workingDayRepo = new RepoImplementation<WorkingDay>(_context);
                }
                return _workingDayRepo;
            }
        }

        public IRepo<Patient> PatientRepo
        {
            get
            {
                if (_patientRepo == null)
                {
                    _patientRepo = new RepoImplementation<Patient>(_context);
                }
                return _patientRepo;
            }
        }

        public IRepo<PatientAppointment> PatientAppointmentRepo
        {
            get
            {
                if (_patientAppointmentRepo == null)
                {
                    _patientAppointmentRepo = new RepoImplementation<PatientAppointment>(_context);
                }
                return _patientAppointmentRepo;
            }
        }

        public int Commit()
        {
            int result = _context.SaveChanges();
            return result;
        }
    }
}
