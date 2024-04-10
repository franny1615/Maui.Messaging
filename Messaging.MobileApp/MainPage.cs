using Messaging.MobileApp.Models;
using Messaging.MobileApp.Services;
using Messaging.MobileApp.Pages;

namespace Messaging.MobileApp;

public class MainPage : ContentPage
{
	private readonly Button SignInButton = new()
	{
		Text = "Sign In"
	};
	private readonly Entry User = new()
	{
		Placeholder = "Username"
	};

	public MainPage()
	{
		Title = "Log In";
		Content = new VerticalStackLayout
		{
			Spacing = 8,
			Padding = 8,
			Children = 
			{
				User,
				SignInButton
			}
		};

		SignInButton.Clicked += SignInButtonClicked;
	}
	~MainPage() 
	{
		SignInButton.Clicked -= SignInButtonClicked;
	}

	private async void SignInButtonClicked(object? sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(User.Text))
		{
			await DisplayAlert("Required", "Username required to proceed.", "Ok");
			return;
		}

		SessionService.Username = User.Text;
		SessionService.AuthToken = (await NetworkService.Get<Auth>("login", []))?.Token ?? "";
		if (string.IsNullOrEmpty(SessionService.AuthToken))
		{
			await DisplayAlert("Error", "Login has failed.", "Ok");
		}
		else 
		{
			await Navigation.PushAsync(new ChannelsPage());
		}		
	} 
}