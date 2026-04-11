using CyberChatBot.Properties;
using System;
using System.IO;
using System.Media;

namespace CyberChatBot
{
    public class AudioManager
    {
      private readonly string _audioFilePath;

            public AudioManager()
            {
            // Look in the project source folder, NOT the bin folder
            string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            _audioFilePath = Path.Combine(projectPath, "Resources", "greeting.wav");

            Console.WriteLine($"[DEBUG] Looking for audio at: {_audioFilePath}");

            if (!File.Exists(_audioFilePath))
            {
                Console.WriteLine("[DEBUG] File not found - trying alternative locations");

                // Try other possible locations
                string[] alternatives = {
                    Path.Combine(Directory.GetCurrentDirectory(), "Resources", "greeting.wav"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "greeting.wav"),
                    @"C:\Users\Bandile\source\repos\CyberChatBot\CyberChatBot\Resources\greeting.wav"
                };

                foreach (string alt in alternatives)
                {
                    if (File.Exists(alt))
                    {
                        _audioFilePath = alt;
                        Console.WriteLine($"[DEBUG] Found at: {alt}");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("[DEBUG] Audio file found!");
            }
        }

        public void PlayVoiceGreeting()
        {
            try
            {
                if (_audioFilePath != null && File.Exists(_audioFilePath))
                {
                    Console.WriteLine("[DEBUG] Playing audio...");
                    using (var soundPlayer = new SoundPlayer(_audioFilePath))
                    {
                        soundPlayer.PlaySync();
                    }
                    Console.WriteLine("[DEBUG] Audio finished");
                }
                else
                {
                    Console.WriteLine("[DEBUG] Cannot play - file not found");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("🔊 GREETING: Hello! Welcome to the Cybersecurity Awareness Bot!");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Audio error: {ex.Message}");
            }
        }
    }
}