using System.Collections.Generic;

namespace GiveAndTake.Core
{
	public static class AppConstants
	{
		public const string AppTitle = "Cho và Nhận";
		public const string DefaultLocationFilter = "Đà Nẵng";
		public const string ApiUrl = "https://api.chovanhan.asia/api/v1/";
		public const int ApiTimeout = 30; // seconds
		public const string GetCategories = "categories/app/list";
		public const string GetPostList = "post/app/list";
		public const string GetPostDetail = "post/app/detail";
		public const string GetPostOfUser = "post/app/listPostOfUser";
		public const string ChangeStatusOfPost = "post/app/status";
		public const string EditPost = "post/app/update";
		public const string LoginFacebook = "user/login/facebook";
		public const string CreatePost = "post/app/create";
		public const string CreateRequest = "request/create";
		public const string CheckUserRequest = "request/checkUserRequest";
		public const string CancelUserRequest = "request/deleteCurrentUserRequest";
		public const string GetUserProfile = "user";
		public const string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiOWM4NzIxMGEtM2E2ZC00MGM5LTgwMmItOGRkZWVhN2RlMDU3IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluIEFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIlVzZXIiLCJBZG1pbiJdLCJuYmYiOjE1MzY2MzM4MTYsImV4cCI6MTUzOTIyNTgxNiwiaXNzIjoiR2l2ZWF3YXkiLCJhdWQiOiJFdmVyeW9uZSJ9.2Jz4t5mnrhXSbr93gtVtSjDdI9nXB412-uwN40xd-aU";
		public const string DefaultUserName = "username";
		public const string DefaultCategoryCreatePostName = "Văn phòng phẩm";
		public const string DefaultCategoryCreatePostId = "0c109358-fae2-42bd-b2f3-45cbe98a5dbd";
		public const string GetProvinceCities = "provincecity/list";
	    public const string GetRequestOfPost = "request/list";
	    public const string ChangeStatusOfRequest = "request/status";

        public const string API_KEY =
			"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiMjMwMzc5MmItZGI1MC00YzlhLTk3MjAtN2JkMWVjN2QzM2U4IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluIEFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkFkbWluIiwiVXNlciJdLCJuYmYiOjE1MzM4ODY3MzMsImV4cCI6MTUzNjQ3ODczMywiaXNzIjoiR2l2ZWF3YXkiLCJhdWQiOiJFdmVyeW9uZSJ9.2A8md3WFGYT-w2Kz0OMAUAj7e20L-wTxIfKL4KyEOko";

		public const string DefaultWarningMessage = "Chức năng này chưa xong, \n Xin vui lòng quay lại sau";
		public const string ErrorMessage = "Đã có lỗi xảy ra, \n Xin vui lòng thử lại sau";
		public const string HomeTab = "Home";
		public const string NotificationTab = "Notification";
		public const string ConversationTab = "Conversation";
		public const string ProfileTab = "Profile";
		public static string SubmitTitle = "Xác nhận";
		public const string PopupCategoriesTitle = "Phân loại";
		public const string PopupSortFiltersTitle = "Xếp theo";
		public const string PopupLocationFiltersTitle = "Lọc theo";

		public const string ChangePostStatus = "Chuyển trạng thái sang \"Đã cho\"";
		public const string ModifyPost = "Chỉnh sửa";
		public const string ViewPostRequests = "Duyệt cho";
		public const string DeletePost = "Xóa";
		public const string ReportPost = "Báo cáo";
		public static List<string> MyPostOptions = new List<string> {ChangePostStatus, ModifyPost, ViewPostRequests, DeletePost};
		public static List<string> OtherPostOptions = new List<string> { ReportPost };

		public static string GivingStatus = "Đang cho";
		public static string GivedStatus = "Đã cho";
		public const string ErrorConnectionMessage = "Lỗi kết nối mạng, \n Xin vui lòng thử lại";
		public static string CancelTitle = "Hủy";
		public static string Done = "Xong";
		public static string DeleteConfirmationMessage = "Bạn có muốn xóa bài viết này?";
		public static string SearchResultNullTitle = "Không tìm thấy kết quả nào";
		public static string SelectedImage = "Đã chọn 0 hình";
		public static string RequestRejectingMessage = "Bạn có chắc chắn từ chối yêu cầu?";
		public static string PopupRequestDetailTitle = "Chi tiết yêu cầu";
		public static string ButtonRejectTitle = "Từ chối";
		public static string ButtonAcceptTitle = "Chấp nhận";
		public static string LoadingDataOverlayTitle= "Đang tải dữ liệu";
		public static string UploadDataOverLayTitle = "Đang đăng bài";
		public static string UpdateOverLayTitle = "Đang cập nhật";
	}
}