using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Giveaway.API.Shared.Enumerators
{
	internal class ConstEnumerable<T> : IEnumerable<T>
	{
		private readonly List<T> consts;

		public ConstEnumerable(Type type)
		{
			consts = type
				.GetFields()
				.Where(x => typeof(T).IsAssignableFrom(x.FieldType))
				.Select(x => (T)x.GetValue(null))
				.ToList();
		}

		public IEnumerator<T> GetEnumerator() => consts.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
