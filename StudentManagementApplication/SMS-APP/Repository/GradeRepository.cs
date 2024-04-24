using SMS_APP.Models;
using SMS_APP.Repository.IRepository;

namespace SMS_APP.Repository
{
    public class GradeRepository:Repository<Grade>,IGradeRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GradeRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
