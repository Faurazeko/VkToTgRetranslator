using VkToTgRetranslator.Models;

namespace VkToTgRetranslator
{
	static public class ConfigManager
	{
		public static Config Config = null;
		private static string _configPath = "config.json";

		public static void LoadConfig()
		{
			if (!File.Exists(_configPath))
				ResetConfig();
			else
			{
				var configText = File.ReadAllText(_configPath);
				var config = System.Text.Json.JsonSerializer.Deserialize<Config>(configText);

				if (config == null)
					ResetConfig();
				else
					Config = config;
			}
		}

		public static void SaveConfig()
		{
			var text = System.Text.Json.JsonSerializer.Serialize(Config);
			File.WriteAllText(_configPath, text);
		}

		public static void ResetConfig()
		{
			Config = new Config();
			SaveConfig();
		}
	}
}
