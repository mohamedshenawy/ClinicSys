namespace Domain.DTO
{
    public class PatientAppointmentManagerDTO : BaseClassDTO
    {
        public int Id { get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string WorkingDayName { get; set; }
        public DateTime Date { get; set; }
    }
}
