using System;
using System.IO;
using System.Xml.Serialization;

namespace Lexicon
{
    public static class SettingsManager
    {
        private static readonly string FilePath = "config.xml";
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(AppSettings));

        public static void Save(AppSettings settings)
        {
            try
            {
                using (FileStream stream = new FileStream(FilePath, FileMode.Create))
                {
                    Serializer.Serialize(stream, settings);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи XML: {ex.Message}");
            }
        }

        public static AppSettings Load()
        {
            if (!File.Exists(FilePath))
            {
                var defaultSettings = new AppSettings();
                Save(defaultSettings);
                return defaultSettings;
            }

            try
            {
                using (FileStream stream = new FileStream(FilePath, FileMode.Open))
                {
                    return (AppSettings)Serializer.Deserialize(stream) ?? new AppSettings();
                }
            }
            catch
            {
                return new AppSettings();
            }
        }
    }
}