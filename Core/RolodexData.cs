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
            
            Menu = new RolodexMenu("TestMenu", null);
            SubMenu = new RolodexMenu("SubMenu", Menu);
            SubSubMenu = new RolodexMenu("SubSubMenu", SubMenu);
            Menu.Elements.Add(new RolodexElement("SubMenu", true, MountSubMenu));
            Menu.Elements.Add(new RolodexElement("Element1", false, () => Element1Pushed = true));
            Menu.Elements.Add(new RolodexElement("Element2", false, () => Debug.Log("Element2 pushed")));
            SubMenu.Elements.Add(new RolodexElement("SubSubMenu", true, () => View.Mount(SubSubMenu)));
            SubMenu.Elements.Add(new RolodexElement("SubElement1", false, () => Debug.Log("SubElement1 pushed")));

            for (int i = 0; i < extraCount; i++)
            {
                Menu.Elements.Add(new RolodexElement($"Extra{i}", false, () => Debug.Log($"Pushed extra {i}")));
            }
        }
        
        public GameObject Canvas { get; }
        public RolodexMenu Menu { get; }
        public RolodexMenu SubMenu { get; }
        public RolodexMenu SubSubMenu { get; }
        public RolodexMenuView View { get; }
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