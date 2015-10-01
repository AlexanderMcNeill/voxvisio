using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FMUtils.KeyboardHook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VoxVisio.Properties;
using VoxVisio.Resources;

namespace VoxVisio.Singletons
{
    public class SettingsSingleton
    {
        private static SettingsSingleton _singleton;
        public delegate void SettingsChangeEventHandler(object sender, SettingsSingleton e);
        public event SettingsChangeEventHandler OnChange;

        private EventList<Command> commands;
        public readonly Hook keyboardHook;
        public event EventHandler CommandsChanged;
        

        protected SettingsSingleton()
        {
            loadCommands();
            //specialCommands = new List<KeyPressCommand>();
            keyboardHook = new Hook("Global Action Hook");
            saveCommands();
            
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

        public void addSpecialCommand(KeyPressCommand toAddCommand)
        {
            commands.Add(toAddCommand);
        }

        // A list of all currently loaded commands
        public EventList<Command> Commands
        {
            get
            {
                return commands;
            }
        }

        

        public void SetCommands(List<Command> commands)
        {
            this.commands = (EventList<Command>)commands;
        }
        public void saveCommands()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"c:\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("commands");
                writer.WriteStartArray();
                foreach (var item in commands)
                {
                    item.SaveToJson().WriteTo(writer);
                }
                writer.WriteEndArray();
                writer.WriteEndObject();

                writer.Close();
            }
        }

        private void loadCommands()
        {
            var tempList = new EventList<Command>();
            string fileContents = Properties.Resources.Commands;
            using (StringReader reader = new StringReader(fileContents))//@"Commands.json"
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                JArray a = (JArray) o["commands"];
                tempList.AddRange(from JObject variable in a select CommandFactory.CreateCommandFromJson(variable));
                
            }
            commands = tempList;
        }

    }
}
