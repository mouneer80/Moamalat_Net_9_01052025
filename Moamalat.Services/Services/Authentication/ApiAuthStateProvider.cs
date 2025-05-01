using Moamalat.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Moamalat.Services
{
    public class ApiAuthStateProvider : AuthenticationStateProvider
 {
     
     private readonly ISecureDataStoreService _secureDataStoreService;
    // private readonly HttpClient _httpClient;
     public ApiAuthStateProvider(ISecureDataStoreService secureDataStoreService)//, HttpClient httpClient)
     {

         // _httpClient = httpClient;
         _secureDataStoreService = secureDataStoreService;
         //_httpClient = httpClient;
     }
     public override async Task<AuthenticationState> GetAuthenticationStateAsync()
     {
         var AuthToken = (await _secureDataStoreService.GetAsync<string>("AuthToken")).Value;
         if(string.IsNullOrWhiteSpace(AuthToken))
         {
             return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
         }

         
        // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", AuthToken);
         return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(AuthToken))));


     }
     //public void MarkUserAsAuthenticated(LoginResponse pUser)
     //{
     //    //var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, pUser.Userdata.UserName),
     //    //                                                                        new Claim(ClaimTypes.UserData,pUser.Userdata.SystemUserId.ToString()),
     //    //                                                                        //new Claim(ClaimTypes.PrimarySid,pUser.CompanyId.ToString()),
     //    //                                                                        new Claim(ClaimTypes.Role,pUser.Role)//,
     //    //                                                                        //new Claim(ClaimTypes.GroupSid,pUser.GradeId.ToString())
     //    //                                                                        }, "apiauth"));
     //   // var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
     //    NotifyAuthenticationStateChanged(authState);
     //}

     public void MarkUserAsLoggedOut()
     {
         var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
         var authState = Task.FromResult(new AuthenticationState(anonymousUser));
         NotifyAuthenticationStateChanged(authState);
     }

     private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
     {
         var claims = new List<Claim>();
         var payload = jwt.Split('.')[1];
         var jsonBytes = ParseBase64WithoutPadding(payload);
         var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

         keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

         if (roles != null)
         {
             if (roles.ToString().Trim().StartsWith("["))
             {
                 var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                 foreach (var parsedRole in parsedRoles)
                 {
                     claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                 }
             }
             else
             {
                 claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
             }

             keyValuePairs.Remove(ClaimTypes.Role);
         }

         claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

         return claims;
     }

     private byte[] ParseBase64WithoutPadding(string base64)
     {
         switch (base64.Length % 4)
         {
             case 2: base64 += "=="; break;
             case 3: base64 += "="; break;
         }
         return Convert.FromBase64String(base64);
     }
 }
}
