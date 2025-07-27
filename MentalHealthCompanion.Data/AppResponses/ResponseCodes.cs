namespace MentalHealthCompanion.Data.AppResponses
{
    public static class ResponseCodes
    {
        public const string Success = "00";
        public const string EmailAlreadyExist = "01";
        public const string UserRegistrationFailed = "02";
        public const string InvalidRegistrationCode = "03";
        public const string UserNotFound = "04";
        public const string ProfileUpdateFailed = "05";
        public const string TokenError = "06";
        public const string ValiationError = "57";
        public const string Exception = "99";
    }
}
