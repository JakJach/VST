using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Windows.Media;

namespace VST.UI.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        private readonly MediaPlayer mediaElement;

        public MainViewModel()
        {
            mediaElement = new MediaPlayer();
        }

        public void SelectFileClick()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                InitialDirectory = Environment.SpecialFolder.MyMusic.ToString(),

                Filter = "Media files (*.wav)|*.wav|All Files (*.*)|*.*",

                RestoreDirectory = true
            };



            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;

                mediaElement.Open(new Uri(selectedFileName));
            }
        }

        public void PlayClick()
        {
            if (mediaElement != null)
            {
                mediaElement.Play();
            }
        }

        public void PauseClick()
        {
            if (mediaElement != null && mediaElement.CanPause)
            {
                mediaElement.Pause();
            }
        }
    }
}
