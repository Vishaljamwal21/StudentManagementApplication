using StudentManagementSystum.Models;

namespace StudentManagementSystum.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string email);
        User Authenticate(string email, string password);
        User Register(string email, string password);
    }
}
