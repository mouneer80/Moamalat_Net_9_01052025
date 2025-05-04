using Microsoft.AspNetCore.Components;
using Moamalat.Entities;
using Moamalat.Services;

namespace Moamalat
{
    public class PlatformHandler : IPlatformHandler
    {
        //private SweetAlert _alert { get; set; } = new SweetAlert();


        public RunningPlatform GetRunningPlatform()
        {
            //if (Environments.Development == "Development")
            //    Console.WriteLine("");
            return RunningPlatform.WEB;
        }
        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            await Task.Delay(1);
            return true;
            //throw new NotImplementedException();
        }

        public Task<string> DisplayPromptAsync(string title, string message, string accept, string cancel)
        {
            throw new NotImplementedException();
        }

        public async Task ShowAlert(string title, string message, string accept, string cancel)
        {
            //await Application.Current.MainPage.DisplayAlert(title,message,accept);
            // CreateAlertComponent(title, message, accept, cancel);

            await Task.Delay(1);
        }

        protected void CreateAlertComponent(string title, string message, string accept, string cancel)
        {
            //< SweetAlert ConfirmButtonText = "@AlertDataMessage[0]"
            //AlertTitle = "@AlertDataMessage[1]"
            //AlertMessage = "@AlertDataMessage[2]"
            //isShown = "isAlertVisible"
            //OnAlertClosed = "HandleAlertClose"
            //AlertType = "sweetAlertType" />



            //RenderFragment renderFragment = (builder) =>
            //{
            //    builder.OpenComponent(0, typeof(ModalComponent));
            //    builder.AddAttribute(0, "ref", _alert);
            //    builder.AddAttribute(0, "AlertTitle", title);
            //    builder.AddAttribute(0, "AlertMessage", message);
            //    builder.AddAttribute(0, "isShown", true);
            //    builder.AddAttribute(0, "AlertType", SweetAlertType.ERROR_ALERT);
            //    builder.AddAttribute(0, "ConfirmButtonText", accept);

            //};
        }
    }
}
