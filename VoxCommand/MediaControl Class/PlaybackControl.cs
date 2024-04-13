﻿using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using VoxCommand.Other_Class;
using VoxCommand.Speech_Class;

//GPT Thank you :)
namespace VoxCommand.MediaControl_Class
{
    internal class PlaybackControl
    {
        // Import the SendInput function from user32.dll to simulate key presses
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        // Constants for key event flags
        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001; // Key down flag
        private const uint KEYEVENTF_KEYUP = 0x0002; // Key up flag

        // Virtual key codes for the media keys
        private const uint VK_MEDIA_PLAY_PAUSE = 0xB3; // Play/Pause Media key
        private const uint VK_MEDIA_NEXT_TRACK = 0xB0; // Next Track Media key
        private const uint VK_MEDIA_PREV_TRACK = 0xB1; // Previous Track Media key

        // Struct to hold information about a single input event
        struct INPUT
        {
            public int type; // Type of the input, 1 means keyboard input
            public InputUnion u; // Union to store input information
        }

        // Union structure to hold different types of input information
        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        // Structure to hold mouse input information (not used in this class but part of the union)
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        // Structure to hold keyboard input information
        struct KEYBDINPUT
        {
            public ushort wVk; // Virtual-key code for the key
            public ushort wScan; // Hardware scan code for the key
            public uint dwFlags; // Flags specifying various aspects of key press
            public uint time; // Time stamp for the event
            public IntPtr dwExtraInfo; // Extra information associated with the key press
        }

        // Structure to hold hardware input information (not used in this class but part of the union)
        struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        // Method to simulate pressing the Play/Pause media key
        public void PlayPauseMedia()
        {
            INPUT input = new INPUT
            {
                type = 1, // Keyboard input
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = (ushort)VK_MEDIA_PLAY_PAUSE,
                        dwFlags = KEYEVENTF_EXTENDEDKEY
                    }
                }
            };

            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));

            input.u.ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));
        }

        // Method to simulate pressing the Next Track media key
        public void NextTrackMedia()
        {
            INPUT input = new INPUT
            {
                type = 1,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = (ushort)VK_MEDIA_NEXT_TRACK,
                        dwFlags = KEYEVENTF_EXTENDEDKEY
                    }
                }
            };

            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));

            input.u.ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));
        }

        // Method to simulate pressing the Previous Track media key
        public void PreviousTrackMedia()
        {
            INPUT input = new INPUT
            {
                type = 1,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = (ushort)VK_MEDIA_PREV_TRACK,
                        dwFlags = KEYEVENTF_EXTENDEDKEY
                    }
                }
            };

            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));

            input.u.ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
            SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void AdjustMediaBasedOnCommand(string command)
        {
            PlaybackControl playbackControl = new PlaybackControl();


            if (command.Contains("play media") || command.Contains("pause media"))
            {
                playbackControl.PlayPauseMedia();
                Speech_recognition.synthesizer.Speak("Play/Pausing Media.");
                Console.WriteLine("Play/Pausing Media.");
            }
            else if (command.Contains("next song") || command.Contains("skip song"))
            {
                playbackControl.NextTrackMedia();
                Speech_recognition.synthesizer.Speak("Playing Next Track.");
                Console.WriteLine("Playing Next Track.");
            }
            else if (command.Contains("previous song"))
            {
                playbackControl.PreviousTrackMedia();
                Speech_recognition.synthesizer.Speak("Playing Previous Track.");
                Console.WriteLine("Playing Previous Track.");
            }
            else
            {
                Speech_recognition.synthesizer.Speak("Could not determine the audio change action required.");
                return;
            }


        }
    }
}

