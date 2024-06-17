namespace Domain.DTO
{
    public class DoctorWorkingDayDTO
    {
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public int? WorkingDayId { get; set; }

        // Navigation properties
        public DoctorDTO Doctor { get; set; }
        public WorkingDayDTO WorkingDay { get; set; }
    }
}
