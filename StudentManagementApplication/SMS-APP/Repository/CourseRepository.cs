using SMS_APP.Models;
using SMS_APP.Repository.IRepository;

namespace SMS_APP.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CourseRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
