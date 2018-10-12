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

		private List<Image> _postImages;
		public List<Image> PostImages
		{
			get => _postImages;
			set => SetProperty(ref _postImages, value);
		}

		private int _postImageIndex;
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

		private bool _canNavigateLeft;
		public bool CanNavigateLeft
		{
			get => _canNavigateLeft;
			set => SetProperty(ref _canNavigateLeft, value);
		}

		private bool _canNavigateRight;
		public bool CanNavigateRight
		{
			get => _canNavigateRight;
			set => SetProperty(ref _canNavigateRight, value);
		}

		public IMvxCommand CloseCommand { get; set; }
		public IMvxCommand NavigateLeftCommand { get; set; }
		public IMvxCommand NavigateRightCommand { get; set; }
		public IMvxCommand<int> UpdateImageIndexCommand { get; set; }

		private readonly IDataModel _dataModel;

		#endregion

		public PostImageViewModel(IDataModel dataModel)
		{
			_dataModel = dataModel;
			CloseCommand = new MvxCommand(() => NavigationService.Close(this, false));
			NavigateLeftCommand = new MvxCommand(() => PostImageIndex--);
			NavigateRightCommand = new MvxCommand(() => PostImageIndex++);
			UpdateImageIndexCommand = new MvxCommand<int>(index => PostImageIndex = index);
		}

		public override Task Initialize()
		{
			PostImages = _dataModel.PostImages;
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
