using Giveaway.Data.EF.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace Giveaway.Data.EF.Helpers
{
	public static class HashHelper
	{
		private static readonly HashAlgorithm Algorithm = new SHA256Managed();
		private static readonly RNGCryptoServiceProvider CryptoServiceProvider = new RNGCryptoServiceProvider();

		public static byte[] ComputeHash(string input, byte[] salt)
		{
			var bytes = Encoding.UTF8.GetBytes(input);
			return Algorithm.ComputeHash(bytes.ConcatArray(salt));
		}

		public static byte[] GenerateSalt()
		{
			var bytes = new byte[64];
			CryptoServiceProvider.GetNonZeroBytes(bytes);
			return bytes;
		}
	}
}
