using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace PoEntry.Data
{
    public class E1Server : Celin.AIS.Server
    {
        public E1Server(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(configuration["baseUrl"], httpClientFactory.CreateClient()) { }
    }
}
