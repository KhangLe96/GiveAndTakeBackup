using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Extensions
{
	public static class IDictionaryExt
	{
		public static T ToObject<T>(this IDictionary<string, string> dictionary) where T : new()
		{
			var t = new T();
			PropertyInfo[] properties = t.GetType().GetProperties();

			foreach (PropertyInfo property in properties)
			{
				var jsonIgnoreAttributes = property.GetCustomAttributes(typeof(JsonIgnoreAttribute), false).OfType<JsonIgnoreAttribute>().ToList();

				if (jsonIgnoreAttributes.Any())
				{
					continue;
				}

				var dataMemberAttributes = property.GetCustomAttributes(typeof(DataMemberAttribute), false).OfType<DataMemberAttribute>().ToList();
				var dataMemberName = dataMemberAttributes.Any() ? dataMemberAttributes[0].Name : property.Name;

				if (!dictionary.Any(x => x.Value != null && x.Key.Equals(dataMemberName, StringComparison.InvariantCultureIgnoreCase)))
				{
					continue;
				}

				var item = dictionary.First(x => x.Key.Equals(dataMemberName, StringComparison.InvariantCultureIgnoreCase));

				// Find which property type (int, string, double? etc) the CURRENT property is...
				Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

				// Fix nullables...
				Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;
				var newA = TypeDescriptor.GetConverter(newT).ConvertFromInvariantString(item.Value);
				// ...and change the type
				//object newA = Convert.ChangeType(item.Value, newT);
				t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
			}

			return t;
		}
	}
}
