using MBM.Code;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyBooksProECom.Services
{
    public class ApiClientConfig
    {
        public clsApplicationConfig AppConfig { get; set; }
        public clsApplicationConfigExtended AppConfigExtended { get; set; }
    }

    public interface IAppConfigService
    {
        Task<ApiClientConfig> GetApiClientConfig(CancellationToken cancellationToken);
        int GetIdentityExpiration();
        string GetCompanyName();
        string GetCompanyUrl();
        string GetCompanyDomain();
        enumLanguagesEnum GetDefaultEnumLanguage();
    }

    public class AppConfigService : IAppConfigService
    {
        private readonly ApiParams _apiParams;
        private readonly CompanyParams _companyParams;
        private readonly IMemoryCache _cache;
        private readonly ILogger<AppConfigService> _logger;

        public AppConfigService(IOptions<ApiParams> apiParams,
                                IOptions<CompanyParams> companyParams,
                                IMemoryCache cache,
                                ILogger<AppConfigService> logger)
        {
            _apiParams = apiParams.Value;
            _companyParams = companyParams.Value;
            _cache = cache;
            _logger = logger;
        }

        public async Task<ApiClientConfig> GetApiClientConfig(CancellationToken cancellationToken)
        {
            return await Task.Run(async() => {
                var key = $"default-{nameof(ApiClientConfig)}";
                var apiConfig = _cache.Get<ApiClientConfig>(key);

                if (apiConfig == null)
                {
                    var appConfig = await GetAppConfigAsync(cancellationToken);
                    var appConfigExtended = GetAppConfigExtended(ref appConfig);

                    apiConfig = new ApiClientConfig
                    {
                        AppConfig = appConfig,
                        AppConfigExtended = appConfigExtended,
                    };

                    _cache.Set(key, apiConfig, DateTimeOffset.Now.AddDays(_apiParams.IdentityExpirationInDays));
                    _companyParams.CompanyName = apiConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_HOME_PAGE_TITLE;
                    _companyParams.CompanyUrl = apiConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_ICON_URL_IMAGE;
                    _companyParams.DefaultEnumLanguage = apiConfig.AppConfigExtended.APPLICATION_ESHOP_SETTINGS.APPLICATION_ESHOP_LANGUAGE_DEFAULT;
                }

                return apiConfig;
            }, cancellationToken);
        }

        public int GetIdentityExpiration() => _apiParams.IdentityExpirationInDays;

        public string GetCompanyName() => _companyParams.CompanyName;

        public string GetCompanyUrl() => _companyParams.CompanyUrl;

        public string GetCompanyDomain() => _companyParams.CompanyDomain;

        public enumLanguagesEnum GetDefaultEnumLanguage() => _companyParams.DefaultEnumLanguage;

        private async Task<clsApplicationConfig> GetAppConfigAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => {
                var appConfig = new clsApplicationConfig();
                var message = string.Empty;
                var messageForUser = string.Empty;
                var workByWebService = string.Empty;
                var workByWebServiceRefreshConnections = string.Empty;

                try
                {
                    clsGeneral.CompanyBooksProIni_Read_FILE_INI_PARAM(_apiParams.IsWorkByWebServices,
                                                                      ref workByWebService,
                                                                      ref message);
                    clsGeneral.CompanyBooksProIni_Read_FILE_INI_PARAM(_apiParams.IsWorkByWebServicesRefreshConnections,
                                                                      ref workByWebServiceRefreshConnections,
                                                                      ref message);

                    appConfig.APPLICATION_IS_DEBUG = clsGeneral.CnvBoolean(_apiParams.IsDebug);
                    appConfig.APPLICATION_IS_WORK_BY_WEBSERVICES = clsGeneral.CnvBoolean(workByWebService);
                    appConfig.APPLICATION_IS_WORK_BY_WEBSERVICES_REFRESH_CONNECTIONS = clsGeneral.CnvBoolean(workByWebServiceRefreshConnections);
                    appConfig.clsConnection.APPLICATION_INIT_DEFAULTS(appConfig);

                    string filePath = Path.Combine(clsGeneral.GetPath_AppDomainBaseDirectory(), "CompanyBooksPro.ini");

                    if (!appConfig.clsConnection.APPLICATION_INIT_ReadFileINI(appConfig))
                    {
                        var notFoundMessage = "Fail Read *.INI file.";
                        _logger.LogError(notFoundMessage);
                        throw new FileNotFoundException(notFoundMessage, "CompanyBooksPro.ini");
                    }

                    appConfig.clsDBLayer.APPLICATION_APPCONFIG_SET_INIT_DEMO_COMPANY(appConfig, 
                                                                                     ref messageForUser);

                    var isSelected = appConfig.clsDBLayer.SELECT_COMPANY_ID_BY_DOMAIN_NAME(appConfig,
                                                                                           _companyParams.CompanyDomain,
                                                                                           out var appType,
                                                                                           out var companyId,
                                                                                           out _);

                    if (!(isSelected &&
                        companyId > 0 &&
                        appType.Equals(enumApplicationsTypes.Application_eShop)))
                    {
                        var notFoundMessage = $"Fail company search with domain name: {_companyParams.CompanyDomain}.";
                        _logger.LogError(notFoundMessage);
                        throw new DataException(notFoundMessage);
                    }

                    _companyParams.CompanyId = companyId;
                    return appConfig;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fail get appConfig.");
                    appConfig.clsConnection.LogException(appConfig, ex);
                    throw;
                }
            }, cancellationToken);
        }

        private clsApplicationConfigExtended GetAppConfigExtended(ref clsApplicationConfig appConfig)
        {
            try
            {
                appConfig = appConfig.clsDBLayer.APPLICATION_INIT_BY_COMPANY_ID(appConfig, _companyParams.CompanyId);

                if (appConfig.APPLICATION_COMPANY_ID != _companyParams.CompanyId)
                {
                    var notFoundMessage = $"Fail company search with id: {_companyParams.CompanyId}.";
                    _logger.LogError(notFoundMessage);
                    throw new DataException(notFoundMessage);
                }

               

                return appConfig.clsDBLayer.ESHOP_APPLICATION_CONFIG_EXTENDED_INIT(appConfig, _companyParams.CompanyDomain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail get appConfigExtended.");
                appConfig.clsConnection.LogException(appConfig, ex);
                throw;
            }
        }
    }
}
