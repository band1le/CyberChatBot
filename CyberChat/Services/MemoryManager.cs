using CyberChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberChat.Services
{
    public class MemoryManager
    {
        private UserProfile _userProfile;
        private Dictionary<string, string> _conversationContext;
        private List<string> _recentTopics;

        public MemoryManager()
        {
            _userProfile = new UserProfile();
            _conversationContext = new Dictionary<string, string>();
            _recentTopics = new List<string>();
        }

        public void SetUserName(string name)
        {
            _userProfile.Name = name;
        }

        public string GetUserName()
        {
            return _userProfile.Name ?? "User";
        }

        public void RememberInterest(string topic)
        {
            _userProfile.UpdateInterest(topic);
            _conversationContext["last_topic"] = topic;
            _recentTopics.Add(topic);

            // Keep only last 5 topics
            if (_recentTopics.Count > 5)
                _recentTopics.RemoveAt(0);
        }

        public void SetFavoriteTopic(string topic)
        {
            _userProfile.FavoriteTopic = topic;
        }

        public string GetFavoriteTopic()
        {
            return _userProfile.FavoriteTopic;
        }

        public string GetLastTopic()
        {
            return _conversationContext.ContainsKey("last_topic") ? _conversationContext["last_topic"] : null;
        }

        public void IncrementQuestions()
        {
            _userProfile.QuestionsAsked++;
        }

        public int GetQuestionCount()
        {
            return _userProfile.QuestionsAsked;
        }

        public string GetPersonalizedGreeting()
        {
            if (!string.IsNullOrEmpty(_userProfile.FavoriteTopic))
            {
                return $"I remember you're interested in {_userProfile.FavoriteTopic}! Would you like to learn more about that today?";
            }
            return null;
        }

        public void RememberSentiment(Sentiment sentiment)
        {
            _userProfile.CurrentSentiment = sentiment;
            _conversationContext["last_sentiment"] = sentiment.ToString();
        }

        public Sentiment GetLastSentiment()
        {
            return _userProfile.CurrentSentiment;
        }

        public bool HasMetBefore()
        {
            return !string.IsNullOrEmpty(_userProfile.Name);
        }

        public List<string> GetInterests()
        {
            return _userProfile.Interest;
        }
    }
}
