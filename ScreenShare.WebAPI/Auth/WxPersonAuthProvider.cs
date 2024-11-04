using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using ScreenShare.Irepositiories;
using ScreenShare.IRepositories;
using ScreenShare.Utils;
using System.Security.Claims;

namespace ScreenShare.WebAPI.Auth
{
    public class WxPersonAuthProvider(
        IWxPerson_Respositoriy _users_Repositories,
        ProtectedSessionStorage _protectedSessionStore
        ) : AuthenticationStateProvider
    {
        private ClaimsIdentity identity = new ClaimsIdentity();


        public async Task<string> SignIn(string Code)
        {

            var user = _users_Repositories.GetFirst(p => p.WxName == Code);

            string UserRole = "WxPerson";
            if (user.IsNull())
            {
                return "";
            }
            // 用户认证成功，创建用户的ClaimsIdentity
            var claims = new[] {
                     new Claim(ClaimTypes.Name, user.WxName),
                    new Claim(ClaimTypes.Role, UserRole)
                     };
            identity = new ClaimsIdentity(claims, UserRole);
            UserSession session = new UserSession() { UserName = user.WxName, Role = UserRole };
            //保存登录信息到redis
            await _protectedSessionStore.SetAsync("WxSession", session);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            string token = DESEncrypt.Encrypt(JsonConvert.SerializeObject(session));
            return token;

        }

        public ClaimsPrincipal GetCurrentUser()
        {
            var user = new ClaimsPrincipal(identity);
            return user;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userSessionStorageResult = await _protectedSessionStore.GetAsync<UserSession>("WxSession");
            var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
            if (userSession.IsNotNull())
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim( ClaimTypes.Role, userSession.Role) };
                identity = new ClaimsIdentity(claims, userSession.Role);
            }
            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }
    }
}
