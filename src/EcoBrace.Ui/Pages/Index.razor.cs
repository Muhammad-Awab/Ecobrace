using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FeatureManagement;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;
using warmbrace.Shared.Constants;

namespace EcoBrace.Ui.Pages;
public partial class Index
{
    #region modelparams
    [Inject] protected IFeatureManager FeatureManager { get; set; } = default!;
    [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }
    [Inject] protected IJSRuntime JS { get; set; } = default!;
    //[Inject] protected IUserService UserService { get; set; } = default!;
    [Inject] protected IToastService ToastService { get; set; } = default!;
    [Inject] protected ILogger<Index> Logger { get; set; } = default!;

    private IJSObjectReference? module;
    private ElementReference modal;

    private string userName = string.Empty;
    private bool firstLogin = false;
    private bool emailValid = true;
    private bool roleFilled = true;
    private bool showLeadsMarketPlacePreview = false;
    private bool showLeadsMarketPlace = false;
    #endregion modelparams

    protected override async Task OnInitializedAsync()
    {
        if (await FeatureManager.IsEnabledAsync(FeatureFlags.LeadsMarketplacePreview))
        {
            showLeadsMarketPlacePreview = true;
        }
        if (await FeatureManager.IsEnabledAsync(FeatureFlags.LeadsMarketplace))
        {
            showLeadsMarketPlace = true;
        }

        if (authenticationState is not null)
        {
            var state = await authenticationState;
            userName = state?.User?.Identity?.Name ?? string.Empty;

            CheckIfNewSignUp(state);
        }
        Logger.LogInformation($"*** On Home page: user: {userName} ***", DateTime.UtcNow.ToLongTimeString());

        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JS.InvokeAsync<IJSObjectReference>("import", "./Pages/Index.razor.js");
        }

    }

    protected async void HideModal(string elementId)
    {
        if (module != null)
        {
            await module.InvokeAsync<string>("HideModal", elementId);
        }
        else
        {
            module = await JS.InvokeAsync<IJSObjectReference>("import", "./Pages/Index.razor.js");
            await module.InvokeAsync<string>("HideModal", elementId);
        }
    }

    //protected async void SaveData(string elementId)
    //{
    //    //First save data, then close modal
    //    emailValid = IsEmailValid(WaitingList.Email);
    //    roleFilled = IsRoleFileld(WaitingList.Notes);

    //    if (emailValid && roleFilled)
    //    {
    //        var added = await UserService.AddEmailToWaitListAsync(WaitingList.Email, WaitingList.Plan, WaitingList.Notes);
    //        await module.InvokeAsync<string>("HideModal", elementId);

    //        if (added)
    //            ToastService.ShowSuccess("Email added to waiting list :)");
    //    }
    //}

    private bool IsRoleFileld(string? notes)
    {
        if (string.IsNullOrEmpty(notes)) return false;

        return true;
    }

    private bool IsEmailValid(string? email)
    {
        if (string.IsNullOrEmpty(email)) return false;

        var emailPattern = @"^\w+([-.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        if (!Regex.IsMatch(email, emailPattern))
            return false;

        return true;
    }
    private void CheckIfNewSignUp(AuthenticationState? state)
    {
        var loginCountClaim = state?.User.FindFirst("loginCount");
        if (loginCountClaim != null)
        {
            if (int.TryParse(loginCountClaim.Value, out int numLogins))
            {
                if (numLogins <= 1)
                {
                    firstLogin = true;
                }
            }
        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }
}