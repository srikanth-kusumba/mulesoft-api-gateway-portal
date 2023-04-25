
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using NLog;
using System.Net.Sockets;
using System.Net;

namespace ControlPlane.Data
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ISessionStorageService _sessionStorageService;
        NLog.ILogger logger;
        public  CustomAuthStateProvider(ISessionStorageService sessionStorageService)
        {
            this._sessionStorageService = sessionStorageService;
            logger = LogManager.GetCurrentClassLogger();
        }

        public void MarkUserAsLoggedOut()
        {
            _sessionStorageService.RemoveItemAsync("anypoint_id");

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            logger.Info("MarkUserAsLoggedOut Request Completed");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anypoint_id = await _sessionStorageService.GetItemAsync<string>("anypoint_id");

            var identity = new ClaimsIdentity();

            if (anypoint_id != null)
            {
                var claims = new List<Claim>();
                Claim anypointClaim = new Claim(ClaimTypes.Name, anypoint_id);
                claims.Add(anypointClaim);

                identity = new ClaimsIdentity(claims, "AnyPoint");
            }

            var user = new ClaimsPrincipal(identity);
            logger.Info("GetAuthenticationStateAsync Request Completed");
            return await Task.FromResult(new AuthenticationState(user));
        }
        public void MarkUserAsAuthenticatedInAnyPoint()
        {
            var claims = new List<Claim>();
            Claim uidClaim = new Claim(ClaimTypes.Name, "ConnectedApp");
            claims.Add(uidClaim);

            var identity = new ClaimsIdentity(claims, "AnyPoint");
            var user = new ClaimsPrincipal(identity);
            logger.Info("MarkUserAsAuthenticated Request Completed");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOutFromAnyPoint()
        {
            _sessionStorageService.RemoveItemAsync("anypoint_id");

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            logger.Info("MarkUserAsLoggedOut Request Completed");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
       
    }
}
