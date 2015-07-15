using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoxVisio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace VoxVisio.Tests
{
    [TestClass()]
    public class CommandSingletonTests
    {
        [TestMethod()]
        public void InstanceTest()
        {
            CommandSingleton cs = CommandSingleton.Instance();
            CommandSingleton cs2 = CommandSingleton.Instance();
            Assert.AreEqual(cs, cs2);
        }

        [TestMethod()]
        public void SetCommandsTest()
        {
            CommandSingleton cs = CommandSingleton.Instance();
            CommandSingleton cs2 = CommandSingleton.Instance();
            List<Command> commands = new List<Command>();
            commands.Add(new Command(new CommandStrings("open", "enter"), new InputSimulator()));
            cs.SetCommands(commands);
            Assert.AreEqual(cs.Commands,cs2.Commands);
            commands.Add(new Command(new CommandStrings("click", "m1"), new InputSimulator()));
            Assert.AreEqual(cs.Commands.Count, 2);
            Assert.AreEqual(cs2.Commands.Count, 2);

        }
    }
}