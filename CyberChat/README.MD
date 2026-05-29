# 🛡️ CyberChatBot - Cybersecurity Awareness Chatbot (Part 2 - WPF)

## Student Information
- **Name:** Bandile Ngubane
- **Student Number:** [10479433]
- **Module:** PROG6221 - Programming 2A
- **Part:** Part 2 - WPF GUI Application

---

## GitHub Repository


---

## Video Presentation
[YouTube Video Link - Will be added after recording]

---

## Project Overview

CyberChatBot is a Windows Presentation Foundation (WPF) desktop application built inside the CyberChatBot solution under the project name "CyberChat". It educates South African citizens about cybersecurity through an interactive chatbot interface. The application features voice greeting on startup, keyword recognition for cybersecurity topics, random responses to keep conversations engaging, memory to recall user information within the same session, sentiment detection to respond empathetically, conversation flow for follow-up questions, and a professional graphical user interface with color-coded topic buttons for quick access.

The chatbot can discuss the following cybersecurity topics:
- **Password Safety** - Creating strong, unique passwords and using password managers
- **Phishing Scams** - How to spot fake emails, suspicious links, and scam attempts
- **Malware Protection** - Protecting against viruses, ransomware, and trojans
- **Two-Factor Authentication (2FA)** - Adding extra security layers to accounts
- **Privacy Protection** - Protecting personal information online and reviewing privacy settings
- **Public WiFi Safety** - Safe browsing practices on public networks

---

## Features Implemented

### ✅ GUI Design (WPF)
The application features a modern dark-themed interface with a dark blue background color code #1a1a2e. The ASCII art logo spells out "CyberChatBot" using the Consolas monospace font in cyan color (#4ECDC4) with fixed dimensions of 713 pixels wide and 149 pixels high. The text wrapping is disabled and spacing is preserved using xml:space="preserve" to ensure the ASCII art displays correctly without line breaks breaking the artwork. The chat display area is a ListBox with a light gray background (#ECF0F1) that shows all messages in a scrollable list and automatically scrolls to the newest message when added. The input area consists of a TextBox with a cyan border where users type their messages and a Send button with a cyan background. The Enter key is also supported for submitting messages. The window dimensions are 900 pixels wide by 800 pixels tall and it opens centered on the user's screen.

The topic buttons are arranged in a WrapPanel so they automatically flow to the next line if the window is resized. Each button uses a different color to visually distinguish topics:
- **Password Safety button** - Green background (#2ECC71)
- **Phishing button** - Red background (#E74C3C)
- **Malware button** - Orange background (#F39C12)
- **2FA button** - Purple background (#9B59B6)
- **Public WiFi button** - Teal background (#1ABC9C)
- **Help button** - Dark gray background (#34495E)
- **Exit button** - Light gray background (#95A5A6)

All buttons have white text, bold font, and consistent dimensions of approximately 100 to 140 pixels wide by 35 pixels tall.

### ✅ Keyword Recognition
The chatbot recognizes six cybersecurity keywords using regular expression pattern matching in the ResponseManager class. The ResponseManager maintains a dictionary called _responsePatterns that maps regex patterns to functions that return lists of possible responses. When a user types a message, the GetResponse method iterates through each pattern and uses Regex.IsMatch with RegexOptions.IgnoreCase to find a match.

The following keywords are recognized:
- **Password topics** - When the user types "password", "passphrase", or "pwd", the bot responds with password safety tips including using 12+ characters, mixing uppercase and lowercase letters, including numbers and symbols, never reusing passwords, using password managers, and enabling Two-Factor Authentication.
- **Phishing topics** - When the user types "phish", "phishing", "scam", "fraud", or "suspicious email", the bot provides phishing prevention advice including checking sender email addresses, hovering over links before clicking, looking for spelling mistakes and urgent language, and remembering that legitimate companies never ask for passwords via email.
- **Malware topics** - When the user types "malware", "virus", "ransomware", "trojan", or "spyware", the bot gives malware protection tips including installing reputable antivirus software, keeping operating systems updated, avoiding untrusted downloads, being careful with USB drives, and scheduling regular system scans.
- **2FA topics** - When the user types "2fa", "two factor", "mfa", "multi factor", or "authenticator", the bot explains Two-Factor Authentication including enabling it everywhere possible, using authenticator apps instead of SMS, saving backup codes securely, and understanding that South African banks now require 2FA.
- **Privacy topics** - When the user types "privacy", "private", "data protection", or "personal info", the bot shares privacy protection tips including reviewing social media privacy settings regularly, being careful about sharing birthdays and addresses online, using privacy-focused browsers, clearing browsing history and cookies, and using VPNs.
- **Public WiFi topics** - When the user types "public wifi" or "wifi hotspot", the bot offers public WiFi safety advice including avoiding sensitive transactions on public networks, using VPNs for encryption, disabling automatic WiFi connections, verifying official network names with staff, and using mobile data instead.

### ✅ Random Responses
The ResponseManager stores multiple response variations for each cybersecurity topic using List<string> collections. Each topic has five different tips stored in its own method. The GetPasswordResponses method returns a List<string> containing five different password safety tips. The GetPhishingResponses method returns five different phishing prevention tips. The GetMalwareResponses method returns five different malware protection tips. The GetTwoFactorResponses method returns five different Two-Factor Authentication tips. The GetPrivacyResponses method returns five different privacy protection tips. The GetWifiResponses method returns five different public WiFi safety tips.

Each time the user asks a question, the Random.Next method selects one response randomly from the appropriate list. The ResponseManager has a private Random instance called _random that is initialized in the constructor. When a pattern matches, the code calls `responses[_random.Next(responses.Count)]` which generates a random index between 0 and the number of responses minus one, then returns the tip at that index. This ensures conversations feel varied and not repetitive because the user gets a different tip each time they ask about the same topic.

### ✅ Conversation Flow
The ResponseManager maintains conversation context by storing the current topic in the _currentTopic variable whenever the user asks about a cybersecurity subject. When the GetPasswordResponses method is called, it sets `_currentTopic = "password"`. When GetPhishingResponses is called, it sets `_currentTopic = "phishing"`. This variable persists across multiple user messages.

When the user types follow-up phrases like "tell me more", "explain more", "more details", "elaborate", "continue", "another tip", "another one", or "give me another", the GetResponse method checks if `_currentTopic` is not null and if the input contains words like "more", "another", or "elaborate". If both conditions are true, it calls GetFollowUpOnTopic which returns another random tip from the same topic by calling GetRandomTipForTopic with the stored _currentTopic value. For example, after asking about passwords and receiving Tip #3, the user can say "tell me more" and receive Tip #1 or any other tip from the password list without having to retype the keyword "password". This creates a natural conversation flow.

Additionally, the ConversationFlow service maintains a queue of the last ten messages using Queue<string>. The AddToHistory method uses Enqueue to add each new message and if the count exceeds ten, it uses Dequeue to remove the oldest message, preserving only the most recent conversation context.

### ✅ Memory and Recall (Same Session)
The MemoryManager service stores user information throughout the conversation within the same session using the UserProfile class. The MemoryManager maintains three data structures: a UserProfile object containing all user data, a Dictionary<string, string> called _conversationContext for storing key-value pairs like the last discussed topic, and a List<string> called _recentTopics that stores the last five topics discussed.

The SetUserName method stores the user's name in the UserProfile after they provide it in the input box popup. The GetUserName method returns the stored name or defaults to "User" if no name was provided. The RememberInterest method calls _userProfile.UpdateInterest with the topic, which checks if the topic already exists in the Interest list using `if (!Interest.Contains(topic))` before adding to prevent duplicate entries. It then stores the topic in the _conversationContext dictionary under the key "last_topic" and adds the topic to the _recentTopics list, removing the oldest topic if more than five are stored.

The GetLastTopic method retrieves the most recently discussed topic from the _conversationContext dictionary. The GetPersonalizedGreeting method checks if the user has a favorite topic by examining `_userProfile.FavoriteTopic` and returns a personalized message like "I remember you're interested in {topic}! Would you like to learn more about that today?" The IncrementQuestions method increases the question count each time the user asks something using `_userProfile.QuestionsAsked++`, and GetQuestionCount returns this total which is displayed in the exit message box. The RememberSentiment method stores the user's current emotional state in both the UserProfile and the _conversationContext dictionary under the key "last_sentiment".

### ✅ Sentiment Detection
The Sentiment_Analyzer service detects the user's emotional state using a dictionary that maps regular expression patterns to Sentiment enum values. The dictionary is initialized in the constructor with the following pattern mappings:

- **Worried sentiment** - Patterns containing words like "worried", "anxious", "nervous", "scared", "fear", "concerned", or "unsafe" map to Sentiment.Worried. Additional patterns with "anxiety", "stress", "panic", "terrified", or "afraid" map to Sentiment.Anxious.
- **Frustrated sentiment** - Patterns with "frustrated", "annoying", "difficult", "hard", "confusing", or "struggling" map to Sentiment.Frustrated. Patterns with "hate", "terrible", "awful", "useless", or "waste" also map to Frustrated.
- **Curious sentiment** - Patterns with "curious", "interested", "want to learn", "tell me", "explain", "how do i", or "what is" map to Sentiment.Curious. Patterns with "learn", "understand", "knowledge", "tips", or "advice" also map to Curious.
- **Confused sentiment** - Patterns with "confused", "don't understand", "not sure", "unclear", or "what does" map to Sentiment.Confused. Patterns with "huh", "what", "hmm", "maybe", or "perhaps" also map to Confused.
- **Grateful sentiment** - Patterns with "thank", "thanks", "helpful", "appreciate", "useful", or "great" map to Sentiment.Grateful. Patterns with "awesome", "perfect", "excellent", or "wonderful" also map to Grateful.
- **Confident sentiment** - Patterns with "i know", "got it", "understood", "makes sense", or "clear" map to Sentiment.Confident. Patterns with "confident", "sure", "definitely", or "certainly" also map to Confident.

The AnalyzeSentiment method takes the user's input, iterates through each pattern in the dictionary using a foreach loop, and uses Regex.IsMatch with RegexOptions.IgnoreCase to check for matches. The first matching pattern found returns its associated Sentiment value. If no patterns match, it returns Sentiment.Neutral as the default.

The GetEmpatheticResponse method uses a switch statement to return appropriate caring responses based on the detected sentiment:
- For Worried users: "It's completely normal to feel worried about online security. Let me share some practical tips that can help put your mind at ease."
- For Anxious users: "I understand this can feel overwhelming. Take a deep breath - let's break this down into simple, easy steps together."
- For Frustrated users: "I hear your frustration. Cybersecurity can feel complicated, but I'll explain it in simple terms that are easy to follow."
- For Confused users: "I can see this is confusing. Let me explain it differently to help make it clearer for you."
- For Curious users: "It's great that you're curious about cybersecurity! That's the first step to staying safe online."
- For Grateful users: "You're very welcome! I'm glad you found that helpful."
- For Confident users: "Excellent! You're building good cybersecurity habits."

The GetEncouragement method provides additional motivational messages:
- For Worried users: "Remember, being aware of these risks means you're already ahead of most people!"
- For Anxious users: "Many people feel this way at first, but with practice, cybersecurity becomes second nature."
- For Frustrated users: "Don't give up! Every expert was once a beginner too."
- For Confused users: "Take your time - these concepts are new to many people."
- For Curious users: "Keep asking questions! That's how we all learn."
- For Grateful users: "Your positive attitude makes learning easier!"
- For Confident users: "You're becoming a cybersecurity expert!"

### ✅ Voice Greeting
The AudioManager service handles voice greeting playback when the application starts. In its constructor, it builds the file path to the greeting.wav file located in the Assets folder using `Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav")`. This ensures the path works correctly regardless of where the application is installed.

The PlayVoiceGreeting method first checks if the audio file actually exists at that location using `File.Exists(_audioFilePath)`. If found, it creates a SoundPlayer object inside a using statement, which ensures the SoundPlayer is properly disposed of after use even if an exception occurs. It then calls `soundPlayer.PlaySync()` which plays the audio synchronously, meaning the program waits for the greeting to finish before continuing. This prevents the audio from being cut off by subsequent program execution.

If the file is not found or any exception occurs, the catch block uses `System.Diagnostics.Debug.WriteLine` to write the error message to the Visual Studio output window for debugging purposes, but does not crash the application. This ensures users without audio files can still use the chatbot normally.

### ✅ Error Handling
The chatbot includes graceful error handling throughout the application to ensure it never crashes from unexpected user input. The GetResponse method in ResponseManager first checks if the user input is empty or whitespace using `string.IsNullOrWhiteSpace(input)` and returns a default response asking the user to say something instead of crashing or doing nothing.

If the user types something the bot does not recognize, the GetDefaultResponse method returns one of five default messages selected randomly using `_random.Next(defaults.Length)`. These default messages include: "I'm not sure I understand. Could you ask about cybersecurity topics like passwords, phishing, or safe browsing?" and "Hmm, I didn't catch that. Try asking me about password safety or how to spot phishing emails!" This ensures the user always gets a helpful response rather than an error.

The AudioManager catches any exceptions during audio playback using a try-catch block and writes debug output without crashing the application. The AskForName method handles empty name input by checking `string.IsNullOrWhiteSpace(input)` and defaulting to "User" as the name if the user does not provide one. The TopicButton_Click handler safely extracts the topic text using `topic.Split(' ')[1]` with conditional checks for multi-word topics like "Public WiFi" to prevent index-out-of-range errors.

---

## Code Structure

The project follows a modular architecture with clear separation of concerns using dedicated folders for Models and Services. The Models folder contains data structures that represent chat messages, user profiles, and sentiment types. The Services folder contains all business logic including response generation, memory management, sentiment analysis, conversation flow tracking, and audio playback. The Assets folder stores the greeting.wav audio file. The MainWindow.xaml file defines the graphical user interface layout using XAML, while MainWindow.xaml.cs handles user interactions and event processing using C#.

### Models Folder (Data Structures)

**ChatMessage.cs** - Represents a single message in the chat conversation. It contains four properties: Sender (string) which is either "User" or "Bot", Message (string) which is the actual message text content, Timestamp (DateTime) which records when the message was sent, and IsUser (bool) which returns true when Sender equals "User". The constructor takes sender and message parameters and sets Timestamp to DateTime.Now. The ToString method is overridden to display messages in the format "[HH:mm] Sender: Message" for debugging purposes.

**Sentiment.cs** - Defines an enumeration of all possible emotional states the chatbot can detect from user input. The enum values include Worried for users expressing fear or concern about security, Curious for users eager to learn more, Frustrated for users struggling to understand concepts, Confused for users who do not comprehend the information, Grateful for users who appreciate the help, Neutral for no strong emotion, Confident for users who understand clearly, and Anxious for users who feel nervous or stressed. This enumeration allows the bot to respond appropriately to the user's emotional state using strongly typed values.

**UserProfile.cs** - Stores all persistent information about the current user throughout the conversation session. The properties include Name (string) for the user's name, FavoriteTopic (string) for the topic the user asks about most frequently, Interest (List<string>) for topics the user has shown curiosity about, QuestionsAsked (int) for the total number of questions the user has asked, and CurrentSentiment (Sentiment) for the user's current emotional state. The constructor initializes Interest as a new empty List<string>, sets CurrentSentiment to Sentiment.Neutral, and QuestionsAsked to 0. The UpdateInterest method takes a topic string as input, checks if the topic already exists in the Interest list using `if (!Interest.Contains(topic))`, and adds it only if it does not already exist, preventing duplicate entries.

### Services Folder (Business Logic)

**AudioManager.cs** - Handles voice greeting playback when the application starts. The class contains a private readonly string field called _audioFilePath. The constructor builds the file path to the greeting.wav file located in the Assets folder using `Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav")`. The PlayVoiceGreeting method uses a try-catch block for error handling. It first checks if the audio file exists at the calculated path using `File.Exists(_audioFilePath)`. If found, it creates a SoundPlayer object inside a using statement to ensure proper disposal, then calls `PlaySync()` to play the audio synchronously. If the file is not found or an exception occurs, the catch block writes the error message to the debug output using `System.Diagnostics.Debug.WriteLine` but does not crash the application.

**ResponseManager.cs** - The brain of the chatbot that handles all keyword detection and response generation. The class contains a Dictionary called _responsePatterns that maps regular expression patterns to Func delegates that return lists of string responses. The dictionary is initialized in the constructor with patterns for greetings, password topics, phishing topics, malware topics, 2FA topics, privacy topics, safe browsing, public WiFi, backup, follow-up questions, help, thanks, and exit commands.

The GetResponse method is the main entry point. It first checks if the input is null or whitespace using `string.IsNullOrWhiteSpace` and returns a default response if so. It then stores the current topic from the parameter for follow-up purposes. It iterates through each pattern in the _responsePatterns dictionary using a foreach loop and uses `Regex.IsMatch(input, pattern.Key, RegexOptions.IgnoreCase)` to check for matches. When a match is found, it calls the associated function to get a List of responses, then uses `_random.Next(responses.Count)` to select a random index and returns that response.

If no pattern matches but the current topic is not null and the input contains "more", "another", or "elaborate", it calls GetFollowUpOnTopic which returns another random tip on the current topic. If nothing matches at all, it calls GetDefaultResponse which returns one of five default messages selected randomly.

The response collection methods each return a List of five different tips. GetPasswordResponses returns tips about using 12+ characters, mixing cases and symbols, never reusing passwords, using password managers, enabling 2FA, and avoiding personal information. GetPhishingResponses returns tips about checking sender addresses, hovering over links, spotting urgent language, legitimate company behavior, and South African phishing statistics. GetMalwareResponses returns tips about antivirus software, system updates, untrusted downloads, USB drive safety, and regular scanning. GetTwoFactorResponses returns tips about enabling 2FA everywhere, using authenticator apps, saving backup codes, South African banking requirements, and 2FA as an extra layer. GetPrivacyResponses returns tips about reviewing social media settings, limiting shared personal information, using privacy-focused browsers, clearing browsing data, and using VPNs. GetWifiResponses returns tips about avoiding sensitive transactions, using VPNs, disabling auto-connect, verifying network names, and using mobile data.

**MemoryManager.cs** - Manages user memory throughout the conversation within the same session. The class contains three private fields: a UserProfile object called _userProfile for storing user data, a Dictionary called _conversationContext for storing key-value pairs like the last discussed topic, and a List called _recentTopics for storing the last five topics discussed.

The constructor initializes _userProfile as a new UserProfile, _conversationContext as a new Dictionary, and _recentTopics as a new List. The SetUserName method stores the user's name in _userProfile.Name. The GetUserName method returns _userProfile.Name or defaults to "User" if null. The RememberInterest method calls _userProfile.UpdateInterest with the topic, stores the topic in _conversationContext under the key "last_topic", adds the topic to _recentTopics, and removes the oldest topic if the count exceeds five using `_recentTopics.RemoveAt(0)`.

The GetLastTopic method checks if _conversationContext contains the key "last_topic" and returns the value if found, otherwise returns null. The GetPersonalizedGreeting method checks if _userProfile.FavoriteTopic is not null or empty and returns a personalized message string. The IncrementQuestions method increases _userProfile.QuestionsAsked by one. The GetQuestionCount method returns _userProfile.QuestionsAsked. The RememberSentiment method stores the sentiment in _userProfile.CurrentSentiment and in _conversationContext under the key "last_sentiment". The GetLastSentiment method returns _userProfile.CurrentSentiment.

**Sentiment_Analyzer.cs** - Detects the user's emotional state from their text input to enable empathetic responses. The class contains a private Dictionary called _keywordMap that maps regular expression patterns to Sentiment enum values. The dictionary is initialized in the constructor with patterns for Worried, Anxious, Frustrated, Curious, Confused, Grateful, and Confident sentiments.

The AnalyzeSentiment method takes a string input, iterates through each KeyValuePair in the _keywordMap dictionary, and uses `Regex.IsMatch(input, pattern.Key, RegexOptions.IgnoreCase)` to check for matches. The first matching pattern found returns its associated Sentiment value. If no patterns match, it returns Sentiment.Neutral.

The GetEmpatheticResponse method takes a Sentiment and a topic string and uses a switch expression to return an appropriate empathetic response. For Sentiment.Worried it returns a message acknowledging concern and offering practical tips. For Sentiment.Anxious it validates feelings and offers to break things down. For Sentiment.Frustrated it acknowledges difficulty and promises simpler explanations. For Sentiment.Confused it offers to explain differently. For Sentiment.Curious it praises interest as the first step. For Sentiment.Grateful it expresses warmth. For Sentiment.Confident it offers encouragement. For all other sentiments it returns a neutral helpful message.

The GetEncouragement method takes a Sentiment and returns a motivational message. For Worried it reminds that awareness puts users ahead. For Anxious it says practice makes perfect. For Frustrated it reminds that every expert was once a beginner. For Confused it encourages taking time. For Curious it encourages asking questions. For Grateful it praises positive attitude. For Confident it says the user is becoming an expert. For other sentiments it gives general encouragement.

**ConversationFlow.cs** - Manages follow-up questions and maintains conversation context continuity. The class contains a Queue called _conversationHistory that stores the last ten messages, a string called _pendingTopic for tracking the current topic being discussed, and a bool called _awaitingFollowUp indicating whether the system is awaiting a follow-up response.

The AddToHistory method takes a message string and adds it to the queue using Enqueue. If the queue count exceeds ten, it removes the oldest message using Dequeue, keeping memory usage efficient. The IsFollowUpRequest method takes an input string, defines an array of follow-up keywords including "more", "another", "elaborate", "explain more", "tell me more", and "continue", then iterates through each keyword and returns true if the input contains any of them using `input.ToLower().Contains(keyword)`. The SetPendingTopic stores the provided topic in _pendingTopic and sets _awaitingFollowUp to true. The GetPendingTopic returns _pendingTopic and resets _awaitingFollowUp to false. The ResetFollowUp sets _awaitingFollowUp to false and clears _pendingTopic to null.

### User Interface

**MainWindow.xaml** - Defines the graphical user interface layout using XAML. The window has a title of "🛡️ CyberChatBot - Cybersecurity Chatbot", dimensions of 900 pixels wide by 800 pixels tall, a dark blue background color of #1a1a2e, and opens centered on the user's screen using WindowStartupLocation="CenterScreen". The layout uses a Grid with five row definitions: Row 0 is Auto height for the header, Row 1 is Auto height for the sentiment indicator, Row 2 is star height which takes all remaining available space for the chat display, Row 3 is Auto height for the input area, and Row 4 is Auto height for the topic buttons.

The header section has a Border with a dark navy background (#2C3E50), rounded corners of 10, and padding of 10. Inside the Border is a StackPanel containing the ASCII art TextBlock and the title TextBlocks. The ASCII art TextBlock uses FontFamily Consolas, FontSize 10, Foreground #4ECDC4, TextWrapping NoWrap, HorizontalAlignment Center, xml:space preserve, Height 149, and Width 713. The text property contains the ASCII art that spells "CyberChatBot" in large block letters. Below the ASCII art is a TextBlock with "🛡️ CYBERGUARDIAN" in white 20-point bold text, and another TextBlock with "Cybersecurity Awareness Assistant for South Africa" in lighter gray 12-point text.

The chat display area is a ListBox named ChatDisplay with a light gray background (#ECF0F1), margin of 10, and border thickness of 1. The ItemTemplate contains a DataTemplate with a TextBlock that binds to the message text, has text wrapping enabled, and a margin of 5. The ScrollViewer.HorizontalScrollBarVisibility is set to Disabled to prevent horizontal scrolling.

The input area is a Grid with two columns. The first column takes all available width, the second column is Auto sized. The TextBox named MessageInput has a height of 40, vertical content alignment centered, font size 14, white background, border brush #BDC3C7, and a KeyDown event attached. The Button named SendButton has a width of 80, height of 40, content "📤 Send", font size 14, font weight bold, background #3498DB, foreground white, and a Click event attached.

The topic buttons are arranged in a WrapPanel with horizontal alignment centered and margin of 10. Each button has a Click event attached to TopicButton_Click. The Password Safety button has content "🔐 Password Safety", width 140, height 35, background #2ECC71, foreground white, font weight bold. The Phishing button has content "🎣 Phishing", width 120, height 35, background #E74C3C. The Malware button has content "🦠 Malware", width 120, height 35, background #F39C12. The 2FA button has content "🔑 2FA", width 100, height 35, background #9B59B6. The Public WiFi button has content "📶 Public WiFi", width 130, height 35, background #1ABC9C. The Help button has content "❓ Help", width 100, height 35, background #34495E. The Exit button has content "❌ Exit", width 100, height 35, background #95A5A6.

**MainWindow.xaml.cs** - Handles all user interactions and contains the main application logic. The class contains private instances of ResponseManager, AudioManager, Sentiment_Analyzer, MemoryManager, and a string for _userName.

The constructor calls InitializeComponent to load the XAML layout, then initializes all service instances: _responseManager = new ResponseManager(), _audioManager = new AudioManager(), _sentimentAnalyzer = new Sentiment_Analyzer(), _memoryManager = new MemoryManager(). It then calls _audioManager.PlayVoiceGreeting() to play the welcome audio, and finally calls AskForName() to prompt the user for their name.

The AskForName method uses Microsoft.VisualBasic.Interaction.InputBox to display a popup dialog asking for the user's name. If the input is not null or whitespace, it trims the input, stores it in _userName, calls _memoryManager.SetUserName, and displays three welcome messages. It also checks for a personalized greeting using _memoryManager.GetPersonalizedGreeting() and displays it if available. If the user does not provide a name, it defaults to "User".

The AddMessage method takes a message string, adds it to the ChatDisplay.Items collection, and then calls ChatDisplay.ScrollIntoView with the last item to automatically scroll to show the newest message.

The SendUserMessage method is the core processing method. It adds the user's message to the chat display, then calls _memoryManager.IncrementQuestions() to count the question. It analyzes sentiment using _sentimentAnalyzer.AnalyzeSentiment, and if the sentiment is not Neutral, Curious, Grateful, or Confident, it gets empathetic prefix and encouragement from the sentiment analyzer and calls _memoryManager.RememberSentiment. It then detects topics in the user input using string.Contains checks for "password", "phish", "scam", "malware", "virus", "2fa", "privacy", and "wifi", calling _memoryManager.RememberInterest and _memoryManager.SetFavoriteTopic for each detected topic. It gets the last topic from memory, calls _responseManager.GetResponse with the user input, user name, and last topic, then stores the current topic in memory if not null. Finally, it combines the empathetic prefix, response, and encouragement and adds the complete message to the chat display.

The SendButton_Click event handler checks if the MessageInput text is not empty, calls SendUserMessage with the text, clears the input box, and sets focus back to it. The MessageInput_KeyDown event handler checks if the pressed key is Enter and calls SendButton_Click if so.

The TopicButton_Click event handler takes the button that was clicked, checks its content string, and sends the appropriate lowercase keyword: "password" for Password button, "phishing" for Phishing button, "malware" for Malware button, "2fa" for 2FA button, "public wifi" for WiFi button, "help" for Help button, and "exit" for Exit button.

The BtnHelp_Click event handler constructs a help message string listing all available topics with their emojis and descriptions, then adds this message to the chat display. The BtnExit_Click event handler gets the total question count from _memoryManager.GetQuestionCount(), displays a MessageBox with a goodbye message including the user's name and the total questions asked, then calls Application.Current.Shutdown() to close the application.

---

## Complete User Interaction Flow

When the user launches the application, the MainWindow constructor executes. It first calls InitializeComponent to load the XAML layout, then creates new instances of ResponseManager, AudioManager, Sentiment_Analyzer, and MemoryManager. It calls PlayVoiceGreeting to play the welcome audio, which uses SoundPlayer.PlaySync to play the greeting.wav file from the Assets folder. It then calls AskForName which displays an InputBox popup dialog asking for the user's name.

After the user enters their name, the bot displays three welcome messages personalized with the user's name: "Hello {name}! 👋 I'm CyberGuardian.", "I'm here to help you stay safe online!", and "Ask me about passwords, phishing, malware, or click the buttons below!". If the user has shown interest in a topic earlier in the same session, the bot also displays a personalized greeting like "I remember you're interested in privacy! Would you like to learn more about that today?"

The user can interact with the chatbot in three ways: typing a question in the text box and pressing Enter, clicking the Send button, or clicking one of the color-coded topic buttons. When the user sends a message, the system adds their message to the chat display, increments the question count, analyzes sentiment using regex pattern matching, and detects topics by checking for keywords like "password", "phishing", "malware", "2fa", "privacy", or "wifi". If the sentiment is negative or concerning, it adds an empathetic prefix and encouragement.

The ResponseManager then processes the user input by iterating through its regex patterns to find a match. If the user typed "password", it calls GetPasswordResponses which returns a list of five password tips, selects one randomly using Random.Next, and returns it. If the user then types "tell me more", the ResponseManager recognizes the follow-up request because _currentTopic is set to "password" and returns another random password tip. If the user types something the bot doesn't recognize, it returns a random default response from a list of five options.

After getting the response, the bot combines the empathetic prefix, the cybersecurity tip, and encouragement, then displays the complete message in the chat display. The chat display automatically scrolls to show the newest message. The user can continue asking questions, and the bot remembers their interests and favorite topics within the same session. When the user clicks the Exit button, the bot displays a MessageBox showing the total number of questions asked during the session and a goodbye message before closing the application.

---

## How to Run

### Prerequisites
- Windows Operating System (7, 8, 10, or 11)
- .NET 8.0 or higher SDK installed
- Visual Studio 2022, 2025, or 2026 (Community, Professional, or Enterprise edition)
- Speakers or headphones to hear the voice greeting (optional)

### References
I Coding Assistants

**DeepSeek AI** - DeepSeek (2026). *DeepSeek AI Assistant* [Large language model]. Hangzhou: DeepSeek Company. Available at: https://www.deepseek.com/

**ChatGPT (OpenAI)** - OpenAI (2026). *ChatGPT* [Large language model]. Available at: https://chat.openai.com/

### How These Sources Were Used

- **DeepSeek AI** - Provided comprehensive code structure guidance for the WPF application including the ResponseManager regex pattern matching system, MemoryManager for user profile storage, Sentiment_Analyzer for emotion detection, ConversationFlow for follow-up handling, AudioManager for voice playback using System.Media.SoundPlayer, and MainWindow.xaml.cs event handling with AskForName using InputBox.

- **ChatGPT (OpenAI)** - Assisted with conceptual explanations of C# WPF concepts including dependency properties, grid layout systems, list box data binding, event handlers for button clicks and key presses, proper disposal of SoundPlayer objects using using statements, and regex pattern construction for keyword matching