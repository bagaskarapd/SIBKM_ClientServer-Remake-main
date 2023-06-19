using API.Models;

namespace API.Repositories.Interface
{
    public interface IUniversitieRepository : IGeneralRepository<Universitie, int>
    {
        IEnumerable<Universitie> GetByName(string name);
    }
}
