using _5Words.Commands.Interfaces;
using _5Words.Extensions;
using _5Words.Managers;
using _5Words.Utility;
using Telegram.Bot.Types;

namespace _5Words.Commands.Factory
{
    public static class CommandFactory
    {
        public static IBotCommand Create(string command)
        {
            if (command.StartWithAny(ConfigurationManager.Configuration.Commands.Start))
                return new StartCommand();
            else if (command.StartWithAny(ConfigurationManager.Configuration.Commands.Find))
                return new FindCommand();
            else if (command.StartWithAny(ConfigurationManager.Configuration.Commands.Help))
                return new HelpCommand();
            else if (command.StartWithAny(ConfigurationManager.Configuration.Commands.Random))
                return new RandomCommand();
            else if (command.StartWithAny(ConfigurationManager.Configuration.Commands.Length))
                return new LengthCommand();
            else if (command.StartWithAny(ConfigurationManager.Configuration.Commands.Contains))
                return new ContainsCommand();
            else if (command.StartWithAny(ConfigurationManager.Configuration.Commands.NonContains))
                return new NonContainsCommand();
            else if (command.StartWithAny(ConfigurationManager.Configuration.Commands.Template))
                return new TemplateCommand();
            else if (command.StartWithAny(ConfigurationManager.Configuration.Commands.AntiTemplate))
                return new AntiTemplateCommand();
            else if (command.StartWithAny(ConfigurationManager.Configuration.Commands.StateInfo))
                return new StateInfoCommand();


            return new NotFindCommand();
        }
    }
}
