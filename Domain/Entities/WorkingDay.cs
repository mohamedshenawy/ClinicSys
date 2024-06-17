using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class WorkingDay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<DoctorWorkingDay> DoctorWorkingDays { get; set; }
        public List<WorkingDay> WorkingDays { get; set; }

    }
}