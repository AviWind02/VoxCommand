# VoxCommand

## Description
This project is a voice-controlled command center that allows users to interact with their computer through voice commands. It supports various commands for managing system volume, media playback, news updates, and more. The application leverages System.Speech for speech recognition and synthesizes responses, providing an interactive and accessible way to control different functionalities.

## Features
- **Voice Activation**: Activate and interact with the system using custom wake words.
- **Media Control**: Play, pause, and navigate through media files.
- **Volume Management**: Adjust system volume through voice commands.
- **News Retrieval**: Get the latest news updates from CNN.
- **Extendable Commands**: Easily add more commands to the system.

## Prerequisites
- .NET Framework (Version specified by the developer)
- System.Speech library
- Microsoft Cognitive Services Speech SDK
- API keys for the following services:
  - OpenAI GPT-3
  - Microsoft Cognitive Services
  - News API
  - OpenWeatherMap API

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/voice-command-center.git

## Some Supported Commands
- "Hey Vox, what's the latest news?" - Fetches the latest news headlines.
- "What’s the weather like today?" - Provides current weather updates.
- "Increase volume to 50" - Sets the volume to 50%.
- "Play media" - Starts playing last media on system.
- "Show me my Steam games" - Displays Steam games library.

## APIs Used
- OpenAI GPT-3: Processes and summarizes news information.
- Microsoft Cognitive Services: Powers the speech recognition and text-to-speech functionalities.
- News API: Retrieves the latest news from various sources.
- OpenWeatherMap API: Provides current weather information.

### Acknowledgements
- System.Speech for enabling speech recognition and synthesis.
- Microsoft Cognitive Services for the advanced speech processing capabiliti
- OpenAI for advanced text interpretation and summarization.
- News API and OpenWeatherMap API for real-time data fetching.

### License
Distributed under the MIT License. See LICENSE for more information.
