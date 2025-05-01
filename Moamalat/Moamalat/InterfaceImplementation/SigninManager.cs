using Moamalat.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.MobileApp
{
    public class SigninManager : ISigninManager
    {
        //private readonly NavigationManager _Nav { get; set; }
        //public SigninManager(NavigationManager Nav)
        //{
        //    _Nav = Nav;
                
        //}
        public Task SignIn()
        {
            throw new NotImplementedException();
        }

        public void SignOut()
        {

            System.Environment.Exit(0); // Application.


        }
    }
}
