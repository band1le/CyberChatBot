using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;

namespace CyberChat.Services
{
    public class AudioManager
    {
        private readonly string _audioFilePath;

        public AudioManager()
        {
            _audioFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");
        }

        public void PlayVoiceGreeting()
        {
            try
            {
                if (File.Exists(_audioFilePath))
                {
                    using (var soundPlayer = new SoundPlayer(_audioFilePath))
                    {
                        soundPlayer.PlaySync();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Audio error: {ex.Message}");
            }
        }
    }
}