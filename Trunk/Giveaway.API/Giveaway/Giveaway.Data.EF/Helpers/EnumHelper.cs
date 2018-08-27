using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Giveaway.Data.EF.Helpers
{
	public static class EnumHelper
	{
		public static string GetDisplayValue(Enum value)
		{
			var fieldInfo = value.GetType().GetRuntimeField(value.ToString());

			if (!(fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) is DisplayAttribute[] descriptionAttributes))
			{
				return string.Empty;
			};

			return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Description : value.ToString();
		}
	}
}
