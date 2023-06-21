using API.Models;
using Client.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[Authorize]
public class EmployeeController : Controller
{
    private readonly EmployeeRepository repository;

    public EmployeeController(EmployeeRepository employee)
    {
        this.repository = employee;
    }

    public async Task<IActionResult> Index()
    {
        var results = await repository.Get();
        var employees = new List<Employee>();

        if (results != null)
        {
            employees = results.Data.ToList();
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
            TempData["Success"] = "Insert Data Success";
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
    public async Task<IActionResult> Details(string id)
    {
        var results = await repository.Get(id);
        var employees = new Employee();

        if (results != null)
        {
            employees = results.Data;
        }

        return View(employees);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var results = await repository.Get(id);
        var employees = new Employee();

        if (results.Data?.NIK is null)
        {
            return View(employees);
        }
        else
        {
            employees.NIK = results.Data.NIK;
            employees.FirstName = results.Data.FirstName;
            employees.LastName = results.Data.LastName;
            employees.BirthDate = results.Data.BirthDate;
            employees.Gender = results.Data.Gender;
            employees.HiringDate = results.Data.HiringDate;
            employees.Email = results.Data.Email;
            employees.PhoneNumber = results.Data.PhoneNumber;
        }
        return View(employees);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Employee employee)
    {
        var result = await repository.Put(employee.NIK, employee);
        if (result.Code == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.Code == 500)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await repository.Get(id);
        var employee = result?.Data;

        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(string id)
    {
        var result = await repository.Delete(id);
        if (result.Code == 200)
        {
            TempData["Success"] = "Delete Data Success";
            return RedirectToAction(nameof(Index));
        }

        var employee = await repository.Get(id);
        return View("Delete", employee?.Data);
    }
}