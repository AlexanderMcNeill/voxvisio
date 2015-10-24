using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FMUtils.KeyboardHook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VoxVisio.Properties;
using VoxVisio.Resources;
using VoxVisio.Commands;

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
        public bool DragonEnabled { get; set; }
        public bool ZoomEnabled { get; set; }
        public double ZoomMagnification { get; set; }
        public Size ZoomFormSize { get; set; }
        public bool DebugEyeMouseMode { get; set; }


        protected SettingsSingleton()
        {
            loadCommands();
            keyboardHook = new Hook("Global Action Hook");
            loadSettings();
            saveSettings();
        }
            
        private void loadSettings()
        {
            string fileContents = Properties.Resources.Settings;
            using (StringReader reader = new StringReader(fileContents))//@"Commands.json"
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                ZoomEnabled = (bool)o["zoom form"]["enabled"];
                ZoomMagnification = (double) o["zoom form"]["magnification"];
                ZoomFormSize = new Size((int)o["zoom form"]["width"], (int)o["zoom form"]["height"]);
                DebugEyeMouseMode = (bool)o["eye tracking"]["debug mouse mode"];
                DragonEnabled = (bool) o["dragon enabled"];
            }
            ZoomEnabled = Settings.Default.ZoomEnabled;
            ZoomMagnification = Settings.Default.ZoomMagnification;
            ZoomFormSize = Settings.Default.ZoomFormSize;
            DebugEyeMouseMode = Settings.Default.DebugEyeMouseMode;
            DragonEnabled = Settings.Default.DragonEnabled;
        }

        public void saveSettings()
        {
            Settings.Default.ZoomEnabled = ZoomEnabled;
            Settings.Default.ZoomMagnification = ZoomMagnification;
            Settings.Default.ZoomFormSize = ZoomFormSize;
            Settings.Default.DebugEyeMouseMode = DebugEyeMouseMode;
            Settings.Default.DragonEnabled = DragonEnabled;
            Settings.Default.Save();
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

        // A list of all currently loaded commands
        public EventList<Command> Commands
        {
            get
            {
                return commands;
            }
        }


        #region command loading and saving
        public void SetCommands(List<Command> commands)
        {
            this.commands = (EventList<Command>)commands;
        }
        public void saveCommands()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"commands.txt"))
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
            // if the commands text file has been created, load it; otherwise load the defualt commands.
            string fileContents = File.Exists("commands.txt") ? File.ReadAllText("commands.txt") : Properties.Resources.Commands;

            var tempList = new EventList<Command>();
            
            using (StringReader reader = new StringReader(fileContents))//@"Commands.json"
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                JArray a = (JArray) o["commands"];
                tempList.AddRange(from JObject variable in a select CommandFactory.CreateCommandFromJson(variable));
                
            }
            commands = tempList;
            saveCommands();
        }


#endregion
    }
}
