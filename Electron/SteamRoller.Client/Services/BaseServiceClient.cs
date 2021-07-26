

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SteamRoller.API.Client
{
   

    public class AuthenticatedHttpClient : HttpClient, IAuthenticatedHttpClient
    {
    
        public AuthenticatedHttpClient(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
         //   Prevent.NullObject(contextAccessor, nameof(contextAccessor));

            // Func<Task<string>> getToken = async () => await contextAccessor.HttpContext.GetTokenAsync("access_token");
            // var accessToken = getToken.Invoke().Result;

            // // TODO: pass in options object
            this.BaseAddress = new Uri(configuration["SteamRoller:Uri"]);

            // this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public new async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption responseHeadersRead,
            CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, responseHeadersRead, cancellationToken);
        }
    }


    public interface IAuthenticatedHttpClient : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption responseHeadersRead, CancellationToken cancellationToken);
    }

}

