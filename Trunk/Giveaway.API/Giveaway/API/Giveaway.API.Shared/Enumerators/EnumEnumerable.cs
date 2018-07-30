using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Giveaway.API.Shared.Enumerators
{
	internal class EnumEnumerable<T> : IEnumerable<T>
		where T : struct, IConvertible
	{
		private readonly Enum filter;
		private readonly IEnumerable<Enum> allValues;

		public EnumEnumerable(T? filter = null)
		{
			CheckType<T>();
			this.filter = filter.HasValue
				? (Enum)(object)filter
				: null;
			allValues = Enum.GetValues(typeof(T)).Cast<Enum>();
		}

		public IEnumerator<T> GetEnumerator()
		{
			var result = allValues;

			if (filter != null)
			{
				result = result.Where(x => filter.HasFlag(x));
			}

			return result.Cast<T>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private static void CheckType<T1>()
		{
			if (!typeof(T1).IsEnum)
			{
				throw new ArgumentException($"Type [{typeof(T1).Name}] is an enumerated type");
			}
		}
	}
}
