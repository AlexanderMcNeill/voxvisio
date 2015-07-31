using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace VoiceApiRequest
{
    public partial class Form1 : Form
    {
        private bool recording = false;
        private int timeSpentRecording = 0;


        public Form1()
        {
            InitializeComponent();

            richTextBox1.Text = RequestSpeechToText("test.flac");
        }

        public string RequestSpeechToText(string audioFile)
        {
            try
            {

                FileStream fileStream = File.OpenRead(audioFile);
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.SetLength(fileStream.Length);
                fileStream.Read(memoryStream.GetBuffer(), 0, (int)fileStream.Length);
                byte[] BA_AudioFile = memoryStream.GetBuffer();
                HttpWebRequest _HWR_SpeechToText = null;
                _HWR_SpeechToText =
                            (HttpWebRequest)HttpWebRequest.Create(
                                "https://www.google.com/speech-api/v2/recognize?output=json&lang=en-us&key=AIzaSyBiQkwh5zYOCRKEaZ-xUbBleL7yvGrVvqM");
                _HWR_SpeechToText.Credentials = CredentialCache.DefaultCredentials;
                _HWR_SpeechToText.Method = "POST";
                _HWR_SpeechToText.ContentType = "audio/x-flac; rate=44100";
                _HWR_SpeechToText.ContentLength = BA_AudioFile.Length;
                Stream stream = _HWR_SpeechToText.GetRequestStream();
                stream.Write(BA_AudioFile, 0, BA_AudioFile.Length);
                stream.Close();

                HttpWebResponse HWR_Response = (HttpWebResponse)_HWR_SpeechToText.GetResponse();
                if (HWR_Response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader SR_Response = new StreamReader(HWR_Response.GetResponseStream());
                    String responseString =  SR_Response.ReadToEnd();
                    
                    
                    String[] responses = responseString.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    JObject responseObject = JObject.Parse(responses[1]);
                    JArray results = (JArray)responseObject["result"];
                    JObject firstResult = (JObject)results[0];
                    JArray alternatives = (JArray)firstResult["alternative"];
                    JObject firstOption = (JObject)alternatives[0];

                    return (string)firstOption["transcript"];
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "No response";
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
