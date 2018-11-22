namespace GiveAndTake.Core
{
	// REVIEW [KHOA]: all texts should get from locale files -> support multi-language later if needed
	public static class AppConstants
	{
		public const string AppTitle = "Cho và Nhận";
		public const string DefaultLocationFilter = "Đà Nẵng";
		public const string ApiUrl = "https://api.chovanhan.asia/api/v1/";
		public const int ApiTimeout = 300; // seconds
		public const string GetCategories = "categories/app/list";
		public const string GetPostList = "post/app/list";
		public const string GetMyPostList = "post/app/listPostOfUser";
		public const string GetPostDetail = "post/app/detail";
		public const string GetPostOfUser = "post/app/listPostOfUser";
		public const string GetMyRequestedPosts = "post/app/listRequestedPostOfUser";
		public const string ChangeStatusOfPost = "post/status";
		public const string EditPost = "post/app/update";
		public const string LoginFacebook = "user/login/facebook";
		public const string LogoutApp = "user/logout";
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
		public const string GetRequestById = "request/getRequestById";
		public const string ChangeStatusOfRequest = "request/status";
		public const string CheckIfRequestProcessed = "request/checkIfRequestProcessed";
		public const string GetNotificationList = "notification/list";
		public const string UpdateReadStatus = "notification/updateReadStatus";

		public const string ApiKey =
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

		public const string ChangePostStatus = "Chuyển trạng thái sang";
		public const string ModifyPost = "Chỉnh sửa";
		public const string ViewPostRequests = "Duyệt cho";
		public const string DeletePost = "Xóa";
		public const string ReportPost = "Báo cáo";

		public static string GivingStatus = "Đang cho";
		public static string GivedStatus = "Đã cho";
		public static string GivingStatusEN = "Giving";
		public static string GivedStatusEN = "Gave";
		public const string ErrorConnectionMessage = "Lỗi kết nối mạng, \n Xin vui lòng thử lại";
		public static string CancelTitle = "Hủy";
		public static string Done = "Xong";
		public static string SelectedImage = "Đã chọn 0 hình";
		public static string RequestRejectingMessage = "Bạn có chắc chắn từ chối yêu cầu?";
		public static string PopupRequestDetailTitle = "Chi tiết yêu cầu";
		public static string ButtonRejectTitle = "Từ chối";
		public static string ButtonAcceptTitle = "Chấp nhận";
		public static string CreateResponse = "response/create";

		public static string RequestApprovalTitle = "Thông tin trao đổi";
		public static string RequestReceiver= "Gửi đến: ";
		public static string InformationPlaceHolder = "Thông tin trao đổi ...";
		public static string ButtonSendTitle = "Gửi";

		public static string RankTitle = "Xếp hạng";
		public static string SentTitle = "Đã cho";
		public static string MyPostsTitle = "Bài đăng của tôi";
		public static string MyRequestsTitle = "Yêu cầu đã gửi";
		public static string Member = "Thành viên";
		public static string Times = "Lần";
		public static string DeleteConfirmationMessage = "Bạn có muốn xóa bài viết này?";
		public static string SearchResultNullTitle = "Không tìm thấy kết quả nào";
		public const string CreatePostDescriptionPlaceHolder = "Mô tả (Nhãn hiệu, kiểu dáng, màu sắc, ... )";
		public const string CreatePostTitlePlaceHolder = "Tiêu đề (Thương hiệu, thể loại, ...)";
		public const string CreatePostBtnSubmitTitle = "Đăng";
		public const string CreatePostSelectedImageWarning = "Bạn hãy chọn một ảnh bất kỳ để vào xem những ảnh đã chọn";
		public static string LoginTitle = "Đăng nhập với tài khoản";
		public static string LoadingDataOverlayTitle= "Đang tải dữ liệu";
		public static string UploadDataOverLayTitle = "Đang đăng bài";
		public static string UpdateOverLayTitle = "Đang cập nhật";
		public static string SuccessfulAcceptanceMessage = "Bạn đã chấp nhận thành công!";
		public static string SuccessfulRejectionMessage = "Bạn đã từ chối thành công!";

		public static int NumberOfRequestPerPage = 20;
		public static string SaveAPost = "Lưu";
		public static string LoginProcessOverLayTitle = "Tiến hành đăng nhập";
		public static int NumOfFragmentViewPager = 3;
		public static string ProcessingDataOverLayTitle = "Đang xử lý";
		public static string ConfirmDeletePost = "Bài đăng đã có yêu cầu nhận, \nbạn có chắc chắn muốn xóa ?";
		public static string ConfirmDeleteOwnRequest = "Bạn có chắc chắn muốn hủy bỏ yêu cầu ?";

		public const string Rename = "Sửa tên";
		public const string ChangeAvatar = "Đổi ảnh đại diện";
		public const string SendFeedback = "Gửi phản hồi";
		public const string LogOut = "Đăng xuất";
		public const int ProfileTabIndex = 3;
	}
}