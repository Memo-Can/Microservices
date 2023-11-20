using Microsoft.AspNetCore.Http;

namespace Course.Shared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        public IHttpContextAccessor _contextAccessor;

        public SharedIdentityService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId => _contextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}
