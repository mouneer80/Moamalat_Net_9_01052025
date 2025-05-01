using Moamalat.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Components
{
    public class PageBaseComponent : LocalizationComponent
    {

        [Inject]
        public NavigationManager? _navigationManager { get; set; }
        public static PageBaseComponent? Current;



        protected PageBaseComponent()
        {

            Current = this;

        }
        //private bool disposedValue;

        ////[Inject]
        ////IJSRuntime _Js { get; set; }
        //[Inject]
        //public NavigationManager _NavigationManager { get; set; }
        //[Inject]
        //public PageHistoryState _PageHistoryState { get; set; }

        //protected override async Task OnInitializedAsync()
        //{
        //    //await js.InvokeVoidAsync("SetLang");

        //    _NavigationManager.LocationChanged += LocationChanged;
        //    await base.OnInitializedAsync();
        //}

        //void LocationChanged(object sender, LocationChangedEventArgs e)
        //{
        //    string navigationMethod = e.IsNavigationIntercepted ? "HTML" : "code";
        //    System.Diagnostics.Debug.WriteLine($"Notified of navigation via {navigationMethod} to {e.Location}");
        //    Console.WriteLine($"Notified of navigation via {navigationMethod} to {e.Location}");
        //}

        //void IDisposable.Dispose()
        //{
        //    // Unsubscribe from the event when our component is disposed
        //    _NavigationManager.LocationChanged -= LocationChanged;
        //}
        //public Stack<string>? VisitedPage { get; set; }

        ////protected override async Task OnAfterRenderAsync(bool firstRender)
        ////{
        ////    if (firstRender)
        ////        await _Js.InvokeVoidAsync("SetLang");
        ////}
        ////protected override async Task OnInitializedAsync()
        ////{
        ////    await _Js.InvokeVoidAsync("SetLang");
        ////}
        //protected async void GoBack()
        //{
        //    await _Js.InvokeVoidAsync("history.back");
        //}


    }
}
