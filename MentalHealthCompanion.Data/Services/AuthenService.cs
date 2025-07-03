using MentalHealthCompanion.Data.AppResponses;
using MentalHealthCompanion.Data.DatabaseEntities;
using MentalHealthCompanion.Data.DataContext;
using MentalHealthCompanion.Data.DTO;
using MentalHealthCompanion.Data.DTO.General;
using MentalHealthCompanion.Data.DTO.ResponseDto;
using MentalHealthCompanion.Data.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MentalHealthCompanion.Data.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IJwtService _jwtService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthenService> _logger;

        public AuthenService(AppDbContext appDbContext, IJwtService jwtService, UserManager<IdentityUser> userManager, ILogger<AuthenService> logger)
        {
            _appDbContext = appDbContext;
            _jwtService = jwtService;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
        {
            var existingUser = await _appDbContext
                .AppUsers
                .FirstOrDefaultAsync(u => u.EmailAddress == loginRequestDto.Email, cancellationToken);

            var userManagerResult = await _userManager.FindByIdAsync(existingUser?.Id!);
            if (existingUser is null || userManagerResult is null)
            {
                return new ApiResponse<AuthResponseDto>(ResponseCodes.UserNotFound, ResponseMessages.UserNotFound);
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(userManagerResult, loginRequestDto.Password);
            if (!passwordCheck)
            {
                return new ApiResponse<AuthResponseDto>(ResponseCodes.ValiationError, ResponseMessages.InvalidPassword);
            }

            var token = _jwtService.GenerateToken(existingUser, cancellationToken);

            return new ApiResponse<AuthResponseDto>()
            {
                Data = new AuthResponseDto
                {
                    Token = token,
                    Role = existingUser.Role
                },
                Code = ResponseCodes.Success,
                Message = ResponseMessages.Success,
                IsSuccessful = true,
                Timestamp = DateTime.UtcNow
            };


        }
    }
}
