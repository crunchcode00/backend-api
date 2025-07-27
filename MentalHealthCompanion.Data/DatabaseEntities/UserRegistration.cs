namespace MentalHealthCompanion.Data.DatabaseEntities
{
    public class UserRegistration : BaseEntity
    {
        public DateTime ExpiryDate { get; set; }
        public string UserId { get; set; }
        public string RegistrationCode { get; set; }
        public string RegistrationLink { get; set; }
    }
}
