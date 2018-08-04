using System.Linq;
using Divvy.Core;
using NUnit.Framework;
using Rolodex.Core;
using UnityEngine;

namespace Rolodex.Tests
{
    public class RolodexViewTests : MonoBehaviour
    {
        [Test]
        public void Mount_MountsMenu()
        {
            var data = new RolodexData();
            data.MountMenu();
            Assert.AreEqual(data.Menu, data.View.Menu);
        }

        [Test]
        public void Mount_MountsElements()
        {
            var data = new RolodexData();
            data.MountMenu();
            Assert.AreEqual(data.Menu.Elements[0], data.View.Elements[0].Element);
        }
    }
}