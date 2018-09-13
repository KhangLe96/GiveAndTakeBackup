using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using GiveAndTake.Core.ViewModels.TabNavigation;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.PictureChooser;

namespace GiveAndTake.Core.ViewModels
{
	public class CreatePostViewModel : BaseViewModelResult<bool>
	{
		private readonly List<byte[]> _imageBytes = new List<byte[]>();
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
		public IMvxAsyncCommand ChoosePictureCommand { get; set; }

		private readonly IMvxPictureChooserTask _pictureChooserTask;

		private string _postDescription;
		private string _postTitle;
		private string _category;
		private string _provinceCity;
		private byte[] _bytes;
		private string _selectedImage;

		public string PostDescription
		{
			get => _postDescription;
			set => SetProperty(ref _postDescription, value);
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

		public CreatePostViewModel(IMvxPictureChooserTask pictureChooserTask, IDataModel dataModel)
		{
			_dataModel = dataModel;
			_pictureChooserTask = pictureChooserTask;
			InitCommand();
		}

		private void InitCommand()
		{
			ImageCommand = new MvxCommand<List<byte[]>>(InitNewImage);
			ShowCategoriesCommand = new MvxAsyncCommand(ShowCategories);
			ShowProvinceCityCommand = new MvxAsyncCommand(ShowProvinceCities);
			CloseCommand = new MvxAsyncCommand(async () =>
			{
				var tasks = new List<Task>
				{
					NavigationService.Close(this),
					NavigationService.Navigate<TabNavigationViewModel>(),
				};
				await Task.WhenAll(tasks);
			});
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

		private async Task ShowPhotoCollection() =>
			PostImages = await NavigationService.Navigate<PhotoCollectionViewModel, List<PostImage>, List<PostImage>>(PostImages);

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
			_pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
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

		public void InitSubmit()
		{
			//if (_postImages == null || !_postImages.Any())
			//{
			//	// Show message
			//	return;
			//}

			InitCreateNewPost();
		}

		public void InitCreateNewPost()
		{
			var managementService = Mvx.Resolve<IManagementService>();
			var post = new CreatePost()
			{
				Title = PostTitle,
				Description = PostDescription,
				PostImages = _postImages,
				PostCategory = (_dataModel.SelectedCategory.CategoryName == AppConstants.DefaultItem) ? AppConstants.DefaultCategoryId : _dataModel.SelectedCategory.Id,
				//PostCategory = "005ee304-800f-4247-97d7-d6a73301ca01", //For test
				Address = "d785b6e2-95c5-4d71-a2c4-1b10d064fe84",	//Da Nang
			};
			managementService.CreatePost(post);
			NavigationService.Close(this,true);
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
