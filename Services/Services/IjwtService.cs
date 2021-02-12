using DataAccess.DataAccess;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(ApplicationUser user);
    }
}
