using SMS_APP.Models;
using SMS_APP.Repository.IRepository;

namespace SMS_APP.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
