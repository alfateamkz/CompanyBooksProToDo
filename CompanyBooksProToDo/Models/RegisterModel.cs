namespace CompanyBooksProECom.Models
{
    public class RegisterModel
    {
        public enumClientsSubTypesEnum BusinessType { get; set; }
        public string BusinessTypeLabel { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string CompanyName { get; set; }
        public string IdentityNumber { get; set; }
        public string Post { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string AdditionalPhone { get; set; }
        public string Site { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
