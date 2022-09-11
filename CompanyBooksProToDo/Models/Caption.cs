using System.Collections.Generic;

namespace CompanyBooksProECom.Models
{
    public class Caption
    {
        public string Phone { get; set; }
        public string CompanyLogoUrl { get; set; }
        public string CompanyName { get; set; }
        public string Slogan { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<enumLanguagesEnum> Languages { get; set; }
    }
}
