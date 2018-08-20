using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private List<PostItemViewModel> postViewModels;

        public List<PostItemViewModel> PostViewModels
        {
            get => postViewModels;
            set
            {
                postViewModels = value;
                RaisePropertyChanged(() => PostViewModels);
            }
        }

        public IMvxAsyncCommand ShowFilterCommand { get; set; }
        public IMvxAsyncCommand ShowShortPostCommand { get; set; }
        public IMvxAsyncCommand ShowCategoriesCommand { get; set; }

        public HomeViewModel()
        {
            var posts = InitPosts();
            PostViewModels = posts.Select(post => new PostItemViewModel(post)).ToList();
        }
      
        private static List<Post> InitPosts()
        {
            return new List<Post>
            {
                new Post
                {
                    Category = new Category {CategoryName = "Sách"},
                    User = new User
                    {
                        FirstName = "Thảo", LastName = "Nguyễn",
                        AvatarUrl = "https://hd1.hotdeal.vn/images/uploads/2015/09/02/176800/176800-caphe-cung-tony-body%20%281%29.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                },
                new Post
                {
                    Category = new Category {CategoryName = "Quần áo"},
                    User = new User
                    {
                        FirstName = "Lâm Nguyễn",
                        AvatarUrl =
                            "https://hd1.hotdeal.vn/images/uploads/2015/09/02/176800/176800-caphe-cung-tony-body%20%281%29.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                },
                new Post
                {
                    Category = new Category {CategoryName = "Sách"},
                    User = new User
                    {
                        FirstName = "Trân Đặng",
                        AvatarUrl =
                            "http://media.baoduhoc.vn/data/mckfinder/khanhngoc/images/2016/04/08/SDuXiGUlwZUl5EI.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                },
                new Post
                {
                    Category = new Category {CategoryName = "Quần áo"},
                    User = new User
                    {
                        FirstName = "Quốc Trần",
                        AvatarUrl = "http://media.baoduhoc.vn/data/mckfinder/khanhngoc/images/2016/04/08/SDuXiGUlwZUl5EI.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                },
                new Post
                {
                    Category = new Category {CategoryName = "Sách"},
                    User = new User
                    {
                        FirstName = "Tài Võ",
                        AvatarUrl =
                            "http://media.baoduhoc.vn/data/mckfinder/khanhngoc/images/2016/04/08/SDuXiGUlwZUl5EI.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                },
                new Post
                {
                    Category = new Category {CategoryName = "Sách"},
                    User = new User
                    {
                        FirstName = "Thảo", LastName = "Nguyễn",
                        AvatarUrl = "https://hd1.hotdeal.vn/images/uploads/2015/09/02/176800/176800-caphe-cung-tony-body%20%281%29.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                },
                new Post
                {
                    Category = new Category {CategoryName = "Quần áo"},
                    User = new User
                    {
                        FirstName = "Lâm Nguyễn",
                        AvatarUrl =
                            "https://hd1.hotdeal.vn/images/uploads/2015/09/02/176800/176800-caphe-cung-tony-body%20%281%29.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                },
                new Post
                {
                    Category = new Category {CategoryName = "Sách"},
                    User = new User
                    {
                        FirstName = "Trân Đặng",
                        AvatarUrl =
                            "http://media.baoduhoc.vn/data/mckfinder/khanhngoc/images/2016/04/08/SDuXiGUlwZUl5EI.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                },
                new Post
                {
                    Category = new Category {CategoryName = "Quần áo"},
                    User = new User
                    {
                        FirstName = "Quốc Trần",
                        AvatarUrl = "http://media.baoduhoc.vn/data/mckfinder/khanhngoc/images/2016/04/08/SDuXiGUlwZUl5EI.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                },
                new Post
                {
                    Category = new Category {CategoryName = "Sách"},
                    User = new User
                    {
                        FirstName = "Tài Võ",
                        AvatarUrl =
                            "http://media.baoduhoc.vn/data/mckfinder/khanhngoc/images/2016/04/08/SDuXiGUlwZUl5EI.jpg"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này",
                    AppreciationCount = 15,
                    CommentCount = 20,
                    RequestCount = 25
                }
            };
        }
    }
}
