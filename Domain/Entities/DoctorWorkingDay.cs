using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DoctorWorkingDay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int? WorkingDayId { get; set; }
        public WorkingDay WorkingDay { get; set; }

    }
}