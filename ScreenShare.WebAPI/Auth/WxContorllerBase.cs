using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScreenShare.Model;
using ScreenShare.Utils;

namespace ScreenShare.WebAPI.Auth
{
    public class WxContorllerBase : ControllerBase
    {
        UserSession _userSession;
        public WxContorllerBase()
        {
            IHttpContextAccessor ih = new HttpContextAccessor();
            var headers = ih.HttpContext.Request.Headers;
            string ip = ih.HttpContext.Connection.RemoteIpAddress.ToString();
            string token = headers["Authorization"];
            _userSession = JsonConvert.DeserializeObject<UserSession>(DESEncrypt.Decrypt(token));

        }
    }
}
