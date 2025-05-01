using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace Moamalat.Components
{
    public class CustomErrorBoundary : ErrorBoundary
    {
        //[Inject]
        //private IHostEnvironment Env { get; set; }

        protected override Task OnErrorAsync(Exception exception)
        {
            //if (Env.IsDevelopment())
            //{
            //    return base.OnErrorAsync(exception);
            //}

            return Task.CompletedTask;


        }
    }
}

