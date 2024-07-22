namespace DataProtectionProvider.Model
{
    public class PersonalData
    {
        public int Id { get; set; }
        public string FirtsName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}