using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
		List<Image> _postImages = new List<Image>();
		List<byte[]> ImageByte = new List<byte[]>();

		public string ProjectName => AppConstants.AppTitle;

		public IMvxCommand<List<byte[]>> ImageCommand { get; set; }

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
			ImageCommand = new MvxCommand<List<byte[]>>(InitCreateNewPost);
		}

		private MvxCommand _takePictureCommand;

		public System.Windows.Input.ICommand TakePictureCommand
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
			ImageByte.Add(Bytes);
			InitCreateNewPost(ImageByte);
		}

		public void InitCreateNewPost(List<byte[]> ImageByte)
		{
			for (int i = 0; i < ImageByte.Count; i++)
			{
				var image = new Image()
				{
					ImageData = ConvertToBase64String(ImageByte[i]),
				};
				_postImages.Add(image);
			}
			var managementService = Mvx.Resolve<IManagementService>();
			var post = new CreatePost()
			{
				PostTitle = "Test Upload Multiple Image",
				Description = "Successfuly! Congratulation. It's too easy. Test upload base64String API. Please make an function to convert base64 code Image -> Image to display on CMS",
				PostImages = _postImages,
				PostCategory = "029a9247-3e01-49b6-98eb-eb41f0b2ab16",
				PostAddress = "82023f0f-10e8-4b71-948e-bd6cf346ca0e",
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
