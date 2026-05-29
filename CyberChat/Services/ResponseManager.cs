using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CyberChat.Services
{
    public class ResponseManager
    {
        private readonly Dictionary<string, Func<string, List<string>>> _responsePatterns;
        private readonly Random _random;
        private string _currentTopic = null;

        public ResponseManager()
        {
            _random = new Random();
            _responsePatterns = new Dictionary<string, Func<string, List<string>>>(StringComparer.OrdinalIgnoreCase)
            {
                // Greetings
                { @"\b(hi|hello|hey|howdy|greetings)\b", (name) => GetGreetingResponses(name) },
                { @"\b(how are you|how do you do)\b", (name) => GetHowAreYouResponses() },
                
                // Password topics
                { @"\b(password|passphrase|pwd|strong password)\b", (name) => GetPasswordResponses() },
                
                // Phishing topics
                { @"\b(phish|phishing|scam|fraud|suspicious email)\b", (name) => GetPhishingResponses() },
                
                // Malware topics
                { @"\b(malware|virus|ransomware|trojan|spyware)\b", (name) => GetMalwareResponses() },
                
                // 2FA topics
                { @"\b(2fa|two factor|mfa|multi factor|authenticator)\b", (name) => GetTwoFactorResponses() },
                
                // Privacy topics
                { @"\b(privacy|private|data protection|personal info)\b", (name) => GetPrivacyResponses() },
                
                // Safe browsing
                { @"\b(safe browsing|https|secure website|padlock)\b", (name) => GetSafeBrowsingResponses() },
                
                // Public WiFi
                { @"\b(public wifi|wifi hotspot|free wifi)\b", (name) => GetWifiResponses() },
                
                // Backup
                { @"\b(backup|data loss|recover)\b", (name) => GetBackupResponses() },
                
                // Follow-up questions
                { @"\b(tell me more|explain more|more details|elaborate|continue)\b", (name) => GetFollowUpResponses() },
                { @"\b(another tip|another one|give me another)\b", (name) => GetAnotherTipResponses() },
                
                // Help
                { @"\b(help|what can you do|topics|what do you know)\b", (name) => GetHelpResponses() },
                
                // Thanks
                { @"\b(thank|thanks|appreciate|helpful)\b", (name) => GetThanksResponses(name) },
                
                // Exit
                { @"\b(exit|quit|bye|goodbye)\b", (name) => GetExitResponses(name) },
            };
        }

        public string GetResponse(string input, string userName, string currentTopic = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return GetDefaultResponse();

            // Store current topic for follow-ups
            if (!string.IsNullOrEmpty(currentTopic))
                _currentTopic = currentTopic;

            foreach (var pattern in _responsePatterns)
            {
                if (Regex.IsMatch(input, pattern.Key, RegexOptions.IgnoreCase))
                {
                    var responses = pattern.Value(userName);
                    return responses[_random.Next(responses.Count)];
                }
            }

            // Check for follow-up based on stored topic
            if (_currentTopic != null && (input.Contains("more") || input.Contains("another") || input.Contains("elaborate")))
            {
                return GetFollowUpOnTopic(_currentTopic);
            }

            return GetDefaultResponse();
        }

        #region Response Collections (For Random Selection)

        private List<string> GetGreetingResponses(string name)
        {
            return new List<string>
            {
                $"Hello {name}! 👋 Ready to boost your cybersecurity knowledge?",
                $"Hi {name}! Great to see you! 💻 Let's learn about staying safe online!",
                $"Hey {name}! Welcome back! 🛡️ What cybersecurity topic interests you today?",
                $"Greetings {name}! I'm excited to help you learn about online safety! 🌟"
            };
        }

        private List<string> GetHowAreYouResponses()
        {
            return new List<string>
            {
                "I'm doing great! 🌟 Powered by cybersecurity knowledge and ready to help!",
                "I'm functioning perfectly! 🚀 Just waiting to share important security tips!",
                "All systems secure! 🔒 How can I help you stay safe online today?",
                "I'm fantastic! 💪 Always happy to talk about cybersecurity!"
            };
        }

        private List<string> GetPasswordResponses()
        {
            _currentTopic = "password";
            return new List<string>
            {
                "🔐 **Password Safety Tip #1:** Use passwords that are at least 12 characters long with a mix of uppercase, lowercase, numbers, and symbols!",
                "🔐 **Password Safety Tip #2:** Never reuse passwords across different accounts. Each account needs its own unique password!",
                "🔐 **Password Safety Tip #3:** Consider using a password manager like Bitwarden or LastPass to generate and store strong passwords!",
                "🔐 **Password Safety Tip #4:** Enable Two-Factor Authentication (2FA) whenever possible for an extra layer of security!",
                "🔐 **Password Safety Tip #5:** Avoid using personal information like birthdays, names, or common words in your passwords!"
            };
        }

        private List<string> GetPhishingResponses()
        {
            _currentTopic = "phishing";
            return new List<string>
            {
                "🎣 **Phishing Prevention Tip #1:** Always check the sender's email address carefully - scammers use addresses that look legitimate but have small differences!",
                "🎣 **Phishing Prevention Tip #2:** Never click on suspicious links! Hover over them first to see the actual URL before clicking.",
                "🎣 **Phishing Prevention Tip #3:** Look for spelling mistakes and urgent language - 'Act now!' and 'Your account will be closed!' are common red flags!",
                "🎣 **Phishing Prevention Tip #4:** Legitimate companies never ask for passwords or personal information via email!",
                "🎣 **Phishing Prevention Tip #5:** In South Africa, phishing attacks increased by 300% recently. Always verify before clicking!"
            };
        }

        private List<string> GetMalwareResponses()
        {
            _currentTopic = "malware";
            return new List<string>
            {
                "🦠 **Malware Protection Tip #1:** Install and update reputable antivirus software on all your devices!",
                "🦠 **Malware Protection Tip #2:** Keep your operating system and all applications updated - updates often include security patches!",
                "🦠 **Malware Protection Tip #3:** Never download software from untrusted sources or click on pop-up ads!",
                "🦠 **Malware Protection Tip #4:** Be careful with USB drives from unknown sources - they can contain malware!",
                "🦠 **Malware Protection Tip #5:** Regular system scans can detect threats early. Schedule weekly scans for best protection!"
            };
        }

        private List<string> GetTwoFactorResponses()
        {
            _currentTopic = "2fa";
            return new List<string>
            {
                "🔑 **2FA Tip #1:** Two-Factor Authentication adds an extra layer of security to your accounts - enable it everywhere possible!",
                "🔑 **2FA Tip #2:** Use authenticator apps like Google Authenticator or Authy instead of SMS for better security!",
                "🔑 **2FA Tip #3:** Always save your backup codes in a secure place - they're the only way to recover if you lose your phone!",
                "🔑 **2FA Tip #4:** Most South African banks now require 2FA for online banking - it significantly reduces fraud!",
                "🔑 **2FA Tip #5:** Even with 2FA, keep your passwords strong. 2FA is an extra layer, not a replacement for good passwords!"
            };
        }

        private List<string> GetPrivacyResponses()
        {
            _currentTopic = "privacy";
            return new List<string>
            {
                "🔒 **Privacy Tip #1:** Review your privacy settings on social media regularly. Limit what the public can see!",
                "🔒 **Privacy Tip #2:** Be careful what personal information you share online - birthdays, addresses, and phone numbers can be used by scammers!",
                "🔒 **Privacy Tip #3:** Use privacy-focused browsers like Brave or Firefox with enhanced tracking protection!",
                "🔒 **Privacy Tip #4:** Regularly clear your browsing history, cookies, and cache to protect your privacy!",
                "🔒 **Privacy Tip #5:** Consider using a VPN to encrypt your internet connection, especially on public WiFi!"
            };
        }

        private List<string> GetSafeBrowsingResponses()
        {
            _currentTopic = "safe browsing";
            return new List<string>
            {
                "🌐 **Safe Browsing Tip #1:** Always look for 'https://' and a padlock icon in the address bar before entering personal information!",
                "🌐 **Safe Browsing Tip #2:** Keep your browser and extensions updated to the latest versions for security patches!",
                "🌐 **Safe Browsing Tip #3:** Use ad-blockers to avoid malicious ads that can contain malware!",
                "🌐 **Safe Browsing Tip #4:** Don't save passwords in your browser - use a dedicated password manager instead!",
                "🌐 **Safe Browsing Tip #5:** Be cautious of pop-ups claiming you've won something - legitimate companies don't advertise that way!"
            };
        }

        private List<string> GetWifiResponses()
        {
            _currentTopic = "Public WiFi";
            return new List<string>
            {
                "📶 **Public WiFi Tip #1:** Avoid accessing banking or sensitive accounts on public WiFi networks!",
                "📶 **Public WiFi Tip #2:** Always use a VPN (Virtual Private Network) when connecting to public WiFi for encryption!",
                "📶 **Public WiFi Tip #3:** Disable automatic WiFi connections so your device doesn't connect to unknown networks!",
                "📶 **Public WiFi Tip #4:** Verify the official network name with staff before connecting - fake networks are common!",
                "📶 **Public WiFi Tip #5:** Consider using your mobile data instead of public WiFi for sensitive transactions!"
            };
        }

        private List<string> GetBackupResponses()
        {
            _currentTopic = "backup";
            return new List<string>
            {
                "💾 **Backup Tip #1:** Follow the 3-2-1 rule: 3 copies of your data, on 2 different media types, with 1 copy off-site!",
                "💾 **Backup Tip #2:** Use cloud services like Google Drive or OneDrive for automatic backups of important files!",
                "💾 **Backup Tip #3:** Test your backups regularly - a backup you can't restore is useless!",
                "💾 **Backup Tip #4:** Consider external hard drives for local backups in addition to cloud storage!",
                "💾 **Backup Tip #5:** Ransomware attacks in South Africa have increased - having backups is your best defense!"
            };
        }

        private List<string> GetFollowUpResponses()
        {
            if (string.IsNullOrEmpty(_currentTopic))
                return new List<string> { "What would you like me to explain more about? Try asking about passwords, phishing, or malware!" };

            return new List<string>
            {
                $"Let me explain more about {_currentTopic}! " + GetTopicSummary(_currentTopic),
                $"Sure! Here's more information about {_currentTopic}. " + GetTopicSummary(_currentTopic),
                $"I'm happy to elaborate on {_currentTopic}! " + GetTopicSummary(_currentTopic)
            };
        }

        private string GetTopicSummary(string topic)
        {
            return topic.ToLower() switch
            {
                "password" => "Always use unique, complex passwords and consider a password manager.",
                "phishing" => "Always verify sender addresses and never click suspicious links.",
                "malware" => "Keep antivirus software updated and avoid unknown downloads.",
                "2fa" => "Use authenticator apps instead of SMS for better security.",
                "privacy" => "Review social media settings and limit shared personal information.",
                "safe browsing" => "Look for HTTPS and keep your browser updated.",
                "Wifi Public" => "Use VPN on public networks and disable auto-connect.",
                "backup" => "Follow the 3-2-1 backup strategy for data protection.",
                _ => "Stay vigilant and follow cybersecurity best practices!"
            };
        }

        private List<string> GetAnotherTipResponses()
        {
            if (string.IsNullOrEmpty(_currentTopic))
                return new List<string> { "What topic would you like another tip about? Try 'password', 'phishing', or 'malware'!" };

            return new List<string>
            {
                GetRandomTipForTopic(_currentTopic),
                GetRandomTipForTopic(_currentTopic),
                GetRandomTipForTopic(_currentTopic)
            };
        }

        private string GetRandomTipForTopic(string topic)
        {
            var tips = topic.ToLower() switch
            {
                "password" => GetPasswordResponses(),
                "phishing" => GetPhishingResponses(),
                "malware" => GetMalwareResponses(),
                "2fa" => GetTwoFactorResponses(),
                "privacy" => GetPrivacyResponses(),
                "safe browsing" => GetSafeBrowsingResponses(),
                "Public WiFi" => GetWifiResponses(),
                "backup" => GetBackupResponses(),
                _ => new List<string> { "Stay safe online by following cybersecurity best practices!" }
            };
            return tips[_random.Next(tips.Count)];
        }

        private string GetFollowUpOnTopic(string topic)
        {
            return GetRandomTipForTopic(topic);
        }

        private List<string> GetHelpResponses()
        {
            return new List<string>
            {
                "📚 **Topics I can help with:**\n• 🔐 Password Safety\n• 🎣 Phishing Prevention\n• 🦠 Malware Protection\n• 🔑 Two-Factor Authentication (2FA)\n• 🔒 Privacy Protection\n• 🌐 Safe Browsing\n• 📶 Public WiFi Security\n• 💾 Data Backup\n\nJust ask me about any of these topics! Type 'exit' when you're done.",
                "💡 **Here's what I know about:**\n\n**Passwords** - Creating strong, unique passwords\n**Phishing** - Spotting fake emails and scams\n**Malware** - Protecting against viruses\n**2FA** - Extra account security\n**Privacy** - Protecting personal info\n\nWhat would you like to learn about?",
                "🛡️ **Cybersecurity Topics:**\n\n1. Password Safety\n2. Phishing Scams\n3. Malware Protection\n4. Two-Factor Authentication\n5. Privacy Settings\n6. Safe Browsing\n7. Public WiFi\n8. Data Backup\n\nType any topic to get tips!"
            };
        }

        private List<string> GetThanksResponses(string name)
        {
            return new List<string>
            {
                $"You're welcome, {name}! Stay safe online! 🛡️",
                $"My pleasure, {name}! Remember, cybersecurity is everyone's responsibility!",
                $"Glad I could help, {name}! Feel free to ask more questions anytime!",
                $"Anytime, {name}! Together we can make the internet safer! 💪"
            };
        }

        private List<string> GetExitResponses(string name)
        {
            return new List<string>
            {
                $"Goodbye {name}! Stay vigilant and keep your data secure! 🔒",
                $"See you later {name}! Remember to use strong passwords and enable 2FA! 👋",
                $"Take care {name}! Don't forget to backup your important files! 💾",
                $"Farewell {name}! Stay safe in the digital world! 🌍"
            };
        }

        private string GetDefaultResponse()
        {
            string[] defaults = {
                "I'm not sure I understand. Could you ask about cybersecurity topics like passwords, phishing, or safe browsing?",
                "Hmm, I didn't catch that. Try asking me about password safety or how to spot phishing emails!",
                "I'm still learning! Try asking about cybersecurity topics like 2FA, malware, or public WiFi safety.",
                "Let's focus on cybersecurity! Ask me about protecting your accounts or staying safe online.",
                "I can help with passwords, phishing, malware, 2FA, privacy, and more! What would you like to know?"
            };
            return defaults[_random.Next(defaults.Length)];
        }

        public string GetCurrentTopic() => _currentTopic;
        public void SetCurrentTopic(string topic) => _currentTopic = topic;

        #endregion
    }
}
