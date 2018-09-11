using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveAndTake.Core.Helpers
{
	public interface IMediaHelper
	{
		Task OpenGallery();
		void ClearFiles(List<string> filePaths);
	}
}
