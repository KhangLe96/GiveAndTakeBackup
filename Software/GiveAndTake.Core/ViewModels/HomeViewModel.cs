using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;
using GiveAndTake.Core.ViewModels.Base;
using MvvmCross.ViewModels;
using RestSharp;

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
            try
            {
                var client = new RestClient("http://192.168.51.126:8090/api/v1/Post/app/list");
                var request = new RestRequest(Method.GET);
                var response = client.Execute<PostResponse>(request);
                Posts = new MvxObservableCollection<Post>(response.Data.Results);
            }
            catch (Exception e)
            {
                // get post error, finish current screen and back to main screen
            }
            return base.Initialize();
        }

        public HomeViewModel()
        {
        }
    }
}
