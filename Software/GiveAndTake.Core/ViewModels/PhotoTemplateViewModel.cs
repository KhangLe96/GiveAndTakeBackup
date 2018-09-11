using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class PhotoTemplateViewModel : BaseViewModel
	{
		private string _imageBase64Data;

		public string ImageBase64Data
		{
			get => _imageBase64Data;
			set => SetProperty(ref _imageBase64Data, value);
		}
		public PhotoCollectionViewModel ParentViewModel { get; set; }
		public IMvxCommand DeleteAPhotoCommand => new MvxCommand(() =>
			ParentViewModel.DeleteAPhoto(ParentViewModel.PhotoTemplateViewModels.IndexOf(this)));
	}
}