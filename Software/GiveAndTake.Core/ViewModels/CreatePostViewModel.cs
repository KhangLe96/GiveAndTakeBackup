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
			// REVIEW [KHOA]: _postImages is already initialized above
			get => _postImages ?? new List<PostImage>();
			set
			{
				_postImages = value;
				InitSelectedImage();
			}
		}

		public ICommand SubmitCommand { get; set; }

		private IMvxAsyncCommand _showPhotoCollectionCommand;
		public IMvxAsyncCommand ShowPhotoCollectionCommand => _showPhotoCollectionCommand ??
															  (_showPhotoCollectionCommand = new MvxAsyncCommand(ShowPhotoCollection));

		// REVIEW [KHOA]: use lazy initiation
		public IMvxCommand ShowCategoriesCommand { get; set; }
		public IMvxCommand ShowProvinceCityCommand { get; set; }
		public IMvxCommand BackPressedCommand { get; set; }

		private readonly IMvxPictureChooserTask _pictureChooserTask;

		private readonly DebouncerHelper _debouncer;

		private string _postDescription;
		private string _postTitle;
		private string _category = AppConstants.DefaultCategoryCreatePostName;
		private string _provinceCity = AppConstants.DefaultLocationFilter;
		private byte[] _bytes;
		private string _selectedImage;
		private Category _selectedCategory;
		private ProvinceCity _selectedProvinceCity;

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


		// REVIEW [KHOA]: get texts from locale instead of hard-codes
		public string PostDescriptionPlaceHolder { get; set; } = "Mô tả (Nhãn hiệu, kiểu dáng, màu sắc, ... )";
		public string PostTitlePlaceHolder { get; set; } = "Tiêu đề (Thương hiệu, thể loại, ...)";
		public string BtnSubmitTitle { get; set; } = "Đăng";
		public string BtnCancelTitle { get; set; } = "Hủy";

		public CreatePostViewModel(IMvxPictureChooserTask pictureChooserTask, IDataModel dataModel)
		{
			_debouncer = new DebouncerHelper();
			_dataModel = dataModel;
			_pictureChooserTask = pictureChooserTask;
			_selectedCategory = _dataModel.Categories.FirstOrDefault(category => category.CategoryName == AppConstants.DefaultCategoryCreatePostName);
			_selectedProvinceCity = _selectedProvinceCity ?? _dataModel.ProvinceCities.First(p => p.ProvinceCityName == AppConstants.DefaultLocationFilter);
			InitCommand();
		}

		private void InitCommand()
		{
			ImageCommand = new MvxCommand<List<byte[]>>(InitNewImage);
			ShowProvinceCityCommand = new MvxCommand(ShowLocationFiltersPopup);
			BackPressedCommand = new MvxAsyncCommand(BackPressed);
			ShowCategoriesCommand = new MvxCommand(ShowCategoriesPopup);
			SubmitCommand = new MvxAsyncCommand(OnCreatePost);
		}

		private async Task BackPressed()
		{
			var result = await NavigationService.Navigate<PopupMessageViewModel, string, RequestStatus>(AppConstants.DeleteConfirmationMessage);
			if (result == RequestStatus.Submitted)
			{
				await NavigationService.Close(this, false);
			}
		}

		private async void ShowCategoriesPopup()
		{
			var result = await NavigationService.Navigate<PopupListViewModel, PopupListParam, string>(new PopupListParam
			{
				Title = AppConstants.PopupCategoriesTitle,
				Items = _dataModel.Categories.Select(c => c.CategoryName).ToList(),
				SelectedItem = _selectedCategory.CategoryName
			});

			if (string.IsNullOrEmpty(result)) return;

			_selectedCategory = _dataModel.Categories.First(c => c.CategoryName == result);
			Category = result;
		}

		private async void ShowLocationFiltersPopup()
		{
			var result = await NavigationService.Navigate<PopupListViewModel, PopupListParam, string>(new PopupListParam
			{
				Title = AppConstants.PopupLocationFiltersTitle,
				Items = _dataModel.ProvinceCities.Select(c => c.ProvinceCityName).ToList(),
				SelectedItem = _selectedProvinceCity.ProvinceCityName
			});

			if (string.IsNullOrEmpty(result)) return;

			_selectedProvinceCity = _dataModel.ProvinceCities.First(c => c.ProvinceCityName == result);
			ProvinceCity = result;
		}

		private async Task ShowPhotoCollection()
		{
			if (PostImages.Any())
			{
				PostImages = await NavigationService.Navigate<PhotoCollectionViewModel, List<PostImage>, List<PostImage>>(PostImages);
			}

			// REVIEW [KHOA]: if there is no post image -> no action for user -> they don't know what happen
		}

		// REVIEW [KHOA]: move to command up to command region
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

		public async Task OnCreatePost()
		{
			try
			{
				var managementService = Mvx.Resolve<IManagementService>();
				var post = new CreatePost
				{
					Title = PostTitle,
					Description = PostDescription,
					PostImages = _postImages,
					PostCategory = _selectedCategory.CategoryName == AppConstants.DefaultCategoryCreatePostName ? AppConstants.DefaultCategoryCreatePostId : _selectedCategory.Id,
					Address = _selectedProvinceCity.Id
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
			// REVIEW [KHOA]: use locale
			SelectedImage = $"Đã chọn {PostImages.Count} hình";
		}
	}
}
