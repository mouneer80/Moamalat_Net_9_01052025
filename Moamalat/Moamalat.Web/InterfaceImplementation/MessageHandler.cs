using Moamalat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat
{
    public class MessageHandler : IMessageHandler
    {
        public async Task ShowAlert(string title, string message, string accept, string cancel)
        {
            //await Application.Current.MainPage.DisplayAlert(title,message,accept);
            await Task.Delay(1);
        }
    }
}
