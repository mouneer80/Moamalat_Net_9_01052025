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

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityPluralNameAttribute : Attribute
    {
        public string PluralName { get; }

        public EntityPluralNameAttribute(string pluralName)
        {
            PluralName = pluralName;
        }
    }
}
