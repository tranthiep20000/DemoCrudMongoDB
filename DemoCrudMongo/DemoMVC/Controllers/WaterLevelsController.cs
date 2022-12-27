using DemoMVC.Models;
using DemoMVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    /// <summary>
    /// Information of WaterLevelsController
    /// CreatedBy: ThiepTT(30/10/2022)
    /// </summary>
    public class WaterLevelsController : Controller
    {
        private readonly IWaterLevelRepository _waterLevelRepository;

        public WaterLevelsController(IWaterLevelRepository waterLevelRepository)
        {
            _waterLevelRepository = waterLevelRepository;
        }

        public IActionResult Index(string? valueSearch)
        {
            IEnumerable<WaterLevel> waterLevels;

            if (string.IsNullOrEmpty(valueSearch))
            {
                waterLevels = _waterLevelRepository.GetAll();
            }
            else
            {
                waterLevels = _waterLevelRepository.GetBySearchValue(valueSearch);
            }

            return View(waterLevels);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WaterLevel waterLevel)
        {
            if (ModelState.IsValid)
            {
                waterLevel.Id = Guid.NewGuid();
                _waterLevelRepository.Create(waterLevel);
                TempData["success"] = "user created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterLevel = _waterLevelRepository.GetById((Guid)id);

            if (waterLevel == null)
            {
                return NotFound();
            }

            return View(waterLevel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, WaterLevel waterLevel)
        {
            if (ModelState.IsValid)
            {
                _waterLevelRepository.Update(id, waterLevel);
                TempData["success"] = "user updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterLevel = _waterLevelRepository.GetById((Guid)id);

            if (waterLevel == null)
            {
                return NotFound();
            }

            return View(waterLevel);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(Guid id)
        {
            var waterLevel = _waterLevelRepository.GetById((Guid)id);

            if (waterLevel == null)
            {
                return NotFound();
            }

            _waterLevelRepository.Delete((Guid)id);
            TempData["success"] = "user deleted successfully";
            return RedirectToAction("Index");
        }
    }
}