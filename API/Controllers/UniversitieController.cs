using API.Base;
using API.Models;
using API.Repositories;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UniversitieController : GeneralController<IUniversitieRepository, Universitie, int>
{
    public UniversitieController(IUniversitieRepository repository) : base(repository) { }
}



