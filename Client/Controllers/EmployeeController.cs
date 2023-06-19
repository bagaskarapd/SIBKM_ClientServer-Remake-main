using API.Models;
using Client.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository repository;

        public EmployeeController(EmployeeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var Results = await repository.Get();
            var employees = new List<Employee>();

            if (Results != null)
            {
                employees = Results.Data.ToList();
            }

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            var result = await repository.Post(employee);
            if (result.Code == 200)
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(string NIK)
        {
            var Results = await repository.Get(NIK);
            var employee = Results.Data;
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string NIK)
        {
            var Results = await repository.Get(NIK);
            var employee = new Employee();

            if (Results.Data?.NIK is null)
            {
                return View(employee);
            }
            else
            {
                employee.NIK = Results.Data.NIK;
                employee.FirstName = Results.Data.FirstName;
                employee.LastName = Results.Data.LastName;
                employee.BirthDate = Results.Data.BirthDate;
                employee.Gender = Results.Data.Gender;
                employee.HiringDate = Results.Data.HiringDate;
                employee.Email = Results.Data.Email;
                employee.PhoneNumber = Results.Data.PhoneNumber;
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Put(employee.NIK, employee);
                if (result.Code == 200)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.Code == 409)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string NIK)
        {
            var result = await repository.Get(NIK);
            var employee = result?.Data;

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(string NIK)
        {
            var result = await repository.Delete(NIK);
            if (result.Code == 200)
            {
                TempData["Success"] = "Data berhasil dihapus";
                return RedirectToAction(nameof(Index));
            }

            var employee = await repository.Get(NIK);
            return View("Delete", employee?.Data);
        }
    }
}