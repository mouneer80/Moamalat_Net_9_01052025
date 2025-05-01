using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Services
{
    public interface IMessageHandler
    {
        Task ShowAlert(string title, string message, string accept, string cancel);
    }
}
