using Moamalat.Services;
using System;

namespace Moamalat
{
    public class OpenExternalUrl : IOpenExternalUrl
    {

        // public event EventHandler<bool> OnClosed;
        public async Task OpenUrlAsync(string url)
        {
            try
            {
                // Application.Current.MainPage = new NavigationPage(new UrlWebView(url));
                await App.Current.MainPage.Navigation.PushModalAsync(new UrlWebView(url));
                //  ((MALIYAH.Maui.MainPage)App.Current.MainPage).e;
                //await Browser.OpenAsync(url,BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"An error has occured ({ex.Message})", "Ok");


            }
        }
        
    }
}
