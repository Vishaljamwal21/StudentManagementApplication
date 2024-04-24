using SMS_APP.Models;
using SMS_APP.Repository.IRepository;

namespace SMS_APP.Repository
{
    public class EnrollmentRepository:Repository<Enrollment>,IEnrollmentRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EnrollmentRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
