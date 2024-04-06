using System.Collections.ObjectModel;
using Messaging.MobileApp.Models;
using Messaging.MobileApp.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls.Shapes;

namespace Messaging.MobileApp;

public class ChatsPage : ContentPage
{
	public ObservableCollection<ChatMessage> AllMessages { get; set; } = [];

	private readonly HubConnection _HubConnection;
	private readonly Button _Send = new()
	{
		WidthRequest = 70,
		Text = "Send"
	};
	private readonly Entry _Message = new()
	{
		Placeholder = "Type your message"
	};
	private readonly CollectionView _MessageCollection = new();
	private bool _Connected = false;
	private readonly Grid _ContentLayout = new()
	{
		Padding = 8,
		RowDefinitions = 
		{
			new RowDefinition { Height = GridLength.Star },
			new RowDefinition { Height = GridLength.Auto }
		},
	};

	public ChatsPage()
	{
		_HubConnection = new HubConnectionBuilder()
			.WithUrl($"{SessionService.APIUrl}/chat")
			.Build();

		_MessageCollection.BindingContext = this;
		_MessageCollection.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
		{
			ItemSpacing = 8
		};
		_MessageCollection.ItemTemplate = new DataTemplate(() => 
		{
			var userLabel = new Label()
			{
				FontSize = 12,
				FontAttributes = FontAttributes.Italic,
				HorizontalTextAlignment = TextAlignment.Start,
				MaxLines = 1
			};
			var messageLabel = new Label()
			{
				FontSize = 16,
				HorizontalTextAlignment = TextAlignment.Start,
			};
			var timeLabel = new Label()
			{
				FontSize = 12,
				FontAttributes = FontAttributes.Italic,
				HorizontalTextAlignment = TextAlignment.End,
				MaxLines = 1
			};
			var border = new Border
			{
				Padding = 8,
				Content = new VerticalStackLayout
				{
					Spacing = 4,
					Children = 
					{
						userLabel,
						messageLabel,
						timeLabel,
					}
				},
				StrokeThickness = 2,
				Stroke = Colors.DarkGray
			};
			border.SetBinding(BindingContextProperty, ".");
			border.SetBinding(Border.MarginProperty, "Margin");
			border.SetBinding(Border.StrokeShapeProperty, "BubbleShape");
			userLabel.SetBinding(Label.TextProperty, "User");
			messageLabel.SetBinding(Label.TextProperty, "Message");
			timeLabel.SetBinding(Label.TextProperty, "SentOnStr");

			return new Grid 
			{
				Children = { border }
			};
		});
		_MessageCollection.SetBinding(CollectionView.ItemsSourceProperty, "AllMessages");

		var msgGrid = new Grid 
		{
			ColumnSpacing = 8,
			ColumnDefinitions = 
			{
				new ColumnDefinition { Width = GridLength.Star },
				new ColumnDefinition { Width = GridLength.Auto }
			}
		};
		msgGrid.Add(_Message, 0, 0);
		msgGrid.Add(_Send, 1, 0);
		
		_ContentLayout.Add(_MessageCollection, 0, 0);
		_ContentLayout.Add(msgGrid, 0, 1);
		Content = _ContentLayout;

		_HubConnection.On<string, string, DateTime>("ReceiveMessage", (user, message, sentOn) =>
		{
			AllMessages.Add(new ChatMessage
			{
				User = user,
				Message = message,
				SentOn = sentOn
			});
		});

		_Send.Clicked += Send_Clicked;
	}
	~ChatsPage()
	{
		_Send.Clicked -= Send_Clicked;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		ToggleConnect();
		FetchPreviousMessages();
    }

	protected override void OnDisappearing()
	{
		ToggleConnect();
		base.OnDisappearing();
	}

    private async void ToggleConnect()
	{
		if (!_Connected)
		{
			await _HubConnection.StartAsync();
			_Send.BackgroundColor = Colors.Green;
			_Connected = true;
		}
		else 
		{
			await _HubConnection.StopAsync();
			_Send.BackgroundColor = Colors.Red;
			_Connected = false;
		}
	}

	private async void FetchPreviousMessages()
	{
		AllMessages.Clear();
		var messages = (await NetworkService.Get<List<ChatMessage>>("messages/all", [])) ?? [];
		foreach(var message in messages)
		{
			AllMessages.Add(message);
		}
	}

	private async void Send_Clicked(object? sender, EventArgs e)
	{	
		if (!_Connected)
		{
			await DisplayAlert("Not Connected", "", "OK");
			return;
		}

		try 
		{
			await _HubConnection.InvokeAsync("SendMessage", SessionService.Username, _Message.Text);
			_Message.Text = "";
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.WriteLine(ex.Message);
		}
	}
}