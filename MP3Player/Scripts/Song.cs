﻿namespace MP3Player
{
    public class Song
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public Song(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
