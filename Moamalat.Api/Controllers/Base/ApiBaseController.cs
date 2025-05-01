using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Moamalat.Entities;
using System.Web;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Moamalat.API
{
    //[Authorize]
    public class ApiBaseController : ControllerBase
    {
        protected JsonSerializerOptions Serializeoptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.Arabic, UnicodeRanges.All),
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        protected TObject? Deserialize<TObject>(string Item)
        {
            //if (string.IsNullOrEmpty(Item))
            //    return null;
            //else
            return JsonSerializer.Deserialize<TObject>(Item, Serializeoptions);
        }

        protected string Serialize<TObject>(TObject Item)
        {

            if (Item == null)
            {
                return "No Data";
            }
            string result = JsonSerializer.Serialize<TObject>(Item, Serializeoptions);
            return result;
        }

       
    }
}

