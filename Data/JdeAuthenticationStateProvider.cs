using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BlazorState;
using MediatR;
using PoEntry.Feature.AppState;

namespace PoEntry.Data
{
    public class JdeAuthenticationStateProvider : AuthenticationStateProvider
    {
        IMediator Mediator { get; }
        Celin.AIS.Server Server { get; }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = Server.AuthResponse == null
                ? new ClaimsPrincipal()
                : new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, Server.AuthResponse.username)
                }, "JdeAuthentication"));
            return Task.FromResult(new AuthenticationState(user));
        }
        async public Task<string> Login(string user, string password)
        {
            Server.AuthRequest.username = user;
            Server.AuthRequest.password = password;
            try
            {
                await Server.AuthenticateAsync();
                await Mediator.Send(new AuthResponseAction { AuthResponse = Server.AuthResponse });
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return string.Empty;
        }
        async public Task Logout()
        {
            await Server.LogoutAsync();
            await Mediator.Send(new AuthResponseAction { AuthResponse = Server.AuthResponse });
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public void Notify()
        {
        }
        public JdeAuthenticationStateProvider(E1Server e1Server, IMediator mediator)
        {
            Server = e1Server;
            Mediator = mediator;
        }
    }
}
