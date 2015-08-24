using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VoxVisio
{
    public class SettingsSingleton
    {
        private InputSimulator inputSimulator;
        private static SettingsSingleton _singleton;
        private List<Command> commands;

        protected SettingsSingleton()
        {
            inputSimulator = new InputSimulator();
            loadCommands();
        }

        public void loadCommands()
        {
            commands = new List<Command>();
            using (StreamReader reader = File.OpenText(@"Commands.json"))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                JArray a = (JArray)o.SelectToken("command");

                foreach (JObject variable in a)
                {
                    commands.Add(new Command((string)variable["word"], (string)variable["keys"], inputSimulator));
                }
            }
        }

        public List<Command> Commands
        {
            get { return commands; }
        }

        public static SettingsSingleton Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_singleton == null)
            {
                _singleton = new SettingsSingleton();
            }

            return _singleton;
        }
    }
}
