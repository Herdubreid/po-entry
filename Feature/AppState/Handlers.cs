using BlazorState;
using Microsoft.AspNetCore.Components;
using PoEntry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PoEntry.Feature.AppState
{
    public partial class AppState
    {
        public class ClearErrorHandler : RequestHandler<ClearErrorAction, AppState>
        {
            AppState AppState => Store.GetState<AppState>();
            public override Task<AppState> Handle(ClearErrorAction aRequest, CancellationToken aCancellationToken)
            {
                AppState.Error = null;
                return Task.FromResult(AppState);
            }
            public ClearErrorHandler(IStore store) : base(store) { }
        }
        public class CreatePoHandler : RequestHandler<CreatePoAction, AppState>
        {
            IUriHelper UriHelper { get; }
            E1Server E1Server { get; }
            AppState AppState => Store.GetState<AppState>();
            async public override Task<AppState> Handle(CreatePoAction aRequest, CancellationToken aCancellationToken)
            {
                AppState.Error = null;
                AppState.BusyMessage = "Creating Purchase Order...";
                UriHelper.NavigateTo("busy");
                try
                {
                    var order = AppState.CurrentBPRows
                        .Where(r => r.Selected)
                        .ToList();
                    var mcu = AppState.CurrentBP.F0006_MCU;
                    var response = await E1Server.RequestAsync<W43101Response>(new W43101BRequest(mcu, order));
                    if (response.fs_P43101_W43101B != null)
                    {
                        AppState.Error = new List<Celin.AIS.ErrorWarning>(response.fs_P43101_W43101B.errors);
                        AppState.PoLines = new List<W43101Row>(response.fs_P43101_W43101B.data.gridData.rowset);
                        UriHelper.NavigateTo("items");
                    }
                    if (response.fs_P43032_W43032C != null)
                    {
                        AppState.PoResponse.AddRange(response.fs_P43032_W43032C.data.gridData.rowset);
                        foreach (var item in AppState.CurrentBPRows.Where(r => r.Selected)) { item.Selected = false; }
                        UriHelper.NavigateTo("create-po");
                    }
                }
                catch(Exception e)
                {
                    UriHelper.NavigateTo("error/" + e.Message);
                }
                return AppState;
            }
            public CreatePoHandler(IStore store, E1Server e1Server, IUriHelper uriHelper) : base(store)
            {
                E1Server = e1Server;
                UriHelper = uriHelper;
            }
        }
        public class ToggleSelectIBPHandler : RequestHandler<ToggleSelectIBPAction, AppState>
        {
            AppState AppState => Store.GetState<AppState>();
            public override Task<AppState> Handle(ToggleSelectIBPAction aRequest, CancellationToken aCancellationToken)
            {
                var row = AppState.CurrentBPRows.Find(r => r.F4102_LITM == aRequest.LITM);
                row.Selected = !row.Selected;
                return Task.FromResult(AppState);
            }
            public ToggleSelectIBPHandler(IStore store) : base(store) { }
        }
        public class V4102XIPHandler : RequestHandler<V4102XPIAction, AppState>
        {
            E1Server E1Server { get; set; }
            AppState AppState => Store.GetState<AppState>();
            async public override Task<AppState> Handle(V4102XPIAction aRequest, CancellationToken aCancellationToken)
            {
                AppState.CurrentBP.v4102XPIResponse = await E1Server.RequestAsync<V4102XPIResponse>(new V4102XPIBrowser(aRequest.BP));
                AppState.CurrentBPRows.Clear();
                AppState.CurrentBPRows.AddRange(AppState.CurrentBP.v4102XPIResponse.fs_DATABROWSE_V4102XPI.data.gridData.rowset);
                return AppState;
            }
            public V4102XIPHandler(IStore store, E1Server e1Server) : base(store)
            {
                E1Server = e1Server;
            }
        }
        public class SelectBPHandler : RequestHandler<SelectBPAction, AppState>
        {
            IUriHelper UriHelper { get; }
            E1Server E1Server { get; }
            AppState AppState => Store.GetState<AppState>();
            async public override Task<AppState> Handle(SelectBPAction aRequest, CancellationToken aCancellationToken)
            {
                AppState.BusyMessage = "Loading Items...";
                UriHelper.NavigateTo("busy");
                var row = AppState.BPs.Find(bp => bp.F0006_MCU == aRequest.BP);
                AppState.CurrentBP = row;
                AppState.CurrentBPRows.Clear();
                if (row.v4102XPIResponse == null)
                {
                    try
                    {
                        row.v4102XPIResponse = await E1Server.RequestAsync<V4102XPIResponse>(new V4102XPIBrowser(aRequest.BP));
                    }
                    catch (Exception e)
                    {
                        UriHelper.NavigateTo("error/" + e.Message);
                    }
                }
                AppState.CurrentBPRows.AddRange(row.v4102XPIResponse.fs_DATABROWSE_V4102XPI.data.gridData.rowset);
                UriHelper.NavigateTo("items");
                return AppState;
            }
            public SelectBPHandler(IStore store, E1Server e1Server, IUriHelper uriHelper) : base(store)
            {
                E1Server = e1Server;
                UriHelper = uriHelper;
            }
        }
        public class AuthResponseHandler : RequestHandler<AuthResponseAction, AppState>
        {
            AppState AppState => Store.GetState<AppState>();
            public override Task<AppState> Handle(AuthResponseAction aRequest, CancellationToken aCancellationToken)
            {
                AppState.AuthResponse = aRequest.AuthResponse;
                return Task.FromResult(AppState);
            }
            public AuthResponseHandler(IStore store) : base(store) { }
        }
        public class F4102BPsHandler : RequestHandler<F4102BPsAction, AppState>
        {
            IUriHelper UriHelper { get; }
            E1Server E1Server { get; }
            AppState AppState => Store.GetState<AppState>();
            async public override Task<AppState> Handle(F4102BPsAction aRequest, CancellationToken aCancellationToken)
            {
                try
                {
                    var f4102 = await E1Server.RequestAsync<F4102BPsResponse>(new F4102GroupBPsBrowser());
                    var f0006 = await E1Server.RequestAsync<F0006Response>(new F0006Browser(
                        f4102.ds_F4102.output
                        .Select(r => r.groupBy.MCU.Trim())
                        .ToArray()));
                    AppState.BPs = new List<F0006Row>(f0006.fs_DATABROWSE_F0006.data.gridData.rowset);
                }
                catch (Exception e)
                {
                    UriHelper.NavigateTo("error/" + e.Message);
                }
                return AppState;
            }
            public F4102BPsHandler(IStore store, E1Server e1Server, IUriHelper uriHelper) : base(store)
            {
                E1Server = e1Server;
                UriHelper = uriHelper;
            }
        }
    }
}
