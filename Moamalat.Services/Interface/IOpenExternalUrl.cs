using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Services
{
    public interface IOpenExternalUrl
    {
       // public string Url { get; set; }

        Task OpenUrlAsync(string url);
    }
}
