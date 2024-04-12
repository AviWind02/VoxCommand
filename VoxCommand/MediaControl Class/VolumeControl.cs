using AudioSwitcher.AudioApi.CoreAudio;
using System;

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
    }
}
