using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mdichat.MdiWebService
{
    public interface IServiceBase
    {
        Task<HttpResponseMessage> Get(string url);
        Task<HttpResponseMessage> Post(string payload, string url);
    }
}
