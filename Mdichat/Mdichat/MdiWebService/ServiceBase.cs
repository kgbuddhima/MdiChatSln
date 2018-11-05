using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MdiChat.Model;

namespace MdiChat.MdiWebService
{
    public class ServiceBase: IServiceBase
    {
        readonly HttpClient _client;

        public ServiceBase()
        {
            _client = new HttpClient(); //{MaxResponseContentBufferSize = 256000};
            if (!string.IsNullOrEmpty(Helpers.Settings.AuthToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Settings.AuthToken);
            }
        }
        public async Task<HttpResponseMessage> Get(string url)
        {
            try
            {
                if (!string.IsNullOrEmpty(Helpers.Settings.AuthToken))
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Settings.AuthToken);
                }
                return await _client.GetAsync($"{Constants.BaseUrl}{url}");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> Post(string payload, string url)
        {
            try
            {
                if (!string.IsNullOrEmpty(Helpers.Settings.AuthToken))
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Settings.AuthToken);
                }
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                return await _client.PostAsync($"{Constants.BaseUrl}{url}", content);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
