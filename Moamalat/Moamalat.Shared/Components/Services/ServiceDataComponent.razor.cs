using Microsoft.AspNetCore.Components;
using Moamalat.Entities;
using Moamalat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Components
{
    public partial class ServiceDataComponent : LocalizationComponent
    {
        
        [Parameter]
        public EventCallback<object> OnParametersEntered { get; set; }
        [Parameter]
        public List<ServiceParameter>? DataParameters { get; set; }

        //protected override async Task OnParametersSetAsync()
        //{

        //    if (!string.IsNullOrEmpty(ServiceId))
        //    {
        //        Parameters = 

        //    }

        //}

        protected async Task OnNextButtonClicked()
        {
            DataParameters.ForEach(a =>
            {
                a.isValid = !string.IsNullOrEmpty(a.ParameterValue);

            });

            if(DataParameters.Any(p=>!p.isValid))
            {

            }
            else
            {
                await OnParametersEntered.InvokeAsync(DataParameters);
            }
           
        }
    }
}
