using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Moamalat.Components;

namespace Moamalat
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            InitializeComponent();
        }
        public void ExternalUrl_OperationCompleted(object sender, bool isCompleted)
        {

            // Unsubscribe to the event to prevent memory leak
            (sender as UrlWebView).OperationCompeleted -= ExternalUrl_OperationCompleted;
            // Do something after change
            // refreshView.IsRefreshing = true;
            if (PageBaseComponent.Current?._navigationManager != null && isCompleted)
            {
                var navigationManager = PageBaseComponent.Current._navigationManager;
                navigationManager.NavigateTo(navigationManager.Uri, true, false);

            }
            // PageBaseComponent.Current._RatingModal.ShowAsync();
        }
        //================================================================
        // This is a delegate for Closing payment window event 
        public async Task PaymentClosed(object sender, bool isClosed)
        {
            await Task.Delay(10);
            //(sender as UrlWebView).WebViewClosed -= PaymentClosed;
            //await PageBaseComponent._RatingModal.ShowAsync();
            // return true;

        }
        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {


        }
    }
}
