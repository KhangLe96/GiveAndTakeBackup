using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveAndTake.Core.ViewModels
{

    public class HomeViewModel : BaseViewModel
    {
        public string ProjectName => "Give And Take";

        private MvxObservableCollection<Post> posts;

        public MvxObservableCollection<Post> Posts
        {
            get => posts;
            set => SetProperty(ref posts, value);
        }

        public override Task Initialize()
        {
            //try
            //{
            //    var client = new RestClient("http://192.168.51.126:8090/api/v1/Post/app/list");
            //    var request = new RestRequest(Method.GET);
            //    var response = client.Execute<PostResponse>(request);
            //    Posts = new MvxObservableCollection<Post>(response.Data.Results);
            //}
            //catch (Exception e)
            //{
            //    // get post error, finish current screen and back to main screen
            //}
            Posts = new MvxObservableCollection<Post>(new List<Post>
            {
                new Post
                {
                    Category = new Category {CategoryName = "Sách"},
                    User = new User {UserName = "Thảo Nguyễn"},
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này"
                },
                new Post
                {
                    Category = new Category {CategoryName = "Quần áo"},
                    User = new User
                    {
                        UserName = "Lâm Nguyễn",
                        AvatarUrl =
                            "https://scontent.fdad1-1.fna.fbcdn.net/v/t1.0-9/19511582_1793393650676860_5395904968303248523_n.jpg?_nc_cat=0&oh=198c9fa12ea19047435590c9ee2f9f77&oe=5C02BC92"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này"
                },
                new Post
                {
                    Category = new Category {CategoryName = "Sách"},
                    User = new User
                    {
                        UserName = "Trân Đặng",
                        AvatarUrl =
                            "https://scontent.fdad1-1.fna.fbcdn.net/v/t1.0-9/34135789_1922622551102029_5344616745766223872_n.jpg?_nc_cat=0&oh=afc2a0b84f10d052a1781da154d1ecb9&oe=5C0B6F64"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này"
                },
                new Post
                {
                    Category = new Category {CategoryName = "Quần áo"},
                    User = new User
                    {
                        UserName = "Quốc Trần",
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này"
                },
                new Post
                {
                    Category = new Category {CategoryName = "Sách"},
                    User = new User
                    {
                        UserName = "Tài Võ",
                        AvatarUrl =
                            "https://scontent.fdad1-1.fna.fbcdn.net/v/t1.0-9/22281885_1470337016381130_627715758415259125_n.jpg?_nc_cat=0&oh=b373dd7f26a168e2f7cab794cdfa5f19&oe=5BFF19EF"
                    },
                    ProvinceCity = new ProvinceCity {ProvinceCityName = "Đà Nẵng"},
                    Description =
                        "Đi qua những rung rinh, những xao xuyến nhẹ, cũng thấm thía cảm giác ngậm ngùi nuối tiếc cho những mối tình không dám nói thành lời nên giờ thì cho những xúc cảm ấy được cất thành âm nhạc ha 🎶\r\nLời từ trái tim chân thành nên nói chung hơi bị tâm đắc với sản phẩm lần này"
                }
            });
            return base.Initialize();
        }

        public HomeViewModel()
        {
        }
    }
}
