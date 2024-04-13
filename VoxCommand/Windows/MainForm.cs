using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoxCommand.MediaControl_Class;
using VoxCommand.Misx_Class;
using VoxCommand.News_Class;
using VoxCommand.Other_Class;
using VoxCommand.Speech_Class;

namespace VoxCommand
{
    public partial class MainForm : Form
    {
        private bool muteInput = false;

        public MainForm()
        {
            InitializeComponent();
            //buttonMute.BackColor = Color.White;
            //RichTextBoxStreamWriter richTextBoxStreamWriter = new RichTextBoxStreamWriter(richTextBoxLog);
            //Console.SetOut(richTextBoxStreamWriter);
        }

        private void buttonSaveLog_Click(object sender, EventArgs e)
        {
            SaveRichTextBoxContent();

        }

        private void SaveRichTextBoxContent()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string filename = $"VoxCommand - {timestamp}.log";
            string path = Path.Combine(Application.StartupPath, filename);

            try
            {
                File.WriteAllText(path, richTextBoxLog.Text);
                MessageBox.Show($"Log saved to {path}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save log: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonMute_Click(object sender, EventArgs e)
        {
            Speech_recognition speechRecognition = new Speech_recognition();

            muteInput = !muteInput;

            // Update button background color based on mute state
            buttonMute.BackColor = muteInput ? Color.Orange : Color.White;
            buttonMute.Text = muteInput ? "Unmute" : "Mute";

            // Assuming you have an existing instance of Speech_recognition
            // Update its mute state
            speechRecognition.muteSpeech(muteInput);  // Make sure you have such a method in Speech_recognition
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            richTextBoxLog.Clear();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
                        new OpenAiService().SummarizeWeatherAsync("location: New York, date: 2024-04-15, temperature: 18°C, condition: Partly Cloudy, humidity: 55%, wind speed: 15 km/h, wind direction: NE, forecast: [date: 2024-04-16, high: 21°C, low: 10°C, condition: Rainy; date: 2024-04-17, high: 19°C, low: 9°C, condition: Cloudy; date: 2024-04-18, high: 22°C, low: 12°C, condition: Sunny]");

        }
    }
}
