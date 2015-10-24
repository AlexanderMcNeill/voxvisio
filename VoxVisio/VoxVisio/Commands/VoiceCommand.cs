using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WindowsInput;
using WindowsInput.Native;
using VoxVisio.Singletons;

namespace VoxVisio.Commands
{
    public class VoiceCommand : Command
    {
        public string VoiceKeyword { set; get; }
        public KeyCombo keyCombo { set; get; }

        private string keyStrings;

        public VoiceCommand(string commandWord, string keyStrings, InputSimulator inputSimulator)
        {
            this.keyStrings = keyStrings;
            this.VoiceKeyword = commandWord;
            this.keyCombo = new KeyCombo(keyStrings, inputSimulator);
        }

        public VoiceCommand(JObject JsonData)
        {
            LoadFromJson(JsonData);
        }

        public Dictionary<string, string> getDict()
        {
            var toReturn = new Dictionary<string, string>
            {
                {"voice keyword", VoiceKeyword},
                {"keys", keyCombo.GetKeyString()}
            };
            return toReturn;
        }

        public void RunCommand()
        {
            if ((keyCombo.GetKeyString().Contains("LButton") || keyCombo.GetKeyString().Contains("RButton")) && SettingsSingleton.Instance().ZoomEnabled)
            {
                SharedFormsSingleton.Instance().zoomForm.startZoomClick(keyCombo);
            }
            else
            {
                keyCombo.PressKeys();
            }
        }

        public void PressKeys()
        {
            keyCombo.PressKeys();
        }

        public void LoadFromJson(JObject jsonData)
        {
            this.VoiceKeyword = (string)jsonData["voice keyword"];
            this.keyStrings = (string)jsonData["keys"];
            this.keyCombo = new KeyCombo((string)jsonData["keys"], SharedObjectsSingleton.Instance().inputSimulator);
        }

        public JObject SaveToJson()
        {
            JObject toReturn = new JObject();
            toReturn["voice keyword"] = VoiceKeyword;
            toReturn["keys"] = keyCombo.GetKeyString();
            toReturn["command type"] = GetCommandType().ToString();
            return toReturn;
        }

        public string GetKeyWord()
        {
            return VoiceKeyword;
        }

        public string GetKeyStrings()
        {
            return keyStrings;
        }

        public eCommandType GetCommandType()
        {
            return eCommandType.VoiceCommand;
        }
    }
}
