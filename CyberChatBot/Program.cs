using System;
using System.Media;
using System.Threading;

namespace CyberChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CyberChatBot";
            Console.CursorVisible = false;

            var bot=new CyberChatBot();
            bot.Start();
        }
    }
}
