using CompanyBooksProECom.Models;
using CompanyBooksProECom.Services;

namespace CompanyBooksProECom.Utils
{
    public static class CaptionHelper
    {
        public static Caption GetCaption(ApiClientConfig apiClientConfig)
        {
            var city = apiClientConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_ADDRESS_CITY;
            var street = apiClientConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_ADDRESS_STREET;
            var zipCode = apiClientConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_ADDRESS_ZIP;
            var address = $"{city} {street} {zipCode}";
            return new Caption
            {
                Address = address,
                CompanyName = apiClientConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_HOME_PAGE_TITLE,
                CompanyLogoUrl = apiClientConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_ICON_URL_IMAGE,
                Email = apiClientConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_EMAILS,
                Phone = apiClientConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_PHONES,
                Slogan = apiClientConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_LOGO_SLOGAN,
                Languages = apiClientConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_LANGUAGES
            };
        }
    }
}
