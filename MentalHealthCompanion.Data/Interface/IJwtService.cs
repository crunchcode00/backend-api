using MentalHealthCompanion.Data.DatabaseEntities;

namespace MentalHealthCompanion.Data.Interface
{
    public interface IJwtService
    {
        string GenerateToken(AppUser appUser, CancellationToken cancellationToken);
    }
}
