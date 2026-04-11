using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CyberChatBot
{
    public class ResponseManager
    {
        private readonly Dictionary<string, Func<string,string>> _responsePatterns;
        public ResponseManager()
        {
            _responsePatterns = new Dictionary<string, Func<string,string>>(StringComparer.OrdinalIgnoreCase)
            {
             // Greeting patterns
                { @"\b(hi|hello|hey|greetings)\b", (name) => GetGreetingResponse(name) },
                { @"\b(how are you|how're you|how are you doing)\b", (name) => GetHowAreYouResponse() },
                
                // Cybersecurity topics
                { @"\b(password|passphrase|pwd)\b", (name) => GetPasswordAdvice() },
                { @"\b(phish|phishing|scam|fraud)\b", (name) => GetPhishingAdvice() },
                { @"\b(safe|browsing|surfing|website)\b", (name) => GetSafeBrowsingAdvice() },
                { @"\b(malware|virus|ransomware|trojan)\b", (name) => GetMalwareAdvice() },
                { @"\b(2fa|two factor|mfa|multi factor)\b", (name) => GetTwoFactorAdvice() },
                { @"\b(social media|facebook|instagram|twitter)\b", (name) => GetSocialMediaAdvice() },
                { @"\b(public wifi|wifi|network)\b", (name) => GetWifiAdvice() },
                { @"\b(backup|data loss)\b", (name) => GetBackupAdvice() },
                
                // Bot information
                { @"\b(purpose|what.*you\?|who.*you)\b", (name) => GetPurposeResponse() },
                { @"\b(ask|questions|topics|help)\b", (name) => GetHelpResponse() },
                { @"\b(thank|thanks|appreciate)\b", (name) => GetThanksResponse(name) },
                
                // General
                { @"\b(your name|what.*name)\b", (name) => GetBotNameResponse() },
                { @"\b(favorite|like|prefer)\b", (name) => GetFavoriteResponse() },
            };
        }

        public string GetResponse(string input, string userName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "I didn't understand that. Could you please rephrase?";
            }

            // Check for topic-specific responses
            foreach (var pattern in _responsePatterns)
            {
                if (Regex.IsMatch(input, pattern.Key, RegexOptions.IgnoreCase))
                {
                    return pattern.Value(userName);
                }
            }

            // Default response for unrecognized input
            return GetDefaultResponse();
        }

        private string GetGreetingResponse(string name)
        {
            string[] greetings = {
                $"Hello {name}! 👋 Ready to boost your cybersecurity knowledge?",
                $"Hi {name}! Great to see you! 💻 Let's learn about staying safe online!",
                $"Hey {name}! Welcome back! 🛡️ What cybersecurity topic interests you today?"
            };
            return greetings[new Random().Next(greetings.Length)];
        }

        private string GetHowAreYouResponse()
        {
            string[] responses = {
                "I'm doing great! 🌟 I'm powered by cybersecurity knowledge and ready to help you!",
                "I'm functioning perfectly! 🚀 Just waiting to share some important cybersecurity tips!",
                "All systems secure! 🔒 Thanks for asking. How can I help you stay safe online today?"
            };
            return responses[new Random().Next(responses.Length)];
        }

        private string GetPasswordAdvice()
        {
            return "🔐 **Password Safety Tips:**\n" +
                   "• Use passwords that are at least 12 characters long\n" +
                   "• Combine uppercase, lowercase, numbers, and symbols\n" +
                   "• Never reuse passwords across different accounts\n" +
                   "• Consider using a password manager like Bitwarden or LastPass\n" +
                   "• Enable Two-Factor Authentication (2FA) whenever possible\n" +
                   "• Avoid using personal information like birthdays or names\n\n" +
                   "Would you like to learn about 2FA as well?";
        }

        private string GetPhishingAdvice()
        {
            return "🎣 **Phishing Prevention Tips:**\n" +
                   "• Always check the sender's email address carefully\n" +
                   "• Never click on suspicious links or download unexpected attachments\n" +
                   "• Look for spelling mistakes and urgent language in emails\n" +
                   "• Hover over links to see the actual URL before clicking\n" +
                   "• If something seems too good to be true, it probably is!\n" +
                   "• Legitimate companies never ask for passwords via email\n\n" +
                   "Remember: In South Africa, phishing attacks increased by 300% in recent years!";
        }

        private string GetSafeBrowsingAdvice()
        {
            return "🌐 **Safe Browsing Tips:**\n" +
                   "• Look for 'https://' and a padlock icon in the address bar\n" +
                   "• Keep your browser and extensions updated\n" +
                   "• Use ad-blockers to avoid malicious ads\n" +
                   "• Don't save passwords in your browser\n" +
                   "• Clear your browsing data regularly\n" +
                   "• Be cautious of pop-ups claiming you've won something\n\n" +
                   "Stay vigilant while surfing the South African web!";
        }

        private string GetMalwareAdvice()
        {
            return "🦠 **Malware Protection:**\n" +
                   "• Install and update reputable antivirus software\n" +
                   "• Don't download software from untrusted sources\n" +
                   "• Keep your operating system updated\n" +
                   "• Be careful with USB drives from unknown sources\n" +
                   "• Regular system scans can detect threats early\n\n" +
                   "Consider using Windows Defender or a trusted third-party antivirus.";
        }

        private string GetTwoFactorAdvice()
        {
            return "🔑 **Two-Factor Authentication (2FA):**\n" +
                   "• Adds an extra layer of security to your accounts\n" +
                   "• Use authenticator apps (Google Authenticator, Authy) instead of SMS\n" +
                   "• Backup codes should be stored securely\n" +
                   "• Most South African banks now require 2FA\n\n" +
                   "Enable 2FA on your email, banking, and social media accounts immediately!";
        }

        private string GetSocialMediaAdvice()
        {
            return "📱 **Social Media Safety:**\n" +
                   "• Review privacy settings regularly\n" +
                   "• Don't share your location in real-time\n" +
                   "• Be careful what personal info you post\n" +
                   "• Verify friend requests before accepting\n" +
                   "• Think twice before posting vacation plans\n\n" +
                   "South Africans are increasingly targeted through social engineering on social media!";
        }

        private string GetWifiAdvice()
        {
            return "📶 **Public WiFi Safety:**\n" +
                   "• Avoid accessing banking or sensitive accounts on public WiFi\n" +
                   "• Use a VPN (Virtual Private Network) for encryption\n" +
                   "• Disable automatic WiFi connections\n" +
                   "• Verify the official network name with staff\n" +
                   "• Consider using your mobile data for sensitive transactions\n\n" +
                   "South African coffee shops and malls are common targets for fake WiFi networks!";
        }

        private string GetBackupAdvice()
        {
            return "💾 **Backup Strategy:**\n" +
                   "• Follow the 3-2-1 rule: 3 copies, 2 different media, 1 off-site\n" +
                   "• Use cloud services like Google Drive or Microsoft OneDrive\n" +
                   "• Test your backups regularly\n" +
                   "• Consider external hard drives for local backups\n\n" +
                   "Ransomware attacks in South Africa have made backups more important than ever!";
        }

        private string GetPurposeResponse()
        {
            return "🎯 **My Purpose:**\n" +
                   "I'm your Cybersecurity Awareness Assistant! I'm here to:\n" +
                   "• Educate South Africans about online safety\n" +
                   "• Provide practical tips to prevent cyber attacks\n" +
                   "• Help you recognize common threats like phishing and malware\n" +
                   "• Make cybersecurity knowledge accessible and engaging\n\n" +
                   "Ask me about passwords, phishing, safe browsing, or any cybersecurity topic!";
        }

        private string GetHelpResponse()
        {
            return "📚 **Topics I Can Help With:**\n" +
                   "• 🔐 Password Safety\n" +
                   "• 🎣 Phishing Prevention\n" +
                   "• 🌐 Safe Browsing\n" +
                   "• 🦠 Malware Protection\n" +
                   "• 🔑 Two-Factor Authentication\n" +
                   "• 📱 Social Media Safety\n" +
                   "• 📶 Public WiFi Security\n" +
                   "• 💾 Data Backup\n\n" +
                   "Just ask me about any of these topics! Type 'exit' when you're done.";
        }

        private string GetThanksResponse(string name)
        {
            string[] responses = {
                $"You're welcome, {name}! Stay safe online! 🛡️",
                $"My pleasure, {name}! Remember, cybersecurity is everyone's responsibility!",
                $"Glad I could help, {name}! Feel free to ask more questions anytime!"
            };
            return responses[new Random().Next(responses.Length)];
        }

        private string GetBotNameResponse()
        {
            return "I'm CyberGuardian! 🛡️ Your personal cybersecurity awareness assistant for South African citizens.";
        }

        private string GetFavoriteResponse()
        {
            return "My favorite thing is helping South Africans stay safe online! 🛡️💻 I love sharing cybersecurity tips and protecting people from digital threats!";
        }

        private string GetDefaultResponse()
        {
            string[] defaultResponses = {
                "I'm not sure I understand. Could you ask about cybersecurity topics like passwords, phishing, or safe browsing?",
                "Hmm, I didn't catch that. Try asking me about password safety or how to spot phishing emails!",
                "I'm still learning! Try asking about cybersecurity topics like 2FA, malware, or public WiFi safety.",
                "Let's focus on cybersecurity! Ask me about protecting your accounts or staying safe online."
            };
            return defaultResponses[new Random().Next(defaultResponses.Length)];
        }
    }
}

