using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Giveaway.Data.EF.Utils
{
    public static class ReflectionUtil
    {
        public static PropertyInfo GetKeyProperty<T>()
            => GetKeyProperty(typeof(T));

        public static PropertyInfo GetKeyProperty(Type type)
        {
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var keyAttribute = typeof(KeyAttribute);
            return props.FirstOrDefault(x => x.CustomAttributes != null && x.CustomAttributes.Any(a => a.AttributeType == keyAttribute))
                ?? props.FirstOrDefault(x => x.Name.ToLowerInvariant() == "id");
        }
    }
}