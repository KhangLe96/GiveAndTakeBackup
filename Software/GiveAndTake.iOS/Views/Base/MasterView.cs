using GiveAndTake.Core.ViewModels.Base;
using GiveAndTake.iOS.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using UIKit;

namespace GiveAndTake.iOS.Views.Base
{
	[MvxRootPresentation]
	public class MasterView : BaseView
	{
		private UILabel _lbProjectName;

		protected override void InitView()
		{
			_lbProjectName = UIHelper.CreateLabel(UIColor.Blue, DimensionHelper.MediumTextSize);

			View.Add(_lbProjectName);

			View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(_lbProjectName, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(_lbProjectName, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0)
			});

			//var avatar = new MvxCachedImageView()
			//{
			//	TranslatesAutoresizingMaskIntoConstraints = false,
			//	ImagePath = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
			//};

			//avatar.AddConstraints(new []
			//{
			//	NSLayoutConstraint.Create(avatar, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 100),
			//	NSLayoutConstraint.Create(avatar, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 100)
			//});

			//View.Add(avatar);

			//View.AddConstraints(new []
			//{
			//	NSLayoutConstraint.Create(avatar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _lbProjectName, NSLayoutAttribute.Bottom, 1, 0),
			//	NSLayoutConstraint.Create(avatar, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0)
			//});
		}

		protected override void CreateBinding()

		{
			base.CreateBinding();
			var set = this.CreateBindingSet<MasterView, MasterViewModel>();

			set.Bind(_lbProjectName)
				.For(v => v.Text)
				.To(vm => vm.ProjectName);

			set.Apply();
		}
	}
}