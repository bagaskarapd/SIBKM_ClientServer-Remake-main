using API.Context;
using API.Handlers;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;

namespace API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
    {
        public AccountRepository(MyContext context) : base(context) { }
        public int Register(RegisterVM registerVM)
        {
            int result = 0;

            //insert to universitie table
            var universitie = new Universitie
            {
                Name = registerVM.UniversitieName,
            };

            if (_context.Universities.Any(u => u.Name.Contains(registerVM.UniversitieName)))
            {
                universitie.Id = _context.Universities.FirstOrDefault(u => u.Name.Contains(registerVM.UniversitieName))!.Id;
            }
            else
            {
                _context.Set<Universitie>().Add(universitie);
                result = _context.SaveChanges();
            }

            //insert to education table
            var education = new Education
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.Gpa,
                UniversityId = universitie.Id
            };
            _context.Set<Education>().Add(education);
            result += _context.SaveChanges();

            //insert to employee table
            var employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Gender = registerVM.Gender,
                BirthDate = registerVM.BirthDate,
                Email = registerVM.Email,
                HiringDate = DateTime.Now,
                PhoneNumber = registerVM.PhoneNumber
            };
            _context.Set<Employee>().Add(employee);
            result += _context.SaveChanges();

            //insert to Account table
            var account = new Account
            {
                EmployeeNIK = registerVM.NIK,
                password = Hashing.HashPassword(registerVM.password)
            };
            _context.Set<Account>().Add(account);
            result += _context.SaveChanges();

            //insert to Profiling table
            var profiling = new Profiling
            {
                EmployeeNIK = registerVM.NIK,
                EducationId = education.Id,
            };
            _context.Set<Profiling>().Add(profiling);
            result += _context.SaveChanges();

            //insert to AccountRole table
            var accountRole = new AccountRole
            {
                AccountNik = registerVM.NIK,
                RoleId = 1
            };
            _context.Set<AccountRole>().Add(accountRole);
            result += _context.SaveChanges();

            return result;
        }
        public bool Login(LoginVM loginVM)
        {
            //Ambil data dari database berdasarkan Email di tabel employee
            var getEmployeeAccount = _context.Employees.Join(_context.Accounts,
                                                     e => e.NIK,
                                                     a => a.EmployeeNIK,
                                                     (e, a) => new
                                                     {
                                                         Email = e.Email,
                                                         Password = a.password
                                                     }).FirstOrDefault(e =>
                                                                           e.Email == loginVM.Email);

            if (getEmployeeAccount == null)
            {
                return false;
            }

            return Hashing.ValidatePassword(loginVM.Password, getEmployeeAccount.Password);
        }
    }
}
