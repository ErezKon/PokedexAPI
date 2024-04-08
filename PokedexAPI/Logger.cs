using PokedexAPI.Extensions;
using LogLevel = PokedexAPI.Enums.LogLevel;

namespace PokedexAPI
{
    internal static class Logger
    {
        private static readonly object logMutex = new();
        private static readonly string logFileName;
        private static readonly Dictionary<string, ConsoleColor> tags = new();
        private static readonly Random _random = new Random();

        static Logger()
        {
            var dir = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Working Directory: {dir}");
            var logName = $"{DateTime.Now.ToLocalTime():ddMMyyyy.HHmmssfff}.log";
            logFileName = Path.Combine(new string[] { dir, "Logs", logName });
            Console.WriteLine($"Log File Name: {logFileName}");
            FileExtension.SafeCreate(logFileName);
        }
        public static void Log(string tag, string message, LogLevel lvl = LogLevel.Info, ConsoleColor color = ConsoleColor.Gray)
        {
            lock (logMutex)
            {
                var stamp = DateTime.Now.ToLocalTime().ToString("ddMMyyyy.HHmmss.fff");
                var level = lvl == LogLevel.Info ? "[I]" : lvl == LogLevel.Warning ? "[W]" : "[E]";
                var msg = $"{stamp} {level} - {message}\n";
                string sanitizedTag = tag;
                if (tag[0] != '[')
                {
                    sanitizedTag = '[' + tag;
                }
                if (tag[tag.Length - 1] != ']')
                {
                    sanitizedTag += ']';
                }

                File.AppendAllText(logFileName, $"{sanitizedTag} {msg}");
                Console.ForegroundColor = GetTagColor(sanitizedTag);
                Console.Write(sanitizedTag + ' ');
                Console.ForegroundColor = color;
                Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private static ConsoleColor GetTagColor(string tag)
        {
            if (tags.ContainsKey(tag))
            {
                return tags[tag];
            }
            var color = GetRandomConsoleColor();
            tags.Add(tag, color);
            return color;
        }

        private static ConsoleColor GetRandomConsoleColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(_random.Next(consoleColors.Length));
        }
    }

}
