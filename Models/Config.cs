namespace VkToTgRetranslator.Models
{
    public class Config
    {
        public string TgBotToken { get; set; } = "TgBotToken";
        public int TgOwnerChatId { get; set; } = 0;
		public string VkLogin { get; set; } = "VkLogin";
        public string VkPassword { get; set; } = "VkPassword";
        public string VkGetTokenUrl { get; set; } =
            "https://oauth.vk.com/authorize?client_id=6287487&scope=1073737727" +
            "&redirect_uri=https://oauth.vk.com/blank.html&display=page&response_type=token&revoke=1";

	}
}
