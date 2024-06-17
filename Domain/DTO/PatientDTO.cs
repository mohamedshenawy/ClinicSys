namespace Domain.DTO
{
    public class PatientDTO :BaseClassDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        
        // Navigation property
        public List<PatientAppointmentDTO> PatientAppointments { get; set; }
    }
}
