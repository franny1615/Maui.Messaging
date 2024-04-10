namespace Messaging.MobileApp.Pages;

public class ChannelsPage : ContentPage
{
	private readonly Button _ChannelOne = new Button()
	{
		Text = "Channel One"
	};
	private readonly Button _ChannelTwo = new Button()
	{
		Text = "Channel Two"
	};
	private readonly Button _ChannelThree = new Button()
	{
		Text = "Channel Three"
	};

	public ChannelsPage()
	{
		Content = new VerticalStackLayout
		{
			Spacing = 16,
			Padding = 8,
			Children = 
			{
				_ChannelOne,
				_ChannelTwo,
				_ChannelThree
			}
		};

		_ChannelOne.Clicked += ChannelOne;
		_ChannelTwo.Clicked += ChannelTwo;
		_ChannelThree.Clicked += ChannelThree;
	}
	~ChannelsPage()
	{
		_ChannelOne.Clicked -= ChannelOne;
		_ChannelTwo.Clicked -= ChannelTwo;
		_ChannelThree.Clicked -= ChannelThree;
	}

	private void ChannelOne(object? sender, EventArgs e)
	{
		Navigation.PushAsync(new ChatPage(1));
	}

	private void ChannelTwo(object? sender, EventArgs e)
	{
		Navigation.PushAsync(new ChatPage(2));
	}

	private void ChannelThree(object? sender, EventArgs e)
	{
		Navigation.PushAsync(new ChatPage(3));
	}
}