using SMS_APP.Models;
using SMS_APP.Repository.IRepository;

namespace SMS_APP.Repository
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
