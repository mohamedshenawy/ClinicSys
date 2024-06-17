namespace Domain.DTO
{
    public class DoctorDTO :BaseClassDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }
        public int? ClinicId { get; set; }

        // Navigation properties
        public ClinicDTO Clinic { get; set; }
        public List<DoctorWorkingDayDTO> DoctorWorkingDays { get; set; }
        public List<PatientAppointmentDTO> PatientAppointments { get; set; }
    }
}
