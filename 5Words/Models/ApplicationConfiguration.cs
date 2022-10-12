using Newtonsoft.Json;

namespace _5Words.Models
{
    public class ApplicationConfiguration
    {
        public RunType RunType { get; set; } = RunType.Console;
        public string TelegramBotApiKey { get; set; }
        public string DictionaryFileName { get; set; }
        public MessagesConfiguration Messages { get; set; }

        public CommandsConfiguration Commands { get; set; }
        public static ApplicationConfiguration LoadConfiguration()
        {
            var configFileName = "appsettings.json";
            var dir = Directory.GetCurrentDirectory();
            var path = Path.Combine(dir, configFileName);
            if (File.Exists(path))
            {
                var configText = File.ReadAllText(path);
                var result = JsonConvert.DeserializeObject<ApplicationConfiguration>(configText);
                return result; 
            }

            return new ApplicationConfiguration();
        }
    }

    public enum RunType
    { 
        Console,
        TelegramBot
    }
}
