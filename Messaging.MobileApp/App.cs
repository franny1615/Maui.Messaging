using Messaging.MobileApp.Services;

namespace Messaging.MobileApp;

public partial class App : Application
{
	public App()
	{
		Resources.MergedDictionaries.Add(new Resources.Styles.Colors());
        Resources.MergedDictionaries.Add(new Resources.Styles.Styles());
		
		SessionService.APIUrl = "http://127.0.0.1:7044";

		MainPage = new NavigationPage(new MainPage());
	}
}
