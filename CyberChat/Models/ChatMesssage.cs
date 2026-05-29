using System;
using System.Collections.Generic;
using System.Text;

namespace CyberChat.Models
{
 
  public class ChatMessage
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsUser => Sender == "User";
        public ChatMessage(string sender, string message)
        {
            Sender = sender;
            Message = message;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm}] {Sender}: {Message}";
        }
    }
}

