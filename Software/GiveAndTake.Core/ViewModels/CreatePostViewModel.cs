using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.Commands;
using MvvmCross.Plugin.PictureChooser;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Services;
namespace GiveAndTake.Core.ViewModels
{
	public class CreatePostViewModel : BaseViewModel<ViewMode, bool>
	{
		private readonly IDataModel _dataModel;
		private readonly IMvxPictureChooserTask _pictureChooserTask;

		private MvxCommand _takePictureCommand;
		private ICommand _submitCommand;
		private IMvxAsyncCommand _showPhotoCollectionCommand;
		private IMvxCommand _showCategoriesCommand;
		private IMvxCommand _showProvinceCityCommand;
		private IMvxCommand _backPressedCommand;
		private IMvxCommand<List<byte[]>> _imageCommand;
		private readonly ILoadingOverlayService _overlay;

		private readonly DebouncerHelper _debouncer;
		private string _postDescription;
		private string _postTitle;
		private string _category = AppConstants.DefaultCategoryCreatePostName;
		private string _provinceCity = AppConstants.DefaultLocationFilter;
		private byte[] _bytes;
		private string _selectedImage;
		private Category _selectedCategory;
		private ProvinceCity _selectedProvinceCity;
		private List<PostImage> _postImages = new List<PostImage>();
		private bool _enableSelectedImage;
		private string _postId;
		private ViewMode _viewModelMode;
		private bool _isSubmitBtnEnabled;

		public ICommand SubmitCommand => _submitCommand ?? (_submitCommand = new MvxAsyncCommand(InitSubmit));
		public IMvxAsyncCommand ShowPhotoCollectionCommand =>
			_showPhotoCollectionCommand ?? (_showPhotoCollectionCommand = new MvxAsyncCommand(ShowPhotoCollection));
		public IMvxCommand ShowCategoriesCommand =>
			_showCategoriesCommand ?? (_showCategoriesCommand = new MvxCommand(ShowCategoriesPopup));
		public IMvxCommand ShowProvinceCityCommand =>
			_showProvinceCityCommand ?? (_showProvinceCityCommand = new MvxCommand(ShowLocationFiltersPopup));
		public IMvxCommand BackPressedCommand =>
			_backPressedCommand ?? (_backPressedCommand = new MvxCommand(BackPressed));
		public IMvxCommand<List<byte[]>> ImageCommand =>
			_imageCommand ?? (_imageCommand = new MvxCommand<List<byte[]>>(InitNewImage));

		public string PostDescription
		{
			get => _postDescription;
			set
			{
				SetProperty(ref _postDescription, value);
				UpdateSubmitBtn();
			}
		}

		public string PostTitle
		{
			get => _postTitle;
			set
			{
				SetProperty(ref _postTitle, value);
				UpdateSubmitBtn();
			}
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

		public bool EnableSelectedImage
		{
			get => _enableSelectedImage;
			set => SetProperty(ref _enableSelectedImage, value);
		}

		public List<PostImage> PostImages
		{
			get => _postImages;
			set
			{
				_postImages = value;
				EnableSelectedImage = (_postImages.Count != 0);
				InitSelectedImage();
			}
		}

		public bool IsSubmitBtnEnabled
		{
			get => _isSubmitBtnEnabled;
			set => SetProperty(ref _isSubmitBtnEnabled, value);
		}


		public string PostDescriptionPlaceHolder { get; set; } = AppConstants.CreatePostDescriptionPlaceHolder;
		public string PostTitlePlaceHolder { get; set; } = AppConstants.CreatePostTitlePlaceHolder;
		public string BtnSubmitTitle { get; set; } = AppConstants.CreatePostBtnSubmitTitle;
		public string BtnCancelTitle { get; set; } = AppConstants.CancelTitle;

		public CreatePostViewModel(IMvxPictureChooserTask pictureChooserTask, IDataModel dataModel, ILoadingOverlayService loadingOverlayService)
		{
			_overlay = loadingOverlayService;
			_debouncer = new DebouncerHelper();
			_dataModel = dataModel;
			_pictureChooserTask = pictureChooserTask;
			_selectedCategory = _dataModel.Categories.FirstOrDefault(category => category.CategoryName == AppConstants.DefaultCategoryCreatePostName);
			_selectedProvinceCity = _selectedProvinceCity ?? _dataModel.ProvinceCities.First(p => p.ProvinceCityName == AppConstants.DefaultLocationFilter);
		}

		private async void BackPressed()
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

			if (string.IsNullOrEmpty(result))
			{
				return;
			}

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

			if (string.IsNullOrEmpty(result))
			{
				return;
			}

			_selectedProvinceCity = _dataModel.ProvinceCities.First(c => c.ProvinceCityName == result);
			ProvinceCity = result;
		}

		private async Task ShowPhotoCollection()
		{
			PostImages = await NavigationService.Navigate<PhotoCollectionViewModel, List<PostImage>, List<PostImage>>(PostImages);
		}

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
					ImageData = JsonHelper.ConvertToBase64String(img),
				};
				PostImages.Add(image);
			}

			EnableSelectedImage = (_postImages.Count != 0);
			InitSelectedImage();
		}

		public async Task InitSubmit()
		{
			bool success = false;
			try
			{
				if (_viewModelMode == ViewMode.EditPost)
				{
					InitEditPost();
				}
				else
				{
					InitCreateNewPost();
				}
				success = true;
			}
			catch (AppException.ApiException)
			{
				await NavigationService.Navigate<PopupWarningViewModel, string, bool>(AppConstants
					.ErrorConnectionMessage);
			}
			finally
			{
				await _overlay.CloseOverlay(777);
				if (success)
				{
					await NavigationService.Close(this, true);
				}
			}
		}

		public async void InitCreateNewPost()
		{
			await _overlay.ShowOverlay(AppConstants.UploadDataOverLayTitle);
			var post = new CreatePost()
			{
				Title = PostTitle,
				Description = PostDescription,
				PostImages = _postImages,
				PostCategory = (_selectedCategory.CategoryName == AppConstants.DefaultCategoryCreatePostName)
					? AppConstants.DefaultCategoryCreatePostId
					: _selectedCategory.Id,
				Address = _selectedProvinceCity.Id,
			};
			await ManagementService.CreatePost(post, _dataModel.LoginResponse.Token);
		}

		public async void InitEditPost()
		{
			var post = new EditPost()
			{
				PostId = _postId,
				Title = PostTitle,
				Description = PostDescription,
				PostImages = _postImages,
				PostCategory = (_selectedCategory.CategoryName == AppConstants.DefaultCategoryCreatePostName) ? AppConstants.DefaultCategoryCreatePostId : _selectedCategory.Id,
				Address = _selectedProvinceCity.Id,
			};
			await ManagementService.EditPost(post, _postId, _dataModel.LoginResponse.Token);
		}

		private void InitSelectedImage()
		{
			SelectedImage = $"Đã chọn {PostImages.Count} hình";
		}

		public void UpdateSubmitBtn() => IsSubmitBtnEnabled = !string.IsNullOrEmpty(_postTitle) && !string.IsNullOrEmpty(_postDescription);

		public byte[] getImageFromUrl(string url)
		{
			System.Net.HttpWebRequest request = null;
			System.Net.HttpWebResponse response = null;
			byte[] b = null;

			request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
			response = (System.Net.HttpWebResponse)request.GetResponse();

			if (request.HaveResponse)
			{
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					Stream receiveStream = response.GetResponseStream();
					using (BinaryReader br = new BinaryReader(receiveStream))
					{
						b = br.ReadBytes(500000);
						br.Close();
					}
				}
			}

			return b;
		}

		public override void Prepare(ViewMode mode)
		{
			if (mode != ViewMode.EditPost) return;
			_viewModelMode = ViewMode.EditPost;
			_postId = _dataModel.CurrentPost.PostId;
			_selectedCategory.CategoryName = _dataModel.CurrentPost.Category.CategoryName;
			_selectedCategory.Id = _dataModel.CurrentPost.Category.Id;
			_selectedProvinceCity.ProvinceCityName = _dataModel.CurrentPost.ProvinceCity.ProvinceCityName;
			_selectedProvinceCity.Id = _dataModel.CurrentPost.ProvinceCity.Id;
			_postTitle = _dataModel.CurrentPost.Title;
			_postDescription = _dataModel.CurrentPost.Description;
			EditImageList();
			IsSubmitBtnEnabled = true;

			BtnSubmitTitle = AppConstants.SaveAPost;
		}

		private void EditImageList()
		{
			var imageList = _dataModel.CurrentPost.Images;
			foreach (var img in imageList)
			{
				var urlInBytes = getImageFromUrl(img.OriginalImage);
				var image = new PostImage()
				{

					ImageData = JsonHelper.ConvertToBase64String(urlInBytes),
				};
				PostImages.Add(image);

				EnableSelectedImage = (_postImages.Count != 0);
				InitSelectedImage();
			}
		}
	}
}
