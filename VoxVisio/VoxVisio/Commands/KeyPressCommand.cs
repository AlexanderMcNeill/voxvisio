using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using VoxVisio.Singletons;

namespace VoxVisio.Commands
{
    public class KeyPressCommand : Command
    {
        // Key that triggers the command
        public Keys triggerKey;
        // Delegate to the method that needs to be called on trigger
        public string commandWord;

        public KeyPressCommand(string commandWord, Keys triggerKey)
        {
            this.triggerKey = triggerKey;
            this.commandWord = commandWord;
        }

        public KeyPressCommand(JObject jsonData)
        {
            LoadFromJson(jsonData);
        }

        //Looks through all of the standard commands 
        public void RunCommand()
        {
            SettingsSingleton.Instance().Commands.Find(x => x.GetKeyWord() == commandWord).RunCommand();
        }

        public void LoadFromJson(JObject jsonData)
        {
            string tempTriggerKey = (string)jsonData["trigger key"];         // Load String from json
            VirtualKeyCode vkCode = KeyTranslater.GetKeyCode(tempTriggerKey); // Convert the string to a keycode using the Keytranslator
            triggerKey = (Keys)vkCode;                                        // Cast the Vkeycode to a key and save to the class
            commandWord = (string)jsonData["command word"];
        }

        public JObject SaveToJson()
        {
            JObject toReturn = new JObject();
            toReturn["trigger key"] = KeyTranslater.GetKeyString((VirtualKeyCode)triggerKey);
            toReturn["command word"] = commandWord;
            toReturn["command type"] = GetCommandType().ToString();
            return toReturn;
        }


        public string GetKeyWord()
        {
            return commandWord;
        }

        public eCommandType GetCommandType()
        {
            return eCommandType.KeyPressCommand;
        }
    }
}
