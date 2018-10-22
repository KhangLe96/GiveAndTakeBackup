using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.PictureChooser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.Exceptions;

namespace GiveAndTake.Core.ViewModels
{
	public class CreatePostViewModel : BaseViewModelResult<bool>
	{
		private readonly IDataModel _dataModel;

		public string ProjectName => AppConstants.AppTitle;
		public IMvxCommand<List<byte[]>> ImageCommand { get; set; }

		private List<PostImage> _postImages = new List<PostImage>();
		public List<PostImage> PostImages
		{
			get => _postImages ?? new List<PostImage>();
			set
			{
				_postImages = value;
				InitSelectedImage();
			}
		}

		private ICommand _submitCommand;
		public ICommand SubmitCommand => _submitCommand ?? (_submitCommand = new MvxCommand(InitSubmit));

		private IMvxAsyncCommand _showPhotoCollectionCommand;
		public IMvxAsyncCommand ShowPhotoCollectionCommand => _showPhotoCollectionCommand ??
															  (_showPhotoCollectionCommand = new MvxAsyncCommand(ShowPhotoCollection));

		public IMvxAsyncCommand ShowCategoriesCommand { get; set; }
		public IMvxAsyncCommand ShowProvinceCityCommand { get; set; }
		public IMvxAsyncCommand CloseCommand { get; set; }

		private readonly IMvxPictureChooserTask _pictureChooserTask;

		private readonly DebouncerHelper _debouncer;

		private string _postDescription;
		private string _postTitle;
		private string _category = AppConstants.DefaultCategoryCreatePostName;
		private string _provinceCity = AppConstants.DefaultLocationFilter;
		private byte[] _bytes;
		private string _selectedImage;

		public string PostDescription
		{
			get => _postDescription;
			set => SetProperty(ref _postDescription, value );
		}

		public string PostTitle
		{
			get => _postTitle;
			set => SetProperty(ref _postTitle, value);
		}

		public byte[] Bytes
		{
			get => _bytes;
			set { _bytes = value; RaisePropertyChanged(() => Bytes); }
		}

		public string Category
		{
			get => _category;
			set => SetProperty(ref _category, value);
		}

		public string ProvinceCity
		{
			get => _provinceCity;
			set => SetProperty(ref _provinceCity, value);
		}


		public string SelectedImage
		{
			get => _selectedImage;
			set => SetProperty(ref _selectedImage, value);
		}

		public string PostDescriptionPlaceHolder { get; set; } = "Mô tả ...";
		public string PostTitlePlaceHolder { get; set; } = "Tiêu đề";
		public string BtnSubmitTitle { get; set; } = "Đăng";
		public string BtnCancelTitle { get; set; } = "Hủy";

		public CreatePostViewModel(IMvxPictureChooserTask pictureChooserTask, IDataModel dataModel)
		{
			_debouncer = new DebouncerHelper();
			_dataModel = dataModel;
			_pictureChooserTask = pictureChooserTask;
			_dataModel.SelectedCategory = _dataModel.Categories.FirstOrDefault((category => category.CategoryName == AppConstants.DefaultCategoryCreatePostName));
			InitCommand();
		}

		private void InitCommand()
		{
			ImageCommand = new MvxCommand<List<byte[]>>(InitNewImage);
			ShowCategoriesCommand = new MvxAsyncCommand(ShowCategories);
			ShowProvinceCityCommand = new MvxAsyncCommand(ShowProvinceCities);
			CloseCommand = new MvxAsyncCommand(() => NavigationService.Close(this, false));
		}

		private async Task ShowCategories()
		{
			var result = await NavigationService.Navigate<PopupCategoriesViewModel, bool>();
			if (result)
			{
				Category = _dataModel.SelectedCategory?.CategoryName;
			}
		}


		private async Task ShowProvinceCities()
		{
			var result = await NavigationService.Navigate<PopupLocationFilterViewModel, bool>();
			if (result)
			{
				ProvinceCity = _dataModel.SelectedProvinceCity.ProvinceCityName;
			}
		}

		private async Task ShowPhotoCollection()
		{
			if (PostImages.Any())
			{
				PostImages = await NavigationService.Navigate<PhotoCollectionViewModel, List<PostImage>, List<PostImage>>(PostImages);
			}
		}

		private MvxCommand _takePictureCommand;

		public ICommand TakePictureCommand
		{
			get
			{
				_takePictureCommand = _takePictureCommand ?? new MvxCommand(DoTakePicture);
				return _takePictureCommand;
			}
		}

		private void DoTakePicture()
		{
			_debouncer.Debouce(() =>
			{
				InvokeOnMainThread(() =>
				{
					_pictureChooserTask.TakePicture(3840, 95, OnPicture, () => { });
				});
			});
		}

		private void OnPicture(Stream pictureStream)
		{
			var imageBytes = new List<byte[]>();
			var memoryStream = new MemoryStream();
			pictureStream.CopyTo(memoryStream);
			Bytes = memoryStream.ToArray();
			imageBytes.Add(Bytes);
			InitNewImage(imageBytes);
		}

		public void InitNewImage(List<byte[]> imageByte)
		{
			foreach (var img in imageByte)
			{
				var image = new PostImage()
				{
					ImageData = ConvertToBase64String(img),
				};
				PostImages.Add(image);
			}

			InitSelectedImage();
		}

		public async void InitSubmit()
		{
			try
			{
				var managementService = Mvx.Resolve<IManagementService>();
				var post = new CreatePost()
				{
					Title = PostTitle,
					Description = PostDescription,
					PostImages = _postImages,
					PostCategory = (_dataModel.SelectedCategory.CategoryName == AppConstants.DefaultCategoryCreatePostName) ? AppConstants.DefaultCategoryCreatePostId : _dataModel.SelectedCategory.Id,
					Address = _dataModel.SelectedProvinceCity.Id,   //Da Nang
				};
				await managementService.CreatePost(post, _dataModel.LoginResponse.Token);
				await NavigationService.Close(this, true);
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string>(AppConstants.ErrorConnectionMessage);
			}
		}

		public string ConvertToBase64String(byte[] imageByte)
		{
			string result = Convert.ToBase64String(imageByte);
			return result;
		}

		private void InitSelectedImage()
		{
			SelectedImage = $"Đã chọn {PostImages.Count} hình";
		}
	}
}
