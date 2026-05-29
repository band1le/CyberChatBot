using System;
using System.Collections.Generic;
using System.Text;

namespace CyberChat.Services
{
   
    
        public class ConversationFlow
        {
            private Queue<string> _conversationHistory;
            private string _pendingTopic;
            private bool _awaitingFollowUp;

            public ConversationFlow()
            {
                _conversationHistory = new Queue<string>();
                _awaitingFollowUp = false;
            }

            public void AddToHistory(string message)
            {
                _conversationHistory.Enqueue(message);
                if (_conversationHistory.Count > 10)
                    _conversationHistory.Dequeue();
            }

            public string GetLastUserMessage()
            {
                // Logic to get last user message
                return null;
            }

            public void SetPendingTopic(string topic)
            {
                _pendingTopic = topic;
                _awaitingFollowUp = true;
            }

            public string GetPendingTopic()
            {
                _awaitingFollowUp = false;
                return _pendingTopic;
            }

            public bool IsAwaitingFollowUp() => _awaitingFollowUp;

            public void ResetFollowUp()
            {
                _awaitingFollowUp = false;
                _pendingTopic = null;
            }

            public bool IsFollowUpRequest(string input)
            {
                var followUpKeywords = new[] { "more", "another", "elaborate", "explain more", "tell me more", "continue" };
                foreach (var keyword in followUpKeywords)
                {
                    if (input.ToLower().Contains(keyword))
                        return true;
                }
                return false;
            }
        }
    }

