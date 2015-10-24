using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsInput.Native;
using VoxVisio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoxVisio.Resources;

namespace VoxVisio.Tests
{
    [TestClass()]
    public class KeyTranslaterTests
    {
        [TestMethod()]
        public void GetKeyCodeTest()
        {
            Assert.AreEqual(KeyTranslater.GetKeyCode("m1"), VirtualKeyCode.LBUTTON);
            Assert.AreEqual(KeyTranslater.GetKeyCode("m2"), VirtualKeyCode.RBUTTON);
        }
    }
}