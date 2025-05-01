using Moamalat.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat
{
    public class OpenExternalUrl : IOpenExternalUrl
    {
        private NavigationManager navigationManager { get; set; }

        public OpenExternalUrl(NavigationManager _navigationManager)
        {
            navigationManager = _navigationManager;
        }

        public async Task OpenUrlAsync(string url)
        {
            //if (string.IsNullOrEmpty(url))
            //    return false;

            try
            {

                navigationManager.NavigateTo(url);

                //return true;
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }
    }
}
