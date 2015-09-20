using System;
using System.Collections.Generic;
using System.IO;
using FMUtils.KeyboardHook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VoxVisio.Singletons
{
    public class SettingsSingleton
    {
        private static SettingsSingleton _singleton;
        private List<VoiceCommand> commands;
        public readonly List<KeyPressCommand> specialCommands;
        public readonly Hook keyboardHook;

        protected SettingsSingleton()
        {
            loadCommands();
            specialCommands = new List<KeyPressCommand>();
            keyboardHook = new Hook("Global Action Hook");
            //saveCommands();
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
            specialCommands.Add(toAddCommand);
        }

        // A list of all currently loaded commands
        public List<VoiceCommand> Commands
        {
            get { return commands; }
        }
        

        public void SetCommands(List<VoiceCommand> commands)
        {
            this.commands = commands;
        }
        private void saveCommands()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"c:\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("voice command");
                writer.WriteStartArray();
                foreach (var item in commands)
                {
                    var dict = item.getDict();
                    writer.WriteStartObject();
                    foreach (var dictItem in dict)
                    {
                        writer.WritePropertyName(dictItem.Key);
                        writer.WriteValue(dictItem.Value);
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndObject();

                writer.Close();
            }
        }


        private void loadCommands()
        {
            var tempList = new List<VoiceCommand>();
            string fileContents = Properties.Resources.Commands;
            using (StringReader reader = new StringReader(fileContents))//@"Commands.json"
            {
                
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));

                JArray a = (JArray) o["voice command"];

                foreach (JObject variable in a)
                {
                    tempList.Add(new VoiceCommand(variable));
                }
            }
            commands = tempList;
        }

    }
}
