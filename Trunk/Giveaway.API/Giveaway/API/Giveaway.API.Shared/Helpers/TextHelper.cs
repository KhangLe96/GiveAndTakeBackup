using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Giveaway.API.Shared.Helpers
{
	public static class TextHelper
	{
		private static string vnCharactersWithTones = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵ";
		private static string replaceCharacters = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyy";

		private static readonly string VnCharactersWithTonesUppercase = vnCharactersWithTones.ToUpper();
		private static readonly string ReplaceCharactersUppercase = replaceCharacters.ToUpper();

		/// <summary>
		/// Convert Vietnamese string to lower, no tones setence.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ToVietnameseNoTones(this string input)
		{
			if (!input.IsVietnameseWithTones())
			{
				return input;
			}

			int i;
			while ((i = vnCharactersWithTones.IndexOfAny(input.ToCharArray())) != -1)
			{
				if (i != -1)
				{
					input = input.Replace(vnCharactersWithTones[i], replaceCharacters[i]);
				}
			}

			while ((i = VnCharactersWithTonesUppercase.IndexOfAny(input.ToCharArray())) != -1)
			{
				if (i != -1)
				{
					input = input.Replace(VnCharactersWithTonesUppercase[i], ReplaceCharactersUppercase[i]);
				}
			}

			return input;
		}

		public static bool IsVietnameseWithTones(this string input)
		{
			return input.IndexOfAny(vnCharactersWithTones.ToCharArray()) != -1 || input.IndexOfAny(VnCharactersWithTonesUppercase.ToCharArray()) != -1;
		}

		public static string JoinList(string separator, List<string> list)
		{
			return string.Join(separator + " ", list);
		}

		public static string Get8Digits()
		{
			var bytes = new byte[4];
			var rng = RandomNumberGenerator.Create();
			rng.GetBytes(bytes);
			uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;

			return string.Format("{0:D8}", random);
		}
	}
}
