using GiveAndTake.Core.ViewModels.Base;

namespace GiveAndTake.Core.ViewModels
{
	public class NotificationIconViewModel : BaseViewModel<int>
	{
		public int NumberOfNotificationNotSeen
		{
			get => _numberOfNotificationNotSeen;
			set => SetProperty(ref _numberOfNotificationNotSeen, value);
		}

		private int _numberOfNotificationNotSeen;

		public NotificationIconViewModel(int numberOfNotificationNotSeen)
		{
			NumberOfNotificationNotSeen = 3;
		}

		public override void Prepare(int number)
		{
			NumberOfNotificationNotSeen = number;
		}
	}
}
