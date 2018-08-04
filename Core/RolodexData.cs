using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexData
    {
        public RolodexData(Transform transform = null)
        {
            View = RolodexPrefabs.GetView<RolodexView>();
            if (!transform)
            {
                Canvas = new GameObject("Canvas", typeof(Canvas));
                Canvas.AddComponent<CanvasScaler>();
                Canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                transform = Canvas.transform;
            }
            View.transform.SetParent(transform);
            
            Menu = new RolodexMenu("TestMenu", null);
            SubMenu = new RolodexMenu("SubMenu", Menu);
            SubSubMenu = new RolodexMenu("SubSubMenu", SubMenu);
            Menu.Elements.Add(new RolodexElement("SubMenu", MountSubMenu));
            Menu.Elements.Add(new RolodexElement("Element1", () => Element1Pushed = true));
            Menu.Elements.Add(new RolodexElement("Element2", () => Debug.Log("Element2 pushed")));
            SubMenu.Elements.Add(new RolodexElement("SubSubMenu", () => View.Mount(SubSubMenu)));
            SubMenu.Elements.Add(new RolodexElement("SubElement1", () => Debug.Log("SubElement1 pushed")));
        }
        
        public GameObject Canvas { get; }
        public RolodexMenu Menu { get; }
        public RolodexMenu SubMenu { get; }
        public RolodexMenu SubSubMenu { get; }
        public RolodexView View { get; }
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