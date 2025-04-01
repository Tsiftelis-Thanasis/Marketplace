
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;


namespace MarketplaceUI.Services
{

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly NavigationManager _navigation;
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

        public CustomAuthStateProvider(ILocalStorageService localStorageService, NavigationManager navigation)
        {
            _localStorageService = localStorageService;
            _navigation = navigation;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Defer localStorage access until OnAfterRenderAsync
            return new AuthenticationState(_currentUser);
        }

        public async Task InitializeAuthState()
        {
            var token = await _localStorageService.GetItemAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                var claims = JwtParser.ParseClaimsFromJwt(token);
                var identity = new ClaimsIdentity(claims, "jwt");
                _currentUser = new ClaimsPrincipal(identity);
            }
            else
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        public async Task Login(string token)
        {
            await _localStorageService.SetItemAsync("authToken", token);
            await InitializeAuthState();
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
            _navigation.NavigateTo("/login", true);
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorageService.SetItemAsync("authToken", token);
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

    }


    //public class CustomAuthStateProvider : AuthenticationStateProvider
    //{
    //    private readonly ILocalStorageService _localStorage;
    //    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    //    public CustomAuthStateProvider(ILocalStorageService localStorage)
    //    {
    //        _localStorage = localStorage;
    //    }

    //    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    //    {
    //        var token = await _localStorage.GetItemAsync("authToken");

    //        if (string.IsNullOrEmpty(token))
    //        {
    //            return new AuthenticationState(_anonymous);
    //        }

    //        var claims = JwtParser.ParseClaimsFromJwt(token); // Ensure you have this utility method
    //        var identity = new ClaimsIdentity(claims, "jwt");
    //        var user = new ClaimsPrincipal(identity);

    //        return new AuthenticationState(user);
    //    }

    //    public async Task MarkUserAsAuthenticated(string token)
    //    {
    //        await _localStorage.SetItemAsync("authToken", token);
    //        var claims = JwtParser.ParseClaimsFromJwt(token);
    //        var identity = new ClaimsIdentity(claims, "jwt");
    //        var user = new ClaimsPrincipal(identity);

    //        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    //    }

    //    public async Task Login(string token)
    //    {
    //        await _localStorage.SetItemAsync("authToken", token);
    //        var claims = JwtParser.ParseClaimsFromJwt(token);
    //        var identity = new ClaimsIdentity(claims, "jwt");
    //        _anonymous = new ClaimsPrincipal(identity);

    //        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    //    }

    //    public async Task Logout()
    //    {
    //        await _localStorage.RemoveItemAsync("authToken");
    //        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    //    }
    //}


}
