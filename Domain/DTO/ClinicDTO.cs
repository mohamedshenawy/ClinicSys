using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ClinicDTO : BaseClassDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        // Navigation property
        public List<DoctorDTO> Doctors { get; set; }
    }
}
