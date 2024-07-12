using System;
using System.Drawing;
using System.Windows.Forms;

namespace MP3Player
{
    public partial class MainWindow : Form
    {
        private MP3Player _mp3Player = new MP3Player();
        private DictionarySaver<string> _saver = new DictionarySaver<string>("songs.json");

        public delegate void ChangeSomething(string path);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeText()
        {
            songs.Items.Clear();
            
            foreach (string song in _saver.Data.Keys)
            {
                songs.Items.Add(song);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _saver.Load();

            ChangeText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mp3Player.OpenSongFile();

            if (_saver.Data.ContainsKey(_mp3Player.Song.Name))
                return;

            _saver.AddElement(_mp3Player.Song.Name, _mp3Player.Song.Path);
            songs.Items.Add(_mp3Player.Song.Name);
            label1.Text = _mp3Player.Song.Name;
            songs.SelectedIndex = songs.Items.Count - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _mp3Player.PlaySongFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _mp3Player.CloseSongFile();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _saver.Save();
        }

        private void songs_SelectedValueChanged(object sender, EventArgs e)
        {
            string song = songs.Items[songs.SelectedIndex].ToString();

            if (_saver.Data.ContainsKey(song))
                return;

            _mp3Player.OpenInWMP(_saver.Data[song]);
            label1.Text = song;
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreferencesWindow form2 = new PreferencesWindow();
            form2.Show();
            form2.OnChangeSomething += ChangeBackground;
        }

        private void ChangeBackground(string pathToImage)
        {
            BackgroundImage = Image.FromFile(pathToImage);
        }
    }
}
