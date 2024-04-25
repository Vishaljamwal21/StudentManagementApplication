using SMS_APP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMS_APP.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(string url, int id);
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<bool> CreateAsync(string url, T ObjToCreate);
        Task<bool> UpdateAsync(string url, T ObjToUpdate);
        Task<bool> DeleteAsync(string url, int id);
        Task<bool> IsUniqueUser(string Email);
        Task<User> Authenticate(string Email, string Password);
        Task<User> Register(string Email, string Password);
        Task<IEnumerable<Student>> GetStudentByEmailAsync(string email, string url);
        Task<bool> IsUniqueEmail(string email);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentId(int studentId);
    }
}
