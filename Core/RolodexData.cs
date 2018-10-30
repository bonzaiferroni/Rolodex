using DivLib.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexData
    {
        public RolodexData(Transform transform = null, int extraCount = 0)
        {
            View = RolodexFactory.GetMenu();
            if (!transform)
            {
                Canvas = new GameObject("Canvas", typeof(Canvas));
                Canvas.AddComponent<CanvasScaler>();
                Canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                transform = Canvas.transform;
            }
            View.transform.SetParent(transform);
            View.gameObject.AddComponent<DivRoot>().Initialize();
            View.Div.SetPivot(new Vector2(0, 1));
            View.Div.LineHeight = 30;
            
            Menu = new RoloMenu("TestMenu", null);
            SubMenu = new RoloMenu("SubMenu", Menu);
            SubSubMenu = new RoloMenu("SubSubMenu", SubMenu);
            Menu.Items.Add(new RolodexElement("SubMenu", true, MountSubMenu));
            Menu.Items.Add(new RolodexElement("Element1", false, () => Element1Pushed = true));
            Menu.Items.Add(new RolodexElement("Element2", false, () => Debug.Log("Element2 pushed")));
            SubMenu.Items.Add(new RolodexElement("SubSubMenu", true, () => View.Mount(SubSubMenu)));
            SubMenu.Items.Add(new RolodexElement("SubElement1", false, () => Debug.Log("SubElement1 pushed")));

            for (int i = 0; i < extraCount; i++)
            {
                Menu.Items.Add(new RolodexElement($"Extra{i}", false, () => Debug.Log($"Pushed extra {i}")));
            }
        }
        
        public GameObject Canvas { get; }
        public RoloMenu Menu { get; }
        public RoloMenu SubMenu { get; }
        public RoloMenu SubSubMenu { get; }
        public RoloView View { get; }
        public bool Element1Pushed { get; private set; }

        public void MountMenu()
        {
            View.Mount(Menu);
        }

        public void MountSubMenu()
        {
            View.Mount(SubMenu);
        }
        
    }
}