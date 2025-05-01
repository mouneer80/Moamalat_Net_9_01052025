using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
//using System.Text.Encodings;
using System.Threading.Tasks;
using Moamalat.Data;
using Moamalat.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
//using System.Text.Json;
//using MillSoft.Core;
namespace Moamalat.API
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[Action]")]
    [ApiController]


    public abstract class APIControllerBase<TEntity> : ControllerBase
            where TEntity : class, IEntity
        // where TRepository : IRepository<TEntity>
    {
        //private readonly TRepository repository;
        //JsonSerializerOptions options = new()
        //{
        //    ReferenceHandler = ReferenceHandler.IgnoreCycles,
        //    WriteIndented = true
        //};
        protected JsonSerializerOptions Serializeoptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.Arabic, UnicodeRanges.All),
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        //public APIControllerBase(TRepository repository)
        //{
        //    this.repository = repository;
        //}


        protected TObject? Deserialize<TObject>(string Item)
        {
            //if (string.IsNullOrEmpty(Item))
            //    return null;
            //else
                return JsonSerializer.Deserialize<TObject>(Item);
        }
        protected string Serialize<TObject>(TObject Item)
        {

            if (Item == null)
            {
                return "No Data";
            }
            string result= JsonSerializer.Serialize<TObject>(Item, Serializeoptions);
            return result;
        }

    }
}
