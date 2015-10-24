using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VoxVisio.Commands
{
    public class CommandFactory
    {
        public static Command CreateCommandFromJson(JObject jsonData)
        {
            Command commandObject = null;

            string commandType = (string)jsonData["command type"];

            if (commandType == eCommandType.KeyPressCommand.ToString())
            {
                commandObject = new KeyPressCommand(jsonData);
            }
            else if (commandType == eCommandType.VoiceCommand.ToString())
            {
                commandObject = new VoiceCommand(jsonData);
            }
            else if (commandType == eCommandType.OpenProgramCommand.ToString())
            {
                commandObject = new OpenProgramCommand(jsonData);
            }

            return commandObject;
        }
    }
}
