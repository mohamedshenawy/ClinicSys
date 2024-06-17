namespace Domain.DTO
{
    public class WorkingDayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public List<DoctorWorkingDayDTO> DoctorWorkingDays { get; set; }
    }
}
