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
<<<<<<< HEAD
        public bool ZoomEnabled { get; private set; }
        public double ZoomMagnification { get; private set; }
        public Size ZoomFormSize { get; private set; }
        public bool DebugEyeMouseMode { get; private set; }
        public bool DragonEnabled { get; private set; }
=======
        public bool ZoomEnabled { get; set; }
        public double ZoomMagnification { get; set; }
        public Size ZoomFormSize { get; set; }
        public bool DebugEyeMouseMode { get; set; }
>>>>>>> origin/master


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
        }

        public void saveSettings()
        {
<<<<<<< HEAD
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"c:\jsonSettings.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                JObject obj1 = new JObject();
                JObject obj2 = new JObject();
                JObject obj3 = new JObject();

                obj2["enabled"] = ZoomEnabled;
                obj2["magnification"] = ZoomMagnification;
                obj2["width"] = ZoomFormSize.Width;
                obj2["height"] = ZoomFormSize.Height;
                obj3["debug mouse mode"] = DebugEyeMouseMode;

                obj1["zoom form"] = obj2;
                obj1["eye tracking"] = obj3;
                obj1["dragon enabled"] = DragonEnabled;
                obj1.WriteTo(writer);

                writer.Close();
            }
=======
            Settings.Default.ZoomEnabled = ZoomEnabled;
            Settings.Default.ZoomMagnification = ZoomMagnification;
            Settings.Default.ZoomFormSize = ZoomFormSize;
            Settings.Default.DebugEyeMouseMode = DebugEyeMouseMode;
            Settings.Default.Save();
>>>>>>> origin/master
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
