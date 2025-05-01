using Moamalat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Services
{
    public interface IPlatformHandler
    {
        Task ShowAlert(string title, string message, string accept, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task<string> DisplayPromptAsync(string title, string message, string accept, string cancel);
        RunningPlatform GetRunningPlatform();
        
    }
}
