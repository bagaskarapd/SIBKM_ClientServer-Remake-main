using API.Context;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Data
{
    public class UniversitieRepository : GeneralRepository<Universitie, int, MyContext>, IUniversitieRepository
    {
        public UniversitieRepository(MyContext context) : base(context) { }
        public IEnumerable<Universitie> GetByName(string name)
        {
            return _context.Universities.Where(x => x.Name.Contains(name));
        }
    }
}
