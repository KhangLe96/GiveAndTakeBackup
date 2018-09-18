﻿using GiveAndTake.Core.ViewModels.Popup;
using MvvmCross.ViewModels;

namespace GiveAndTake.Core
{
	public static class AppConstants
	{
		public const string AppTitle = "Give And Take";
		public const string DefaultShortFilter = "Thời gian (mới nhất)";
		public const string DefaultLocationFilter = "Đà Nẵng";
		public const string Login = "ĐĂNG NHẬP";
		public const string ApiUrl = "http://192.168.51.137:8089/api/v1/";
		public const int ApiTimeout = 30; // seconds
		public const string GetCategories = "categories/app/list";
		public const string GetPostList = "post/app/list";
		public const string GetPostDetail = "post/app/detail";
		public const string GetPostOfUser = "post/app/listPostOfUser";
		public const string ChangeStatusOfPost = "post/app/status";
		public const string EditPost = "post/app/update";
		public const string LoginFacebook = "user/login/facebook";
		public const string CreatePost = "post/app/create";
		public const string GetUserProfile = "user";
		public const string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiOWM4NzIxMGEtM2E2ZC00MGM5LTgwMmItOGRkZWVhN2RlMDU3IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluIEFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIlVzZXIiLCJBZG1pbiJdLCJuYmYiOjE1MzY2MzM4MTYsImV4cCI6MTUzOTIyNTgxNiwiaXNzIjoiR2l2ZWF3YXkiLCJhdWQiOiJFdmVyeW9uZSJ9.2Jz4t5mnrhXSbr93gtVtSjDdI9nXB412-uwN40xd-aU";
		public const string DefaultUserName = "username";
		public const string DefaultItem = "Tất Cả";
		public const string DefaultCategoryId = "01911325-f4cc-48ac-8328-c03975ac0a3f";
		public const string GetProvinceCities = "provincecity/list";
		public const string API_KEY =
			"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiMjMwMzc5MmItZGI1MC00YzlhLTk3MjAtN2JkMWVjN2QzM2U4IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluIEFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkFkbWluIiwiVXNlciJdLCJuYmYiOjE1MzM4ODY3MzMsImV4cCI6MTUzNjQ3ODczMywiaXNzIjoiR2l2ZWF3YXkiLCJhdWQiOiJFdmVyeW9uZSJ9.2A8md3WFGYT-w2Kz0OMAUAj7e20L-wTxIfKL4KyEOko";

		public const string DefaultWarningMessage = "Chức năng này chưa xong, \n Xin vui lòng quay lại sau";
		public static string DefaultUrl = "foo";
	}
}