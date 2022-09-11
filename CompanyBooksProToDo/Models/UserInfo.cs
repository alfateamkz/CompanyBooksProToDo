using System;

namespace CompanyBooksProECom.Models
{
    public class UserInfo
    {
        public Guid UserId { get; set; }
        public long InternalId { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAuntificate { get; set; }
        public enumLanguagesEnum CurrentLanguage { get; set; }
    }
}
