using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Mdichat.MdiWebService.DTO;
using Newtonsoft.Json;
using Mdichat.Model;//

namespace Mdichat.MdiWebService
{
    public class RegistrationService : IRegistrationService
    {
        HttpClient client;
        private readonly IServiceBase _serviceBase;

        public RegistrationService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            _serviceBase = new ServiceBase();
        }

       /// <summary>
       /// Conform email by security code
       /// </summary>
       /// <param name="email"></param>
       /// <param name="code"></param>
       /// <returns></returns>
        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            string result = string.Empty;
            try
            {
                ConfirmEmail BE = new ConfirmEmail() {Email= email, Code= code };
                var json = JsonConvert.SerializeObject(BE);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(Constants.BaseUrl + Constants.urlConfirmEmail, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }              
        }

        /// <summary>
        /// Register with MDi by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<string> RequestRegisterEmailAsync(string email)
        {
            string result = string.Empty;
            try
            {
                RegisterEmail BE = new RegisterEmail() { Email = email };
                var json = JsonConvert.SerializeObject(BE);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(Constants.BaseUrl + Constants.urlRegsterEmail, content);

                if (response.IsSuccessStatusCode)
                {
                    var _result = await response.Content.ReadAsStringAsync();
                    var devicetoken = JsonConvert.DeserializeObject<string>(_result);
                    return devicetoken;
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        result = Constants.S_Exists;
                    }
                    else
                    {
                        result = Constants.S_NotSuccess;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<string> RegisterMobileNumber(MobileNoRegistration payload)
        {
            try
            {
                var response = await _serviceBase.Post(JsonConvert.SerializeObject(payload), Constants.RegisterMobileUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var devicetoken = JsonConvert.DeserializeObject<string>(result);
                    return devicetoken;
                    //  return response.Content.ToString();
                }
                return ""; 
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<bool> ConfirmMobileNumber(VerifyCode payload)
        {
            try
            {
                var response = await _serviceBase.Post(JsonConvert.SerializeObject(payload), Constants.VerifyMobileUrl);
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

        public async Task<MdiResponse> UpdateUserData(UserRegister payload)
        {
            try
            {
               
                var response = await _serviceBase.Post(JsonConvert.SerializeObject(payload), Constants.UpdateUserUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MdiResponse>(result);
                }
                return new MdiResponse {IsSuccess = false, Message = string.Empty};
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<MdiResponse> DeleteUnRegisteredUser(RegisterContact payload)
        {
            try
            {
                var response = await _serviceBase.Post(JsonConvert.SerializeObject(payload), Constants.DeleteUnregistredUserUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MdiResponse>(result);
                }
                return new MdiResponse { IsSuccess = false, Message = string.Empty };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<MdiResponse> EditUserData(UserRegister payload)
        {
            try
            {

                var response = await _serviceBase.Post(JsonConvert.SerializeObject(payload), Constants.EditUserUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MdiResponse>(result);
                }
                return new MdiResponse { IsSuccess = false, Message = string.Empty };
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
