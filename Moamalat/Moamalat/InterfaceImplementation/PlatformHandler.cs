using Moamalat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Moamalat.Entities;

namespace Moamalat
{
    public class PlatformHandler : IPlatformHandler
    {

       public RunningPlatform GetRunningPlatform()
        {
            var _platform = RunningPlatform.ANDROID;
                #if ANDROID
                     _platform = RunningPlatform.ANDROID;
                #elif IOS
                     _platform= RunningPlatform.IOS; 

                #elif WINDOWS
                     _platform= RunningPlatform.WINDOWS;
                #endif

            return _platform;
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task<string> DisplayPromptAsync(string title, string message, string accept, string cancel)
        {

            return await Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel);

        }

        public async Task ShowAlert(string title, string message, string accept, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title,message,accept);
        }

    }
}
