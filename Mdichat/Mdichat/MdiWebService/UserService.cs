using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MdiChat.MdiWebService.DTO;
using Newtonsoft.Json;
using MdiChat.Model;

namespace MdiChat.MdiWebService
{
    public class UserService : IUserService
    {
        private readonly IServiceBase _serviceBase;
        //  HttpClient client;
        public UserService()
        {
            _serviceBase = new ServiceBase();
            //  client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<MdiUser> GetUser()
        {
            try
            {
                var response = await _serviceBase.Get(Constants.GetUserUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<MdiUser>(result);
                    return user;
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<MdiResponse> ForgotPassword(ForgotPassword payload)
        {
            try
            {
                var response = await _serviceBase.Post(JsonConvert.SerializeObject(payload), Constants.ForgotPasswordUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MdiResponse>(result);
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> ResetPassword(PasswordReset payload)
        {
            try
            {
                var response = await _serviceBase.Post(JsonConvert.SerializeObject(payload), Constants.ResetPasswordUrl);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<MdiResponse> Login(UserLogin payload)
        {
            try
            {
                var nvc = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", payload.UserName),
                    new KeyValuePair<string, string>("password", payload.Password),
                    new KeyValuePair<string, string>("grant_type", "password")
                };

                var client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, $"{Constants.BaseUrl}{Constants.UserLoginUrl}")
                { Content = new FormUrlEncodedContent(nvc) };

                var response = await client.SendAsync(req);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var authTokenObj = JsonConvert.DeserializeObject<AuthToken>(result);
                    return new MdiResponse {AuthToken = authTokenObj.access_token, IsSuccess = true, Message = ""};
                }
                else if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                   return new MdiResponse { AuthToken = string.Empty, IsSuccess = false, Message = "User name or password is incorrect!.." };
                }
                return new MdiResponse { AuthToken = string.Empty, IsSuccess = false, Message = "" };

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
