using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Nethereum.Blazor;
using Nethereum.Metamask;
using Nethereum.Metamask.Blazor;
using Nethereum.UI;
using WasmDemo4;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IMetamaskInterop, MetamaskBlazorInterop>();
builder.Services.AddSingleton<MetamaskHostProvider>();

//Add metamask as the selected ethereum host provider
builder.Services.AddSingleton(services =>
{
    var metamaskHostProvider = services.GetService<MetamaskHostProvider>();
    var selectedHostProvider = new SelectedEthereumHostProviderService();
    selectedHostProvider.SetSelectedEthereumHostProvider(metamaskHostProvider);
    return selectedHostProvider;
});


builder.Services.AddSingleton<AuthenticationStateProvider, EthereumAuthenticationStateProvider>();

// builder.Services.AddValidatorsFromAssemblyContaining<Nethereum.Erc20.Blazor.Erc20Transfer>();


await builder.Build().RunAsync();
