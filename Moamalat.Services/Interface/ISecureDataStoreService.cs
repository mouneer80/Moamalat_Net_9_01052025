using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Services
{
    public interface ISecureDataStoreService
    {
        bool KeyExist(string key);
        ValueTask SetAsync(string key, object value);
        ValueTask<ProtectedResult<TValue>> GetAsync<TValue>(string key);
        bool Remove(string key);
        void RemoveAll();

    }

    public struct ProtectedResult<TValue>
    {
        public bool Success { get; set; }
        public TValue? Value { get; set; }
    }
   



}
