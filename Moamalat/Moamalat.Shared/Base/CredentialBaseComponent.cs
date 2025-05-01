using Moamalat.Components;
using Moamalat.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Components
{
    public partial class CredentialBaseComponent:PageBaseComponent
    {
        [Inject]
        protected NavigationManager navigationManager { get; set; }
        //[Inject]
        //ISecureDataStoreService _secureDataStoreService { get; set; }
        //[Inject]
        //protected AuthenticateService _AuthenticateService { get; set; }
        //[Inject]
        //protected ISecureDataStoreService _secureStorage { get; set; }
        protected bool isValid { get; set; } = true;
        protected bool isLoading { get; set; }


    }
}
