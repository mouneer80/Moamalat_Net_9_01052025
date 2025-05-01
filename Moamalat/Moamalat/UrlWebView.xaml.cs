namespace Moamalat;

public partial class UrlWebView : ContentPage
{
	public event EventHandler<bool> OperationCompeleted;
    public event EventHandler<bool> WebViewClosed;
    // public Task EventHandler<bool> asyncClosed

    public UrlWebView(string Url)
    {
        InitializeComponent();
        webView.Source = Url;
        if (OperationCompeleted == null)
            OperationCompeleted += ((Moamalat.MainPage)App.Current.MainPage).ExternalUrl_OperationCompleted;
        if (WebViewClosed == null)
            WebViewClosed += async (sender,
                                     eventArgs) => { await ((Moamalat.MainPage)App.Current.MainPage).PaymentClosed(sender, eventArgs); };// ((BeneficiarySystem.Maui.MainPage)App.Current.MainPage).PaymentClosed;

    }


    private async void webView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        //OperationCompeleted.


        await Checking(e.Url);
    }


    private async Task Checking(string Url)
    {
        if (Url.Contains("PaymentResult") && !Url.Contains("Close"))
        {
            OperationCompeleted?.Invoke(this, true);
        }
        if (Url.Contains("Close"))
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());
            //await Navigation.PopModalAsync();
            // OperationCompeleted?.Invoke(this, EventArgs.Empty);
            WebViewClosed.Invoke(this, true);
            await Navigation.PopModalAsync();
        }

    }
}