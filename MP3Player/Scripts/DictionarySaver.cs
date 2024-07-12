using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MP3Player
{
    public class DictionarySaver<T>
    {
        public Dictionary<string, T> Data => _data;

        private Dictionary<string, T> _data = new Dictionary<string, T>();

        private string _filePath = string.Empty;
        
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        public DictionarySaver(string filePath = "data.json")
        {
            _filePath = filePath;
        }

        public void AddElement(string name, T element)
        {
            _data.Add(name, element);
        }

        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(_data, _options);
            File.WriteAllText(_filePath, jsonString);
        }

        public void Load()
        {
            if (!File.Exists(_filePath))
                return;

            string jsonString = File.ReadAllText(_filePath);
            _data = JsonSerializer.Deserialize<Dictionary<string, T>>(jsonString);

            if (_data == null)
                _data = new Dictionary<string, T>();
        }
    }
}
