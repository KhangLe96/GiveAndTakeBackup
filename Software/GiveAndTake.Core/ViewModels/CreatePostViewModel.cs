using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.Services;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.PictureChooser;

namespace GiveAndTake.Core.ViewModels
{
	public class CreatePostViewModel : BaseViewModel
	{
		private readonly List<PostImage> _postImages = new List<PostImage>();
		private readonly List<byte[]> _imageBytes = new List<byte[]>();
		private string currentCategory;

		public string ProjectName => AppConstants.AppTitle;
		public IMvxCommand<List<byte[]>> ImageCommand { get; set; }

		private ICommand _submitCommand;
		public ICommand SubmitCommand => _submitCommand ?? (_submitCommand = new MvxCommand(InitSubmit));

		public IMvxAsyncCommand ShowCategoriesCommand { get; set; }

		private readonly IMvxPictureChooserTask _pictureChooserTask;

		private string _postDescription;
		private string _postTitle;
		private byte[] _bytes;

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

		public CreatePostViewModel(IMvxPictureChooserTask pictureChooserTask)
		{
			_pictureChooserTask = pictureChooserTask;
			InitCommand();
		}

		private void InitCommand()
		{
			ImageCommand = new MvxCommand<List<byte[]>>(InitNewImage);
			ShowCategoriesCommand = new MvxAsyncCommand(ShowCategories);
		}

		private async Task<string> ShowCategories()
		{
			if (currentCategory == null)
			{
				currentCategory = AppConstants.DefaultCategory;
			}
			var category = await NavigationService.Navigate<PopupCategoriesViewModel, string, string>(currentCategory);
			if (category != null)
			{
				currentCategory = category;
			}
			return category;
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
			_pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
		}

		private void OnPicture(Stream pictureStream)
		{
			var memoryStream = new MemoryStream();
			pictureStream.CopyTo(memoryStream);
			Bytes = memoryStream.ToArray();
			_imageBytes.Add(Bytes);
			InitNewImage(_imageBytes);
		}

		public void InitNewImage(List<byte[]> imageByte)
		{
			for (int i = 0; i < imageByte.Count; i++)
			{
				var image = new PostImage()
				{
					ImageData = ConvertToBase64String(imageByte[i]),
				};
				_postImages.Add(image);
			}
		}

		public void InitSubmit()
		{
			if (_postImages == null || !_postImages.Any())
			{
				// Show message
				return;
			}

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
				PostCategory = "07187ea7-da4a-4d9d-9131-dcf5cac006d6",
				Address = "d785b6e2-95c5-4d71-a2c4-1b10d064fe84",
			};
			managementService.CreatePost(post);
		}

		public string ConvertToBase64String(byte[] imageByte)
		{
			string result = Convert.ToBase64String(imageByte);
			return result;
		}

	}
}
