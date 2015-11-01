using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using VoxVisio.Screen_Overlay;
using VoxVisio.Singletons;

namespace VoxVisio.Commands
{
    // Command that opens given program when the user uses the command
    public class OpenProgramCommand : Command
    {
        private string ProgramLocation;
        private string KeyWord;

        public OpenProgramCommand(string programLocation, string KeyWord)
        {
            this.ProgramLocation = programLocation;
            this.KeyWord = KeyWord;
        }

        public OpenProgramCommand(JObject jsonData)
        {
            LoadFromJson(jsonData);
        }

        public void RunCommand()
        {
            if (File.Exists(ProgramLocation))
            {
                Process.Start(ProgramLocation);
            }
            else
            {
                SharedFormsSingleton.Instance().ToastOverlay.NewMessage("The program " + ProgramLocation + " was not found.");
            }
        }

        public void LoadFromJson(JObject jsonData)
        {
            ProgramLocation = (string)jsonData["program location"];
            KeyWord = (string)jsonData["keyword"];
        }

        public JObject SaveToJson()
        {
            JObject toReturn = new JObject();
            toReturn["program location"] = ProgramLocation;
            toReturn["keyword"] = KeyWord;
            toReturn["command type"] = GetCommandType().ToString();
            return toReturn;
        }

        public string GetKeyWord()
        {
            return KeyWord;
        }

        public string GetProgramLocation()
        {
            return ProgramLocation;
        }
        public eCommandType GetCommandType()
        {
            return eCommandType.OpenProgramCommand;
        }
    }
}
