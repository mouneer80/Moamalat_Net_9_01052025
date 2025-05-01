using Microsoft.AspNetCore.Components;
using Moamalat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Components
{
    public class ServiceRequestBase :LocalizationComponent
    {
        
        [Parameter]
        public string? ServiceId { get; set; }

    }
}
