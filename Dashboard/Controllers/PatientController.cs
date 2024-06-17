using ApplicationService.ServiceImplementation;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _PatientService;
        public PatientController(PatientService PatientService)
        {
            _PatientService = PatientService;
        }
        public IActionResult Index()
        {
            var data = _PatientService.GetWhere(e => e.IsDeleted == false);
            return View(data);
        }
        [HttpPost]
        public IActionResult Create([FromBody] PatientDTO model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                var result = _PatientService.Create(model);
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
        public IActionResult Update([FromBody] PatientDTO model)
        {
            try
            {
                var result = _PatientService.Update(model);
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
            var result = _PatientService.Delete(Id);
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
