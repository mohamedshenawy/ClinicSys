using ApplicationService.ServiceImplementation;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorService _DoctorService;
        private readonly WorkingDayService _workingDayService;
        private readonly ClinicService _clinicService;
        public DoctorController(DoctorService DoctorService , WorkingDayService workingDayService, ClinicService clinicService)
        {
            _DoctorService = DoctorService;
            _workingDayService = workingDayService;
            _clinicService = clinicService;
        }
        public IActionResult Index()
        {
            var data = _DoctorService.GetWhere(e => e.IsDeleted == false);
            //get working days
            var workingDays = _workingDayService.GetAll();
            var clinics = _clinicService.GetAll();
            ViewBag.WorkingDays = workingDays;
            ViewBag.Clinics = clinics;
            return View(data);
        }
        [HttpPost]
        public IActionResult Create([FromBody] DoctorDTO model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                var result = _DoctorService.Create(model);
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
        public IActionResult Update([FromBody] DoctorDTO model)
        {
            try
            {
                var result = _DoctorService.Update(model);
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
            var result = _DoctorService.Delete(Id);
            if (result > 0)
            {
                return Json(new { status = 1, message = "Deleted" });
            }
            else
            {
                return Json(new { status = 0, message = "Error! Please contact system administrator." });
            }
        }
    }
}
