using System.Windows.Forms;
using WMPLib;
using System.IO;

namespace MP3Player
{
    partial class MP3Player
    {
        public Song Song => _song;

        private Song _song = null;
        private WindowsMediaPlayer _mediaPlayer = new WindowsMediaPlayer();

        private string getFileName(string path)
        {
            string[] separatedPath = path.Split('\\');

            if (separatedPath.Length == 0)
                return string.Empty;

            string songName = separatedPath[separatedPath.Length - 1];

            if (songName.Length == 0)
                return string.Empty;

            return songName.Split('.')[0];
        }

        public void OpenInWMP(string path)
        {
            _song = new Song(getFileName(path), path);
        }

        public void OpenSongFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Media | *.wav; *.mp3"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)                         
                OpenInWMP(openFileDialog.FileName);     
            
        }

        public void PlaySongFile()
        {
            if (_song == null || !File.Exists(_song.Path))
                return;

            _mediaPlayer.URL = _song.Path;
            _mediaPlayer.controls.play();
        }

        public void CloseSongFile()
        {
            if (_song == null)
                return;

            _mediaPlayer.controls.stop();
            _song = null;
        }
    }
}
