using System;
using System.Media;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CyberChatBot
{
    public class CyberChatBot
    {
        private string _userName;
        private readonly ResponseManager _responseManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;
        public CyberChatBot()
        {
            _responseManager = new ResponseManager();
            _uiManager = new UIManager();
            _audioManager = new AudioManager();
        }
        public void Start()
        {
            try
            {
                // Play voice greeting
                _audioManager.PlayVoiceGreeting();

                // Display ASCII art logo
                _uiManager.DisplayLogo();

                // show welcome message with decorative borders
                _uiManager.DisplayWelcomeBorder();

                // Get users name with vaildation
                _userName = GetUserName();

                // Display personalized greeting
                _uiManager.DisplayPersonalizedWelcome(_userName);

                //show available topics
                _uiManager.DisplayHelpMenu();

                // Start conversation loop
                RunConversationLoop();
            }
            catch (Exception ex)
            {
                _uiManager.DisplayError($"An error occured:{ex.Message}");
            }
        }
        private string GetUserName()
        {
            String name = string.Empty;
            bool ValidInput = false;

            while (!ValidInput)
            {
                _uiManager.DisplayPrompt("Please enter your name:");
                name = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    _uiManager.DisplayWarning("Name cannot be empty. Please try again.");
                }
                else if (name.Length < 2)
                {
                    _uiManager.DisplayWarning("Name must be at least 2 characters long.");
                }
                else if (name.Length > 30)
                {
                    _uiManager.DisplayWarning("Name cannot be longer than 30 characters.");
                }
                else
                {
                    ValidInput = true;
                }
                }
                return name;
            }
        
            private void RunConversationLoop()
            {
                bool isRunning = true;
                while (isRunning)
                {
                    _uiManager.DisplayPrompt($"{_userName}:");
                    string userInput = Console.ReadLine()?.Trim().ToLower();

                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        _uiManager.DisplayBotMessage("Input cannot be empty. Please try again.");
                        continue;
                    }
                    // Check for exit commands
                    if (userInput == "exit" || userInput == "quit" || userInput == "bye")
                    {
                        _uiManager.DisplayGoodbye(_userName);
                        isRunning = false;
                        continue;
                    }
                    // Process the input and get response
                    string response = _responseManager.GetResponse(userInput, _userName);
                    _uiManager.DisplayBotMessage(response);
                }
            }
        }
    }