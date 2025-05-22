using Pharmacy.Core.Models;

namespace Pharmacy.Application.Services
{
    public interface IJWTService
    {
        string Generate(User user);
    }
}