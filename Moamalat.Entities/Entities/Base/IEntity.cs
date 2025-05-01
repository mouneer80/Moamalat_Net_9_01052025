using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moamalat.Entities
{
    public interface IEntity
    {
       
    }

    public class DBResult<TValue>
    {
        public TValue Result { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
