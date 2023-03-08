using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VkToTgRetranslator.Models.Tg;

namespace VkToTgRetranslator.Engines
{
	class TelegramEngine
	{
		static private TelegramBotClient _bot;
		static private ReceiverOptions _tgReceiverOptions =
			new ReceiverOptions
			{
				AllowedUpdates = Array.Empty<UpdateType>() // all update types
			};
		static int _lastUpdateId;
		static DateTime InitUtcDateTime = DateTime.UtcNow;

		private VkEngine _vkEngine;
		private AppDbContext _dbContext;

		private Timer _msgsCheckingTimer;

		public TelegramEngine(VkEngine vkEngine)
		{
			_vkEngine = vkEngine;
			_dbContext = new AppDbContext();
			_dbContext.Database.EnsureCreated();

			_msgsCheckingTimer = new Timer(NewVkMsgsCheck, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

			InitBot();
		}

		private void InitBot()
		{
			_bot = new TelegramBotClient(ConfigManager.Config.TgBotToken);

			_bot.SetMyCommandsAsync(_botCommands).GetAwaiter().GetResult();
			_bot.StartReceiving(
				updateHandler: HandleUpdateAsync,
				pollingErrorHandler: HandlePollingErrorAsync,
				receiverOptions: _tgReceiverOptions
			);

			Logger.Log("Bot started!");
		}

		async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{
			if (_lastUpdateId == update.Id)
				return;
			else
				_lastUpdateId = update.Id;

			if (update.Message is not { } message)
				return;

			if (message.Text is not { } messageText)
				return;

			if (update.Message.Chat.Type != ChatType.Private)
				return;

			LogTgMsg(message);

			var chatId = message.Chat.Id;

			var trimmedText = message.Text.Trim();
			var commandString = trimmedText.Split(" ").First();

			foreach (var cmd in _botCommands)
			{
				if (string.Compare(cmd.Command, 0, commandString, 1, commandString.Length) == 0)
				{
					await cmd.Callback(message);
					break;
				}
			}
		}

		public static void LogTgMsg(Message message)
		{
			var logMsg = "";

			if (message.From.Username != null)
				logMsg += $"@{message.From.Username} ";

			if (message.From.FirstName.Length > 0)
			{
				logMsg += $"({message.From.FirstName}";

				if (message.From.LastName != null)
					logMsg += message.From.LastName;

				logMsg += ")";
			}

			logMsg += $" - \"{message.Text}\";";

			Logger.Log(logMsg);
		}

		public void NewVkMsgsCheck(object? state)
		{
			var convsResponse = _vkEngine.GetConversations(filter: "unread");

			if (!convsResponse.IsOk)
				return;

			foreach (var item in convsResponse.Response!.Items)
			{
				var conv = item.Conversation;

				var dbConv = _dbContext.Conversations.FirstOrDefault(e => e.ChatId == conv.Peer.Id);

				if (dbConv == null)
				{
					dbConv = new Models.DbConversation() { ChatId = conv.Peer.Id, LastReadConvMsgId = 0, LastReadMsgId = 0 };

					_dbContext.Conversations.Add(dbConv);
				}

				int msgsToGet;

				if (dbConv.LastReadConvMsgId == 0)
					msgsToGet = conv.UnreadCount;
				else
					msgsToGet = (int)conv.LastConversationMessageId - dbConv.LastReadConvMsgId;

				if (msgsToGet > conv.UnreadCount)
					msgsToGet = conv.UnreadCount;

				var lastMsgId = dbConv.LastReadMsgId;
				var lastMsgConvId = dbConv.LastReadConvMsgId;

				while (msgsToGet > 0)
				{
					var msgCount = msgsToGet > 200 ? 200 : msgsToGet;
					msgsToGet -= msgCount;


					var history = _vkEngine.GetHistory(conv.Peer.Id, count: msgCount, offset: msgsToGet);
					lastMsgId = history.Response.Items.First().Id;
					lastMsgConvId = history.Response.Items.First().ConversationMessageId;

					history.Response.Items.Reverse();

					foreach (var msg in history.Response.Items)
					{
						var profile = history.Response.Profiles.FirstOrDefault(e => e.Id == msg.FromId);

						var fromString = "НЕПОНЯТНЫЙ ЧЕЛОВЕК";

						if (profile != null)
							fromString = $"{profile.FirstName} {profile.LastName}";


						var title = conv.ChatSettings == null ? fromString : conv.ChatSettings.Title;

						var attachments = "";

						if (msg.Attachments.Count > 0)
						{
							attachments = "[ Приложено: ";
							foreach (var att in msg.Attachments)
							{
								attachments += $"{att.Type} ";
							}
							attachments += "]";
						}

						var dateTime = Utils.UnixTimeToLocal((double)msg.Date).ToString("dd.MM.yyyy HH:mm:ss");

						var formattedMsg = 
							$"[{dateTime}]\n" +
							$"==>{title}\n";

						if (conv.ChatSettings != null)
							formattedMsg += $"=>{fromString}\n";

						formattedMsg += $">{msg.Text} {attachments}\n\n" +
							$"https://vk.com/im?sel={conv.Peer.Id}";

						Logger.Log($"New VK msg");

						_sendMsg(formattedMsg);
					}
				}

				dbConv.LastReadConvMsgId = lastMsgConvId;
				dbConv.LastReadMsgId = lastMsgId;
				_dbContext.SaveChanges();
			}
		}

		private void _sendMsg(string msg)
		{
			_bot!.SendTextMessageAsync(ConfigManager.Config.TgOwnerChatId, msg).GetAwaiter().GetResult();
		}

		Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
		{
			var ErrorMessage = exception switch
			{
				ApiRequestException apiRequestException
					=> $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
				_ => exception.ToString()
			};

			Console.WriteLine(ErrorMessage);
			return Task.CompletedTask;
		}

		static private BotCommandCallback[] _botCommands =
		{
			new BotCommandCallback {
				Command = "start",
				Description = "Is it working?",

				Callback = async message =>
				{
					await _bot!.SendTextMessageAsync( 
						message.Chat.Id, 
						$"Yooooooo, your ChatId is {message.Chat.Id}"
					);
				}
			},
			new BotCommandCallback {
				Command = "uptime",
				Description = "Get uptime",

				Callback = async message =>
				{
					var elapsed = DateTime.UtcNow - InitUtcDateTime;

					var elapsedString = $"{elapsed.Days} d, {elapsed.Hours} h, {elapsed.Minutes} m, {elapsed.Seconds} s.";

					await _bot!.SendTextMessageAsync(message.Chat.Id, $"The bot has been working for {elapsedString} so far\n" +
						$"Bot startup time: {InitUtcDateTime.ToString("dd.MM.yyyy (HH:mm:ss)")} UTC");
				}
			},
		};
	}
}
