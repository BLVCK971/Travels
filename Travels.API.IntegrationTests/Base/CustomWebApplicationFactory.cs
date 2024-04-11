using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Travels.API.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
