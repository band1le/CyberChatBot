using CyberChat.Models;
using CyberChat.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResponseManager _responseManager;
        private AudioManager _audioManager;
        private Sentiment_Analyzer _sentimentAnalyzer;
        private MemoryManager _memoryManager;
        private string _userName;

        public MainWindow()
        {
            InitializeComponent();
            // Initialize all services
            _responseManager = new ResponseManager();
            _audioManager = new AudioManager();
            _sentimentAnalyzer = new Sentiment_Analyzer();
            _memoryManager = new MemoryManager();

            // Play voice greeting
            _audioManager.PlayVoiceGreeting();

            // Ask for name
            AskForName();
        }

        private void AskForName()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Welcome to CyberChatBot!\n\nPlease enter your name:",
                "Welcome",
                "");

            if (!string.IsNullOrWhiteSpace(input))
            {
                _userName = input.Trim();
                _memoryManager.SetUserName(_userName);
                AddMessage($"🤖 Bot: Hello {_userName}! 👋 I'm CyberGuardian.");
                AddMessage($"🤖 Bot: I'm here to help you stay safe online!");
                AddMessage($"🤖 Bot: Ask me about passwords, phishing, malware, or click the buttons below!");

                // Check for personalized greeting if returning user
                var personalizedGreeting = _memoryManager.GetPersonalizedGreeting();
                if (!string.IsNullOrEmpty(personalizedGreeting))
                {
                    AddMessage($"🤖 Bot: {personalizedGreeting}");
                }
            }
            else
            {
                _userName = "User";
                _memoryManager.SetUserName("User");
                AddMessage($"🤖 Bot: Welcome! I'm CyberGuardian, your cybersecurity assistant.");
            }
        }

        private void AddMessage(string message)
        {
            ChatDisplay.Items.Add(message);
            ChatDisplay.ScrollIntoView(ChatDisplay.Items[ChatDisplay.Items.Count - 1]);
        }

        private void SendUserMessage(string message)
        {
            AddMessage($"👤 {_userName}: {message}");

            // Increment question count for memory
            _memoryManager.IncrementQuestions();

            // Analyze sentiment
            var sentiment = _sentimentAnalyzer.AnalyzeSentiment(message);
            string empatheticPrefix = "";
            string encouragement = "";

            // Add empathetic response for non-neutral, non-positive sentiments
            if (sentiment != Sentiment.Neutral && sentiment != Sentiment.Curious && sentiment != Sentiment.Grateful && sentiment != Sentiment.Confident)
            {
                empatheticPrefix = _sentimentAnalyzer.GetEmpatheticResponse(sentiment, _responseManager.GetCurrentTopic());
                encouragement = " " + _sentimentAnalyzer.GetEncouragement(sentiment);
                
            }

            // Detect topics and remember interests
            string lowerInput = message.ToLower();
            if (lowerInput.Contains("password")) _memoryManager.RememberInterest("passwords");
            if (lowerInput.Contains("phish") || lowerInput.Contains("scam")) _memoryManager.RememberInterest("phishing");
            if (lowerInput.Contains("malware") || lowerInput.Contains("virus")) _memoryManager.RememberInterest("malware");
            if (lowerInput.Contains("2fa") || lowerInput.Contains("two factor")) _memoryManager.RememberInterest("2FA");
            if (lowerInput.Contains("privacy")) _memoryManager.RememberInterest("privacy");
            if (lowerInput.Contains("wifi")) _memoryManager.RememberInterest("WiFi");

            // Get the cybersecurity response
            string lastTopic = _memoryManager.GetLastTopic();
            string response = _responseManager.GetResponse(message, _userName, lastTopic);

            // Store current topic for memory
            string currentTopic = _responseManager.GetCurrentTopic();
            if (!string.IsNullOrEmpty(currentTopic))
            {
                _memoryManager.RememberInterest(currentTopic);
            }

            // Combine everything
            AddMessage($"🤖 Bot: {empatheticPrefix}{response}{encouragement}");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MessageInput.Text))
            {
                SendUserMessage(MessageInput.Text);
                MessageInput.Clear();
                MessageInput.Focus();
            }
        }

        private void MessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendButton_Click(sender, e);
            }
        }

        private void TopicButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string topic = btn.Content.ToString();

            if (topic.Contains("Password"))
                SendUserMessage("password");
            else if (topic.Contains("Phishing"))
                SendUserMessage("phishing");
            else if (topic.Contains("Malware"))
                SendUserMessage("malware");
            else if (topic.Contains("2FA"))
                SendUserMessage("2fa");
            else if (topic.Contains("WiFi"))
                SendUserMessage("public wifi");
            else if (topic.Contains("Help"))
                SendUserMessage("help");
            else if (topic.Contains("Exit"))
                SendUserMessage("exit");
            else
                SendUserMessage(topic.ToLower());
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            string helpMessage = "📚 **Topics I can help with:**\n\n" +
                                 "• 🔐 Password Safety - Strong passwords and managers\n" +
                                 "• 🎣 Phishing - Spot fake emails and scams\n" +
                                 "• 🦠 Malware - Viruses, ransomware protection\n" +
                                 "• 🔑 Two-Factor Authentication (2FA)\n" +
                                 "• 📶 Public WiFi - Safe browsing on public networks\n\n" +
                                 "Just type your question or click the buttons!";

            AddMessage($"🤖 Bot: {helpMessage}");
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            int totalQuestions = _memoryManager.GetQuestionCount();
            MessageBox.Show($"Goodbye {_userName}! Stay safe online! 🔒\n\nYou asked {totalQuestions} cybersecurity questions today!",
                           "Goodbye",
                           MessageBoxButton.OK,
                           MessageBoxImage.Information);
            Application.Current.Shutdown();
        }
    }
}