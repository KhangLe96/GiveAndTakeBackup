using System.Collections.Generic;

namespace GiveAndTake.Droid
{
    internal class DroidConstants
    {
        public static Dictionary<string, int> TabNavigationIcons = new Dictionary<string, int>(){
            {"Home",Resource.Drawable.tab_navigation_icon_home},
            {"Notification",Resource.Drawable.tab_navigation_icon_notification},
            {"Conversation",Resource.Drawable.tab_navigation_icon_conversation},
            {"Profile",Resource.Drawable.tab_navigation_icon_profile},
        };
    }
}