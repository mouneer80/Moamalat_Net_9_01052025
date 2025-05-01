using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Moamalat.Services;

namespace Moamalat
{
    public class SecureSessionStoreService : ISecureDataStoreService
    {
        //public List<string> Keys {
        //    get
        //    {
        //        SecureStorage.DefaultAccessible.
        //    } set; } = new List<string>();

        public bool KeyExist(string key)
        {
            var value = SecureStorage.GetAsync(key).Result;

            if (string.IsNullOrEmpty(value))
                return false;
            else
                return true;
        }
        public async ValueTask<ProtectedResult<TValue>> GetAsync<TValue>(string key)
        {
            var Result = await SecureStorage.Default.GetAsync(key);

            ProtectedResult<TValue> result =
            new ProtectedResult<TValue>
            {
                Success = false

            };
            if (Result != null)
            {
                result = new ProtectedResult<TValue>
                {
                    Success = true,
                    Value = JsonSerializer.Deserialize<TValue>(Result)
                };


            }


            return result;
        }

        public bool Remove(string key)
        {
            return SecureStorage.Default.Remove(key);
        }

        public void RemoveAll()
        {
            SecureStorage.Default.Remove("AuthToken");
            SecureStorage.Default.Remove("UserData");
            SecureStorage.Default.Remove("CompanyId");
            SecureStorage.Default.Remove("GradeId");
            SecureStorage.Default.Remove("Menu");
            //SecureStorage.Default.Remove("");
            //SecureStorage.Default.Remove("");
            ////SecureStorage.Default.RemoveAll();
        }

        public async ValueTask SetAsync(string key, object value)
        {
           await SecureStorage.Default.SetAsync(key, JsonSerializer.Serialize(value)); 
        }
    }
}
