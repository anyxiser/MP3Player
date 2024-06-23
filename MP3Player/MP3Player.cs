using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MP3Player
{
    partial class MP3Player
    {
        

        [DllImport("winmm.dll")]
        private static extern long mciSendString(
            string command,
            StringBuilder stringReturn,
            int returnLength,
            IntPtr hwndCallBack);

        public Song Song { get; set; }
        private bool _isFileOpened = false;

        private string _getNameOfSong(string path)
        {
            string[] separatedPath = path.Split('\\');
            string songName = separatedPath[separatedPath.Length - 1];
            return songName.Split('.')[0];
        }

        public void OpenInMCI(string path)
        {
            Song = new Song(_getNameOfSong(path), path);
            string commandString = "open \"" + path + "\" type mpegvideo alias MediaFile";
            mciSendString(commandString, null, 0, IntPtr.Zero);

            _isFileOpened = true;
        }

        public void OpenSongFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Media | *.wav; *.mp3"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                OpenInMCI(openFileDialog.FileName);
               
            }
        }

        public void PlaySongFile()
        {
            if (_isFileOpened)
            {
                string commandString = "play " + Song.Path + " from 0";
                mciSendString(commandString, null, 0, IntPtr.Zero);
            }
        }

        public void CloseSongFile()
        {
            if (_isFileOpened)
            {
                string commandString = "close " + Song.Path;
                mciSendString(commandString, null, 0, IntPtr.Zero);
                _isFileOpened = false;
            }
        }

    }
}
