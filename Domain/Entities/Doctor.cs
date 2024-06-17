using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Doctor : BaseClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }

        public int? ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        public List<DoctorWorkingDay> DoctorWorkingDays { get; set; }
        public List<PatientAppointment> PatientAppointments { get; set; }
    }

}