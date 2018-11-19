using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
	public class PostImageViewModel : BaseViewModelResult<bool>
	{
		#region Properties

		public IMvxCommand CloseCommand =>
			_closeCommand ?? (_closeCommand = new MvxCommand(() => NavigationService.Close(this, false)));

		public IMvxCommand NavigateLeftCommand =>
			_navigateLeftCommand ?? (_navigateLeftCommand = new MvxCommand(() => PostImageIndex--));

		public IMvxCommand NavigateRightCommand =>
			_navigateRightCommand ?? (_navigateRightCommand = new MvxCommand(() => PostImageIndex++));

		public IMvxCommand<int> UpdateImageIndexCommand => 
			_updateImageIndexCommand ??(_updateImageIndexCommand = new MvxCommand<int>(index => PostImageIndex = index));

		public List<Image> PostImages
		{
			get => _postImages;
			set => SetProperty(ref _postImages, value);
		}

		public int PostImageIndex
		{
			get => _postImageIndex;
			set
			{
				SetProperty(ref _postImageIndex, value);
				_dataModel.PostImageIndex = value;
				UpdateNavigationButtons();
			}
		}

		public bool CanNavigateLeft
		{
			get => _canNavigateLeft;
			set => SetProperty(ref _canNavigateLeft, value);
		}

		public bool CanNavigateRight
		{
			get => _canNavigateRight;
			set => SetProperty(ref _canNavigateRight, value);
		}
		
		private readonly IDataModel _dataModel;
		private IMvxCommand _navigateLeftCommand;
		private IMvxCommand _navigateRightCommand;
		private IMvxCommand<int> _updateImageIndexCommand;
		private IMvxCommand _closeCommand;
		private List<Image> _postImages;
		private int _postImageIndex;
		private bool _canNavigateLeft;
		private bool _canNavigateRight;

		#endregion

		public PostImageViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
		}

		public override Task Initialize()
		{
			PostImages = _dataModel.CurrentPost.Images;
			PostImageIndex = _dataModel.PostImageIndex;
			return base.Initialize();
		}

		private void UpdateNavigationButtons()
		{
			CanNavigateLeft = PostImages.Count > 1 && PostImageIndex > 0;
			CanNavigateRight = PostImages.Count > 1 && PostImageIndex < PostImages.Count - 1;
		}
	}
}
