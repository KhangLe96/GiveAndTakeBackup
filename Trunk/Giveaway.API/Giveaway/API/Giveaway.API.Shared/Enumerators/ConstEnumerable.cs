using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Giveaway.API.Shared.Enumerators
{
	internal class ConstEnumerable<T> : IEnumerable<T>
	{
		private readonly List<T> _consts;

		public ConstEnumerable(Type type)
		{
			_consts = type
				.GetFields()
				.Where(x => typeof(T).IsAssignableFrom(x.FieldType))
				.Select(x => (T)x.GetValue(null))
				.ToList();
		}

		public IEnumerator<T> GetEnumerator() => _consts.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
