using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Shared.Services
{
    public interface INavigationService
    {
        List<NavMenuItem> GetMenuItems();
        string ApplicationName { get; }
        string ApplicationType { get; } // "Admin", "Web", "Mobile"
        bool ShowMobileFooter { get; }
    }

    public class NavMenuItem
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public List<NavMenuItem> Children { get; set; } = new List<NavMenuItem>();
        public bool RequiresAuthentication { get; set; } = true;
        public string[] ApplicationTypes { get; set; } = new[] { "Admin", "Web", "Mobile" }; // Which apps show this item
    }
}
