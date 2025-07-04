using MentalHealthCompanion.Data.DTO;
using MentalHealthCompanion.Data.DTO.General;
using MentalHealthCompanion.Data.DTO.ResponseDto;

namespace MentalHealthCompanion.Data.Interface
{
    public interface IAuthenService
    {
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellationToken);
    }
}
