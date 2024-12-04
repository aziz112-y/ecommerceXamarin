using Xamarin.Essentials;
using Xamarin.Forms;

namespace e_comm
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            string userRole = Preferences.Get("UserRole", "USER"); 

            if (userRole == "USER")
            {
                gestionStoreTab.IsVisible = false;
            }
            else if (userRole == "ADMIN")
            {
                gestionStoreTab.IsVisible = true;
            }
        }
    }
}