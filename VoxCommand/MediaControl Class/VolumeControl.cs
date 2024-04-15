using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using VoxCommand.Speech_Class;

namespace VoxCommand.Other_Class
{
    internal class VolumeControl
    {
        private CoreAudioDevice defaultPlaybackDevice;


        public VolumeControl()
        {
            var controller = new CoreAudioController();
            defaultPlaybackDevice = controller.DefaultPlaybackDevice; 
            Console.WriteLine("Default playback device initialized.");
        }

        // Sets the volume level to a specific percentage.
        public void SetVolume(double volume)
        {
            // Validate the input volume range.
            if (volume < 0 || volume > 100)
            {
                Console.WriteLine($"Attempted to set volume outside valid range: {volume}%. Volume must be between 0 and 100.");
                return;
            }

            // Set the volume asynchronously and log the action.
            defaultPlaybackDevice.SetVolumeAsync(volume).Wait();
            Console.WriteLine($"Volume set to: {volume}%.");
        }

        // Gets the current volume level from the default playback device.
        public double GetVolume()
        {
            double volume = defaultPlaybackDevice.Volume;
            Console.WriteLine($"Current volume: {volume}%.");
            return volume;
        }

        // Decreases the volume by 10 units.
        public void LowerVolume()
        {
            double currentVolume = GetVolume();
            SetVolume(currentVolume - 10);
        }

        // Increases the volume by 10 units.
        public void HigherVolume()
        {
            double currentVolume = GetVolume(); 
            SetVolume(currentVolume + 10);
        }

        // Mutes or unmutes the volume based on the boolean parameter.
        public void MuteVolume(bool mute)
        {
            defaultPlaybackDevice.SetMuteAsync(mute).Wait(); 
            Console.WriteLine($"Mute state set to: {mute}.");
        }
        public static void AdjustVolumeBasedOnCommand(string command)
        {
            VolumeControl volumeControl = new VolumeControl();

            // Extract the first number from the command
            var match = Regex.Match(command, @"\d+");
            int volumeChangePercentage = match.Success ? int.Parse(match.Value) : 10; // Default to 10 if no number found

            // Retrieve current volume
            double currentVolume = volumeControl.GetVolume();

            // Calculate new volume based on the command
            double newVolume = 0;
            if (command.Contains("increase volume"))
            {
                newVolume = currentVolume + (currentVolume * (volumeChangePercentage / 100.0));
                newVolume = Math.Min(newVolume, 100); // Ensure the volume does not exceed 100%
                SpeechRecognition.synthesizer.Speak($"Increasing volume by {volumeChangePercentage}%, new volume: {Math.Round(newVolume)}%.");
            }
            else if (command.Contains("decrease volume"))
            {
                newVolume = currentVolume - (currentVolume * (volumeChangePercentage / 100.0));
                newVolume = Math.Max(newVolume, 0); // Ensure the volume does not go below 0%
                SpeechRecognition.synthesizer.Speak($"Decreasing volume by {volumeChangePercentage}%, new volume: {Math.Round(newVolume)}%.");
            }
            else if(command.Contains("set volume"))
            {
                SpeechRecognition.synthesizer.Speak($"Setting volume to {volumeChangePercentage}%.");
                newVolume = volumeChangePercentage;
            }
            else if (command.Contains("mute volume"))
            {
                if(volumeControl.defaultPlaybackDevice.IsMuted)
                {
                    SpeechRecognition.synthesizer.Speak($"Volume already muted.");
                    return;
                }

                SpeechRecognition.synthesizer.Speak($"muting volume.");
                volumeControl.MuteVolume(true);
            }
            else if (command.Contains("unmute volume"))
            {
                if (!volumeControl.defaultPlaybackDevice.IsMuted)
                {
                    SpeechRecognition.synthesizer.Speak($"Volume already unmuted.");
                    return;
                }
                SpeechRecognition.synthesizer.Speak($"unmuting volume.");
                volumeControl.MuteVolume(false);
            }
            else
            {
                SpeechRecognition.synthesizer.Speak("Could not determine the volume change action required.");
                Console.WriteLine("No action (increase/decrease) specified in the command.");
                return;
            }

            // Set the new volume
            volumeControl.SetVolume(newVolume);

        }
    }
}
