using CyberChat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CyberChat.Services
{
    public class Sentiment_Analyzer
    {
        private readonly Dictionary<string, Sentiment> _keywordMap;

        public Sentiment_Analyzer()
        {
            _keywordMap = new Dictionary<string, Sentiment>(StringComparer.OrdinalIgnoreCase)
            {
                // Worried/Anxious
                { @"\b(worried|anxious|nervous|scared|fear|concerned|unsafe)\b", Sentiment.Worried },
                { @"\b(anxiety|stress|panic|terrified|afraid)\b", Sentiment.Anxious },
                
                // Frustrated
                { @"\b(frustrated|annoying|difficult|hard|confusing|struggling)\b", Sentiment.Frustrated },
                { @"\b(hate|terrible|awful|useless|waste)\b", Sentiment.Frustrated },
                
                // Curious
                { @"\b(curious|interested|want to learn|tell me|explain|how do i|what is)\b", Sentiment.Curious },
                { @"\b(learn|understand|knowledge|tips|advice)\b", Sentiment.Curious },
                
                // Confused
                { @"\b(confused|don't understand|not sure|unclear|what does)\b", Sentiment.Confused },
                { @"\b(huh|what|hmm|maybe|perhaps)\b", Sentiment.Confused },
                
                // Grateful
                { @"\b(thank|thanks|helpful|appreciate|useful|great)\b", Sentiment.Grateful },
                { @"\b(awesome|perfect|excellent|wonderful)\b", Sentiment.Grateful },
                
                // Confident
                { @"\b(i know|got it|understood|makes sense|clear)\b", Sentiment.Confident },
                { @"\b(confident|sure|definitely|certainly)\b", Sentiment.Confident }
            };
        }

        public Sentiment AnalyzeSentiment(string input)
        {
            foreach (var pattern in _keywordMap)
            {
                if (Regex.IsMatch(input, pattern.Key, RegexOptions.IgnoreCase))
                {
                    return pattern.Value;
                }
            }
            return Sentiment.Neutral;
        }

        public string GetEmpatheticResponse(Sentiment sentiment, string topic)
        {
            return sentiment switch
            {
                Sentiment.Worried => "It's completely normal to feel worried about online security. Let me share some practical tips that can help put your mind at ease. ",
                Sentiment.Anxious => "I understand this can feel overwhelming. Take a deep breath - let's break this down into simple, easy steps together. ",
                Sentiment.Frustrated => "I hear your frustration. Cybersecurity can feel complicated, but I'll explain it in simple terms that are easy to follow. ",
                Sentiment.Confused => "I can see this is confusing. Let me explain it differently to help make it clearer for you. ",
                Sentiment.Curious => "It's great that you're curious about cybersecurity! That's the first step to staying safe online. ",
                Sentiment.Grateful => "You're very welcome! I'm glad you found that helpful. ",
                Sentiment.Confident => "Excellent! You're building good cybersecurity habits. ",
                _ => "Here's some helpful information for you. "
            };
        }

        public string GetEncouragement(Sentiment sentiment)
        {
            return sentiment switch
            {
                Sentiment.Worried => "Remember, being aware of these risks means you're already ahead of most people!",
                Sentiment.Anxious => "Many people feel this way at first, but with practice, cybersecurity becomes second nature.",
                Sentiment.Frustrated => "Don't give up! Every expert was once a beginner too.",
                Sentiment.Confused => "Take your time - these concepts are new to many people.",
                Sentiment.Curious => "Keep asking questions! That's how we all learn.",
                Sentiment.Grateful => "Your positive attitude makes learning easier!",
                Sentiment.Confident => "You're becoming a cybersecurity expert!",
                _ => "You're doing great with your cybersecurity awareness!"
            };
        }
    }
}


