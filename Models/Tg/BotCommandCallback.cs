using Telegram.Bot.Types;

#pragma warning disable CS8618

namespace VkToTgRetranslator.Models.Tg
{
    class BotCommandCallback : BotCommand
    {
        public delegate Task ExecuteCallbackFn(Message message);

        public ExecuteCallbackFn Callback { get; set; }
    }
}
