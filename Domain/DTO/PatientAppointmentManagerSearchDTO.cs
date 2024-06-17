namespace Domain.DTO
{
    public class PatientAppointmentManagerSearchDTO
    {
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
