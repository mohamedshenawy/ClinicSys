using Domain.Entities;

namespace Domain.DTO
{
    public class PatientAppointmentDTO :BaseClassDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }

        // Navigation properties
        public PatientDTO Patient { get; set; }
        public DoctorDTO Doctor { get; set; }
        public int? WorkingDayId { get; set; }
        public WorkingDayDTO WorkingDay { get; set; }
    }
}
