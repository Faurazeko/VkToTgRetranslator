using VkToTgRetranslator.Engines;

namespace VkToTgRetranslator
{
	internal class Program
	{
		private static string _vkToken = string.Empty;
		private static Timer _tokenGettingTimer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(22));

		private static TelegramEngine _tgEngine;
		private static VkEngine _vkEngine = new VkEngine();
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			ConfigManager.LoadConfig();

			_vkEngine.SetToken(_vkToken);

			while (String.IsNullOrEmpty(_vkToken))
				Thread.Sleep(1000);

			_tgEngine = new TelegramEngine(_vkEngine);

			while (true)
				Console.ReadLine();
		}

		private static void TimerCallback(object? state)
		{
			while(true) 
			{
				var newToken = VkTokenGetterEngine.GetNewToken();

				if (string.IsNullOrEmpty(newToken))
					continue;

				var isValid = _vkEngine.SetToken(newToken);

				if (!isValid)
					continue;

				_vkToken = newToken;

				return;
			}

		}
	}
}