using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class PatientAppointmentViewModel
    {
        public PatientAppointmentDTO PatientAppointment { get; set; }
        public List<DoctorDTO> Doctors { get; set; }
        public List<PatientDTO> Patients { get; set; }
    }
}
