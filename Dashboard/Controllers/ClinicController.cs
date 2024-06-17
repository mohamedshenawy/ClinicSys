using ApplicationService.ServiceImplementation;
using Dashboard.Models;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.AccessControl;

namespace Dashboard.Controllers
{
    public class ClinicController : Controller
    {
        private readonly ClinicService _clinicService;
        public ClinicController(ClinicService clinicService)
        {
            _clinicService = clinicService;
        }
        public IActionResult Index()
        {
            var data = _clinicService.GetWhere(e => e.IsDeleted == false);
            return View(data);
        }
        [HttpPost]
        public IActionResult Create([FromBody] ClinicDTO model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                var result = _clinicService.Create(model);
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
        public IActionResult Update([FromBody] ClinicDTO model)
        {
            try
            {
                var result = _clinicService.Update(model);
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
            var result = _clinicService.Delete(Id);
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
