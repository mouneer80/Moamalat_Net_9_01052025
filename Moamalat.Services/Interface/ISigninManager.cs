using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Services
{
    public interface ISigninManager
    {

        Task SignIn();
        void SignOut();

    }
}
