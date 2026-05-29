using System;
using System.Collections.Generic;
using System.Text;

namespace CyberChat.Models
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string FavoriteTopic { get; set; }
        public List<string> Interest { get; set; }
        public int QuestionsAsked { get; set; }
        public Sentiment CurrentSentiment { get; set; }

        public UserProfile()
        {
            Interest = new List<string>();
            CurrentSentiment = Sentiment.Neutral;
            QuestionsAsked = 0;
        }
        public void UpdateInterest(string topic)
        {
            if (!Interest.Contains(topic))
            {
                Interest.Add(topic);
            }
        }
    }

}
