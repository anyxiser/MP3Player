using System;
using System.Windows.Forms;

namespace MP3Player
{
    public partial class Form1 : Form
    {
        private MP3Player _mp3Player = new MP3Player();

        public Form1()
        {
            InitializeComponent();
        }

        private void changeText()
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
            changeText();
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
            changeText();                  
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
            foreach (Song song in SongsToJSON.Songs)
                if (song.Name == songs.Items[songs.SelectedIndex])
                {
                    _mp3Player.OpenInMCI(song.Path);
                    label1.Text = _mp3Player.Song.Name;
                    break;
                }
        }
    }
}
