using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PatientAppointment : BaseClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        private DateTime _date;
        [Column(TypeName = "date")]
        public DateTime Date
        {
            get => _date.Date; 
            set => _date = value.Date; 
        }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }

        public int? PatientId { get; set; }
        public Patient Patient { get; set; }

        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int? WorkingDayId { get; set; }
        public WorkingDay WorkingDay { get; set; }

    }

}