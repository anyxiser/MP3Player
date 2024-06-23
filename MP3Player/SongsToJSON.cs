using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MP3Player
{
    internal static class SongsToJSON
    {
        private const string _fileName = "songs.json";

        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        public static List<Song> Songs { get; private set; } = GetSongsFromJSONFile();

        public static void WriteSongsToJSONFile()
        {
            string jsonString = JsonSerializer.Serialize(Songs, _options);
            File.WriteAllText(_fileName, jsonString);
        }

        public static List<Song> GetSongsFromJSONFile()
        {
            if (File.Exists(_fileName))
            {
                string jsonString = File.ReadAllText(_fileName);
                List<Song> songs = JsonSerializer.Deserialize<List<Song>>(jsonString);
                return songs;
            }
            return null;
        }
    }
}
