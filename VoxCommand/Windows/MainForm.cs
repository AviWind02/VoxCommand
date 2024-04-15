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
using VoxCommand.API_Class;
using VoxCommand.Other_Class;
using VoxCommand.Speech_Class;
using static System.Net.Mime.MediaTypeNames;

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
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, filename);

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
            SpeechRecognition speechRecognition = new SpeechRecognition();

            muteInput = !muteInput;

            // Update button background color based on mute state
            buttonMute.BackColor = muteInput ? Color.Orange : Color.White;
            buttonMute.Text = muteInput ? "Unmute" : "Mute";

            // Assuming you have an existing instance of SpeechRecognition
            // Update its mute state
            speechRecognition.muteSpeech(muteInput);  // Make sure you have such a method in SpeechRecognition
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            Environment.Exit(1);
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            richTextBoxLog.Clear();
        }

        private async void buttonTest_Click(object sender, EventArgs e)
        {
            //string a = await new WeatherServices().GetFormattedWeatherDataAsync("OrangeVille");
            //await new OpenAiService().SummarizeWeatherAsync(a);

            //string a = await new NewsService().FetchTopHeadlinesAsync();
            //await new OpenAiService().SummarizeNewsAsync(a);NewsScraper_Headlines()
            //await new OpenAiService().SummarizeNewsAsync(combinedDetails);
        }
    }
}
