using System.Threading;
using System.Threading.Tasks;
using CompanyBooksProECom.Services;
using CompanyBooksProToDo.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyBooksProECom.Utils
{
    public class AppConfigServiceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;
        private bool _firstStart = true;

        public AppConfigServiceMiddleware(RequestDelegate next,
                                          IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (_firstStart)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var appConfigService = scope.ServiceProvider.GetRequiredService<IAppConfigService>();

                    ApiHelper.ApiConfig = await appConfigService.GetApiClientConfig(CancellationToken.None);         
                }

                _firstStart = false;
            }
            
            await _next.Invoke(httpContext);
        }
    }
}
