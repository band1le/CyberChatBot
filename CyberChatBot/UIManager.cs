using System;
using System.Threading;


namespace CyberChatBot
{
    public class UIManager
    {
        public void DisplayLogo()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string[] logo = {
                @"    ╔═══════════════════════════════════════════════════════════════╗",
                @"    ║   ██████╗██╗   ██╗██████╗ ███████╗██████╗     ██████╗  ██████╗ ████████╗    ║",
                @"    ║  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗    ██╔══██╗██╔═══██╗╚══██╔══╝    ║",
                @"    ║  ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝    ██████╔╝██║   ██║   ██║       ║",
                @"    ║  ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗    ██╔══██╗██║   ██║   ██║       ║",
                @"    ║  ╚██████╗   ██║   ██████╔╝███████╗██║  ██║    ██████╔╝╚██████╔╝   ██║       ║",
                @"    ║   ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝    ╚═════╝  ╚═════╝    ╚═╝       ║",
                @"    ╚═══════════════════════════════════════════════════════════════╝",
                @"",
                @"    ███████╗███████╗ ██████╗██╗   ██╗██████╗ ██╗████████╗██╗   ██╗",
                @"    ██╔════╝██╔════╝██╔════╝╚██╗ ██╔╝██╔══██╗██║╚══██╔══╝╚██╗ ██╔╝",
                @"    ███████╗█████╗  ██║      ╚████╔╝ ██████╔╝██║   ██║    ╚████╔╝ ",
                @"    ╚════██║██╔══╝  ██║       ╚██╔╝  ██╔══██╗██║   ██║     ╚██╔╝  ",
                @"    ███████║███████╗╚██████╗   ██║   ██████╔╝██║   ██║      ██║   ",
                @"    ╚══════╝╚══════╝ ╚═════╝   ╚═╝   ╚═════╝ ╚═╝   ╚═╝      ╚═╝   ",
                @"",
                @"    🌍 Protecting South African Citizens Online 🛡️",
                @""
            };

            foreach (string line in logo)
            {
                Console.WriteLine(line);
                Thread.Sleep(10);
            }
            Console.ResetColor();
        }
        public void DisplayWelcomeBorder()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string border = new string('═', 70);
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine($"║{"Welcome to CyberChatBot!".PadLeft(40).PadRight(70)}║");
            Console.WriteLine($"╚{border}╝");
            Console.ResetColor();
        }
        public void DisplayPersonalizedWelcome(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("╔══ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"🌟 Welcome, {userName}! ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🌟");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();

            TypeWriterEffect($"Hello {userName}! I'm CyberGuardian, your dedicated cybersecurity awareness assistant. I'm here to help you stay safe online! 🛡️", 30);
            Console.WriteLine();
        }

        public void DisplayHelpMenu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("┌─────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                         💡 QUICK START GUIDE                         │");
            Console.WriteLine("├─────────────────────────────────────────────────────────────────────┤");
            Console.WriteLine("│                                                                     │");
            Console.WriteLine("│   You can ask me about:                                             │");
            Console.WriteLine("│   • 🔐 Password safety and management                               │");
            Console.WriteLine("│   • 🎣 Phishing scams and how to spot them                          │");
            Console.WriteLine("│   • 🌐 Safe browsing practices                                      │");
            Console.WriteLine("│   • 🦠 Malware and virus protection                                 │");
            Console.WriteLine("│   • 🔑 Two-Factor Authentication (2FA)                              │");
            Console.WriteLine("│   • 📱 Social media security tips                                   │");
            Console.WriteLine("│   • 📶 Public WiFi safety                                           │");
            Console.WriteLine("│   • 💾 Data backup strategies                                       │");
            Console.WriteLine("│                                                                     │");
            Console.WriteLine("│   Simply type your question or topic!                               │");
            Console.WriteLine("│   Type 'exit' when you're done.                                     │");
            Console.WriteLine("└─────────────────────────────────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void DisplayBotMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("🤖 CyberGuardian: ");
            Console.ResetColor();
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void DisplayPrompt(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(prompt);
            Console.ResetColor();
        }

        public void DisplayWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"⚠️  {message}");
            Console.ResetColor();
        }

        public void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"❌ ERROR: {message}");
            Console.ResetColor();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public void DisplayGoodbye(string userName)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║                    Thank You for Visiting, {userName}!                     ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║    Stay safe online! Remember: Cybersecurity is everyone's          ║");
            Console.WriteLine("║    responsibility. Be vigilant, stay informed, and protect          ║");
            Console.WriteLine("║    yourself and your loved ones.                                     ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║    🔒 Keep your digital life secure! 🔒                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void TypeWriterEffect(string message, int delayMs)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
        }
    }
}



