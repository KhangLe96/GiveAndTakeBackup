using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Nio.Channels;
using Java.Nio.Channels.Spi;

namespace GiveAndTake.Droid.Controls
{
	class CustomProfileItem:Selector
	{
		public override void Close()
		{
			throw new NotImplementedException();
		}

		public override ICollection<SelectionKey> Keys()
		{
			throw new NotImplementedException();
		}

		public override SelectorProvider Provider()
		{
			throw new NotImplementedException();
		}

		public override int Select()
		{
			throw new NotImplementedException();
		}

		public override int Select(long timeout)
		{
			throw new NotImplementedException();
		}

		public override ICollection<SelectionKey> SelectedKeys()
		{
			throw new NotImplementedException();
		}

		public override int SelectNow()
		{
			throw new NotImplementedException();
		}

		public override Selector Wakeup()
		{
			throw new NotImplementedException();
		}

		public override bool IsOpen { get; }
	}
}