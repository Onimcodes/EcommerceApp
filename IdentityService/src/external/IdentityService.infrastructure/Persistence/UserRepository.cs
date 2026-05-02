using IdentityService.application.Interfaces.Persistence;
using IdentityService.domain.User.Models;
using IdentityService.infrastructure.Common.DataContext;

namespace IdentityService.infrastructure.Persistence
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
