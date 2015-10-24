using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Newtonsoft.Json.Linq;

/*
*   Notes:
*   VirtualKeyCodes are used by the input simulator to simulate key presses,
*   and Keys provided by the system hook whenever a key is pressed. VirtualKeyCodes and keys can be cast between eachother nativly.
*/

namespace VoxVisio.Commands
{

    public interface Command
    {
        void RunCommand();
        void LoadFromJson(JObject jsonData);
        JObject SaveToJson();
        string GetKeyWord();
        eCommandType GetCommandType();
    }
}
