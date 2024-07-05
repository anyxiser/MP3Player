using System;
using System.Drawing;
using System.Windows.Forms;

namespace MP3Player
{
    public partial class MainWindow : Form
    {
        private MP3Player _mp3Player = new MP3Player();

        public delegate void ChangeSomething(string path);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeText()
        {
            songs.Items.Clear();
            if (SongsToJSON.Songs != null)
            {
                foreach (Song song in SongsToJSON.Songs)
                    songs.Items.Add(song.Name);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (SongsToJSON.GetSongsFromJSONFile() != null)
                SongsToJSON.Songs = SongsToJSON.GetSongsFromJSONFile();
            ChangeText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mp3Player.OpenSongFile();
            foreach (Song song in SongsToJSON.Songs)
            {
                if (_mp3Player.Song.Name == song.Name)
                    return;
            }
            SongsToJSON.Songs.Add(_mp3Player.Song);
            label1.Text = _mp3Player.Song.Name;
            ChangeText();
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
            SongsToJSON.WriteSongsToJSONFile();
        }

        private void songs_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Song song in SongsToJSON.Songs)
                    if (song.Name == songs.Items[songs.SelectedIndex].ToString())
                    {
                        _mp3Player.OpenInMCI(song.Path);
                        label1.Text = _mp3Player.Song.Name;
                        break;
                    }
            }
            catch
            {
                return;
            }          
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
