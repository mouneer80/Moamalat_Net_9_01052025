using Moamalat.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Moamalat
{
    public class SecureSessionStoreService : ISecureDataStoreService
    {
        private readonly ProtectedSessionStorage ProtectedSessionStore;
        private List<string> Keys { get; set; } = new List<string>();

        public SecureSessionStoreService(ProtectedSessionStorage _ProtectedSessionStore)
        {
            ProtectedSessionStore = _ProtectedSessionStore;
        }
        public bool KeyExist(string key)
        {

            if (!Keys.Contains(key))
                return false;
            else
                return true;
        }

        public async ValueTask SetAsync(string key, object value)
        {
            await ProtectedSessionStore.SetAsync(key, value);
            Keys.Add(key);


        }
        public async ValueTask<ProtectedResult<TValue>> GetAsync<TValue>(string key)
        {
            try
            {
                var _result = await ProtectedSessionStore.GetAsync<TValue>(key);
                ProtectedResult<TValue> result =
                new ProtectedResult<TValue>
                {
                    Success = _result.Success,
                    Value = _result.Value

                };
                return result;
            }
            catch (Exception ex)
            {
                ProtectedResult<TValue> result =
                new ProtectedResult<TValue>
                {
                    Success = false

                };
                return result;

            }
        }

        public bool Remove(string key)
        {
            ProtectedSessionStore.DeleteAsync(key);
            Keys.Remove(key);
            return true;
        }

        public void RemoveAll()
        {
            foreach (string key in Keys)
            {
                ProtectedSessionStore.DeleteAsync(key);
            }
            Keys.Clear();
        }
    }
    //public class SecureLocalStoreService : ISecureDataStoreService
    //{
    //    private readonly ProtectedLocalStorage ProtectedLocalStore;

    //    public SecureLocalStoreService(ProtectedLocalStorage _protectedLocalStore)
    //    {
    //        ProtectedLocalStore = _protectedLocalStore;
    //    }

    //    public async ValueTask<ProtectedResult<TValue>> GetAsync<TValue>(string key)
    //    {
    //        var _result = await ProtectedLocalStore.GetAsync<TValue>(key);
    //        ProtectedResult<TValue> result =
    //        new ProtectedResult<TValue>
    //        {
    //            Success = _result.Success,
    //            Value = _result.Value

    //        };
    //        return result;
    //    }

    //    public async ValueTask SetAsync(string key, object value)
    //    {
    //        await ProtectedLocalStore.SetAsync(key, value);
    //    }
    //}
}
