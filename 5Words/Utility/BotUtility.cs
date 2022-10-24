using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Models;
using _5Words.Extensions;
using Telegram.Bot.Types.Enums;
using _5Words.Managers;

namespace _5Words.Utility
{
    public static class BotUtility
    {
        public static async Task SetCommandMenu(ITelegramBotClient bot)
        {
            var commandsProps = ConfigurationManager.Configuration.Commands.GetType().GetProperties();
            var botCommandsMenuCollection = new List<BotCommand> {
                new BotCommand
                {
                    Command = "/start",
                    Description = "Начало работы",
                },
                new BotCommand
                {
                    Command = "/help",
                    Description = "Помощь по поиску"
                },
                new BotCommand
                {
                    Command = "/params",
                    Description = "Текущие параметры поиска"
                },
                new BotCommand
                {
                    Command = "/random",
                    Description = "Случайное слово(обязательно должен присутствовать параметр длина)"
                },
                 new BotCommand
                {
                    Command = "/find",
                    Description = "Запуск поиска по параметрам"
                },
            };
            
            await bot.SetMyCommandsAsync(botCommandsMenuCollection);
        }
    }
}
