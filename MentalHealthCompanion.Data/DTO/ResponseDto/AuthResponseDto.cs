namespace MentalHealthCompanion.Data.DTO.ResponseDto
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public AuthResponseDto()
        {

        }

        public AuthResponseDto(string token, string role)
        {
            Token = token;
            Role = role;
        }
    }
}
