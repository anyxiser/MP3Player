using System;
using System.Drawing;
using System.Windows.Forms;

namespace MP3Player
{
    public partial class PreferencesWindow : Form
    {
        public event MainWindow.ChangeSomething OnChangeSomething;

        private string _pathToBackgroundImage = "";

        private string _filter = "Image | *.jpg; *.png; *.JPEG";

        public PreferencesWindow()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) 
            {
                openFileDialog.Filter = _filter;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {         
                    previewImage.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                    _pathToBackgroundImage = openFileDialog.FileName;
                }
            }
        }

        private void PreferencesWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _pathToBackgroundImage = "";
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            OnChangeSomething(_pathToBackgroundImage);
            Close();
        }
    }
}
