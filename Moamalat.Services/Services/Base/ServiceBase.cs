using Moamalat.Entities;
using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Web;
using System.Security.Cryptography.X509Certificates;

namespace Moamalat.Services
{

     public class ServiceBase<TEntity, ThttpClientFactory> : IServiceBase<TEntity>
       where TEntity : class, IEntity
       where ThttpClientFactory : IHttpClientFactory

      //where EName :typeof(string)
  {

      protected readonly HttpClient _httpClient;
      // private readonly TEntityData _entityData;
      //private HttpRequestMessage _request;


      private string ControllerName = "";
      // private ThttpClientFactory _httpClientFactory;

      protected JsonSerializerOptions Serializeoptions = new JsonSerializerOptions
      {
          Encoder = JavaScriptEncoder.Create(UnicodeRanges.Arabic, UnicodeRanges.All),
          WriteIndented = true,
          ReferenceHandler = ReferenceHandler.IgnoreCycles
      };
      // public IConfiguration Configuration { get; }
      public ServiceBase(string _ControllerName,
                         ThttpClientFactory _thttpClientFactory)
      {


          _httpClient = _thttpClientFactory.CreateClient("BaseHttpClient");
          ControllerName = _httpClient.BaseAddress + _ControllerName;

      }



      public HttpClient GetHttpClient()
      {
          return _httpClient;
      }

      public async Task<IEnumerable<TEntity>> GetByField(string field, string value)
      {

          var entity = await _httpClient.GetFromJsonAsync<List<TEntity>>(ControllerName + $"/GetByField?field={field}&value={value}");

          return entity;
      }
      public async Task<PaginationResult<TEntity>> GetPaged(PaginationResult<TEntity> _Pagination)
      {
          ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<TEntity>>(_Pagination)) };
          var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{ControllerName}/GetPaged{typeof(TEntity).Name}s", apiRequest);
          if (response.IsSuccessStatusCode)
          {
              var result = await DeserializeAsync<ApiResponse>(response);
              if (result.DataObject != null)
                  return await DeserializeAsync<PaginationResult<TEntity>>(Utilities.Decrypt(result.DataObject));
          }
          return new PaginationResult<TEntity>()
          {
              Items = Enumerable.Empty<TEntity>()

          };

      }


      //protected string EncodeEncryptedString(string Value)
      //{
      //    string encrypted = Utilities.Encrypt(Value);
      //    string encoded = HttpUtility.UrlEncode(encrypted);
      //    return encoded;
      //}

      //protected string DecodeEncryptedString(string Value)
      //{

      //    string decrypted = Utilities.Decrypt(Value);
      //    string decoded = HttpUtility.UrlDecode(decrypted);
      //    return decoded;
      //}

      public string Serialize<Tvalue>(Tvalue Value)
      {
          return JsonSerializer.Serialize(Value, Serializeoptions);
      }

      public async Task<TValue> DeserializeAsync<TValue>(HttpResponseMessage response)
      {

          var contentStream = await response.Content.ReadAsStreamAsync();
          var result = await JsonSerializer.DeserializeAsync<TValue>(contentStream, Serializeoptions);
          return result;
      }

      public async Task<TValue> DeserializeAsync<TValue>(string Json)
      {
          var result = JsonSerializer.Deserialize<TValue>(Json, Serializeoptions);
          return result;
      }


  }

}
