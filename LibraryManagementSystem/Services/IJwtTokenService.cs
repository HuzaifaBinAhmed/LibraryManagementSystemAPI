using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
