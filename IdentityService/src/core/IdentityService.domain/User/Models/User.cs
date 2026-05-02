using IdentityService.domain.Common.Models;

namespace IdentityService.domain.User.Models
{
    public class User : IDbEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }    = DateTime.UtcNow;
    }
}
