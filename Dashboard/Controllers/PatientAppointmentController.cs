using ApplicationService.ServiceImplementation;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dashboard.Controllers
{
    public class PatientAppointmentController : Controller
    {
        private readonly ClinicService _clinicService;
        private readonly DoctorService _doctorService;
        private readonly PatientAppointmentService _patientAppointmentService;
        private readonly PatientService _patientService;
        private readonly DoctorWorkingDayService _doctorWorkingDayService;
        private readonly IMapper _mapper;
        public PatientAppointmentController(ClinicService clinicService, PatientAppointmentService patientAppointmentService , DoctorService doctorService , PatientService patientService,IMapper mapper, DoctorWorkingDayService doctorWorkingDayService)
        {
            _clinicService = clinicService;
            _patientAppointmentService = patientAppointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _mapper = mapper;
            _doctorWorkingDayService = doctorWorkingDayService;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add(int id = 0)
        {
            // create view model 
            PatientAppointmentViewModel model = new PatientAppointmentViewModel();
            model.Doctors = _doctorService.GetWhere(e => e.IsDeleted == false).ToList();
            model.Patients = _patientService.GetWhere(e => e.IsDeleted == false).ToList();
            if (id > 0)
            {
                model.PatientAppointment = _patientAppointmentService.GetWhere(e => e.IsDeleted == false && e.Id == id, 1, 0).FirstOrDefault();
                model.PatientAppointment.Doctor.DoctorWorkingDays = _doctorWorkingDayService.GetByDoctorId((int)model.PatientAppointment.DoctorId).ToList();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Create([FromBody] PatientAppointmentDTO model)
        {
            try
            {
                List<string> validationMsgs = new List<string>();
                //validation 
                var DayOfweekId = (int) model.Date.DayOfWeek;
                if(DayOfweekId != model.WorkingDayId - 1)
                {
                    validationMsgs.Add("Invalid Date , Please select day of doctor working days");
                }

                DoctorDTO doctorInfo = _doctorService.GetWhere(e => e.IsDeleted == false 
                && e.Id == model.DoctorId, 
                x=>x.PatientAppointments.Where(pa =>
                pa.IsDeleted == false
                && pa.StartDate < model.EndDate
                && pa.EndDate > model.StartDate
                && pa.Date == model.Date
                && pa.WorkingDayId == model.WorkingDayId
                ).OrderByDescending(pa => pa.StartDate)
                ).FirstOrDefault();

                if(doctorInfo.StartDate > model.StartDate || doctorInfo.EndDate < model.StartDate)
                {
                    validationMsgs.Add($"The time of Appointment is not in doctor working hours. doctor working Hours from : {doctorInfo.StartDate} to {doctorInfo.EndDate}");
                }
                if(doctorInfo.PatientAppointments.Count > 0)
                {
                    validationMsgs.Add($"The time of Appointment is already taken for {doctorInfo.PatientAppointments.Count.ToString()} appointments . starts from :{doctorInfo.PatientAppointments.FirstOrDefault().StartDate} to {doctorInfo.PatientAppointments.LastOrDefault().EndDate} . please select another time.");
                }

                if(validationMsgs.Count > 0)
                {
                    return Json(new { status = 2, message = validationMsgs });
                }
                
                model.CreationDate = DateTime.Now;
                var result = _patientAppointmentService.Create(model);
                if (result > 0)
                {
                    return Json(new { status = 1, message = "Item saved successfully" });
                }
                else
                {
                    return Json(new { status = 0, message = "Error! Please contact system administrator." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] PatientAppointmentDTO model)
        {
            try
            {
                List<string> validationMsgs = new List<string>();
                //validation 
                var DayOfweekId = (int)model.Date.DayOfWeek;
                if (DayOfweekId != model.WorkingDayId - 1)
                {
                    validationMsgs.Add("Invalid Date , Please select day of doctor working days");
                }

                DoctorDTO doctorInfo = _doctorService.GetWhere(e => e.IsDeleted == false
                && e.Id == model.DoctorId,
                x => x.PatientAppointments.Where(pa =>
                pa.IsDeleted == false
                && pa.StartDate < model.EndDate
                && pa.EndDate > model.StartDate
                && pa.Date == model.Date
                && pa.WorkingDayId == model.WorkingDayId
                && pa.Id != model.Id
                ).OrderByDescending(pa => pa.StartDate)
                ).FirstOrDefault();

                if (doctorInfo.StartDate > model.StartDate || doctorInfo.EndDate < model.StartDate)
                {
                    validationMsgs.Add($"The time of Appointment is not in doctor working hours. doctor working Hours from : {doctorInfo.StartDate} to {doctorInfo.EndDate}");
                }
                if (doctorInfo.PatientAppointments.Count > 0)
                {
                    validationMsgs.Add($"The time of Appointment is already taken for {doctorInfo.PatientAppointments.Count.ToString()} appointments . starts from :{doctorInfo.PatientAppointments.FirstOrDefault().StartDate} to {doctorInfo.PatientAppointments.LastOrDefault().EndDate} . please select another time.");
                }

                if (validationMsgs.Count > 0)
                {
                    return Json(new { status = 2, message = validationMsgs });
                }

                var result = _patientAppointmentService.Update(model);
                if (result > 0)
                {
                    return Json(new { status = 1, message = "Item saved successfully" });
                }
                else
                {
                    return Json(new { status = 0, message = "Error! Please contact system administrator." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var result = _patientAppointmentService.Delete(Id);
            if (result > 0)
            {
                return Json(new { status = 1, message = "Deleted" });
            }
            else
            {
                return Json(new { status = 0, message = "Error! Please contact system administrator." });
            }
        }


        [HttpPost]
        public IActionResult GetDataManager([FromBody] DTPostObj dTPostObj)
        {
            try
            {
                TabulatorDTO tabulatorDTO = new TabulatorDTO();
                var criteriaDTO = dTPostObj?.criteria != null ? JsonConvert.DeserializeObject<PatientAppointmentManagerSearchDTO>(dTPostObj?.criteria) : new PatientAppointmentManagerSearchDTO();
                criteriaDTO.Skip = dTPostObj.start ; //*dTPostObj.length skip
                criteriaDTO.Take = dTPostObj.length; // take

                Expression<Func<PatientAppointment, bool>> where = e =>
                e.IsDeleted == false
                && (e.Patient.Name.Contains(criteriaDTO.PatientName) || criteriaDTO.PatientName == "" || criteriaDTO.PatientName == null)
                && (e.Doctor.Name.Contains(criteriaDTO.DoctorName) || criteriaDTO.DoctorName == "" || criteriaDTO.DoctorName == null)
                && (e.Date >= criteriaDTO.StartDate || criteriaDTO.StartDate == null)
                && (e.Date <= criteriaDTO.EndDate || criteriaDTO.EndDate == null);



                var totalRecords = _patientAppointmentService.GetWhereCount(where);

                var data = _patientAppointmentService.GetWhere(where, criteriaDTO.Take, criteriaDTO.Skip);
                var mappedData = _mapper.Map<List<PatientAppointmentManagerDTO>>(data);
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / dTPostObj.length);

                tabulatorDTO.Data = mappedData;
                tabulatorDTO.Last_page = totalPages;
                return Ok(new
                {
                    draw = dTPostObj.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    tableData = tabulatorDTO
                });

            }catch (Exception ex)
            {
                throw;
            }
            

        }

        [HttpGet]
        public IActionResult GetDoctorWarkingDays(int doctorId)
        {
            var data = _doctorWorkingDayService.GetByDoctorId(doctorId);
            var doctor = _doctorService.GetWhere(e => e.IsDeleted == false && e.Id == doctorId).FirstOrDefault();
            ViewBag.Doctor = doctor;
            return PartialView("~/Views/PatientAppointment/Partials/_DoctorWorkingDaysDropdown.cshtml", data);
        }
    }
}
