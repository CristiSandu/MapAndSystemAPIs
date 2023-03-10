using MapAPIs.Views;

namespace MapAPIs;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(InfoPage), typeof(InfoPage));
    }
}
