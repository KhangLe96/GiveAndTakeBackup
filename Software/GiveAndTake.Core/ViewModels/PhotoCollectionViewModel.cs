using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross.Commands;

namespace GiveAndTake.Core.ViewModels
{
	public class PhotoCollectionViewModel : BaseViewModel<List<PostImage>, List<PostImage>>
	{
		public List<PostImage> PostImages { get; private set; }

		private ObservableCollection<PhotoTemplateViewModel> _photoTemplateViewModels;
		public ObservableCollection<PhotoTemplateViewModel> PhotoTemplateViewModels
		{
			get => _photoTemplateViewModels;
			set => SetProperty(ref _photoTemplateViewModels, value);
		}

		private IMvxCommand<int> _deleteAPhotoCommand;
		public IMvxCommand<int> DeleteAPhotoCommand =>
			_deleteAPhotoCommand ??
			(_deleteAPhotoCommand = new MvxCommand<int>(DeleteAPhoto));

		private IMvxAsyncCommand _iOSBackPressedCommand;
		public IMvxAsyncCommand IOSBackPressedCommand => _iOSBackPressedCommand ??
		(_iOSBackPressedCommand = new MvxAsyncCommand(IOSBackPressed));

		private async Task IOSBackPressed()
		{
			await NavigationService.Close(this);
		}

		public override void Prepare(List<PostImage> postImages)
		{
			PostImages = postImages;
			PhotoTemplateViewModels = new ObservableCollection<PhotoTemplateViewModel>();
			foreach (var postImage in postImages)
			{
				PhotoTemplateViewModels.Add(new PhotoTemplateViewModel()
				{
					ImageBase64Data = postImage.ImageData,
					ParentViewModel = this
				});
			}
		}

		public override void ViewDisappearing()
		{
			base.ViewDisappearing();
			CloseCompletionSource?.TrySetResult(PostImages);
		}

		public void DeleteAPhoto(int position)
		{
			if (position == -1 || PhotoTemplateViewModels == null)
			{
				return;
			}
			PhotoTemplateViewModels.RemoveAt(position);
			PostImages.RemoveAt(position);
		}
	}
}
