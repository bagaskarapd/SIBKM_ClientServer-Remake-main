using API.Models;
using API.ViewModels;

namespace API.Repositories.Interface
{
    public interface IEmployeeRepository : IGeneralRepository<Employee, string>
    {
        string GetFullNameByEmail(string email);
    }
}
