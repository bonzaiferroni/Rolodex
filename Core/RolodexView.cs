using System.Collections.Generic;
using Divvy.Core;
using UnityEngine;

namespace Rolodex.Core
{
    public class RolodexView : MonoBehaviour, IRolodexPrefab
    {
        [SerializeField] private DivvyText _label;
        
        public DivvyText Label => _label;
        
        public DivvyParent Panel { get; private set; }
        public RolodexMenu Menu { get; private set; }
        public List<RolodexElementView> Elements { get; } = new List<RolodexElementView>();

        public void Init()
        {
            Panel = GetComponent<DivvyParent>();
            Panel.Init();
        }
        
        public void Mount(RolodexMenu menu)
        {
            Reset();
            Menu = menu;
            
            Label.Text = Menu.ToString();
            
            if (Menu.Parent != null)
            {
                var element = new RolodexElement("back", Back);
                AddElement(element);
            }

            foreach (var element in Menu.Elements)
            {
                AddElement(element);
            }
        }

        private void AddElement(RolodexElement element)
        {
            var elementView = RolodexPrefabs.GetView<RolodexElementView>();
            Panel.AddChild(elementView.Panel); // must come before elementView.Mount(element)
            elementView.Mount(element);
            Elements.Add(elementView);
        }

        private void Back()
        {
            Mount(Menu.Parent);
        }
        
        private void Reset()
        {
            foreach (var view in Elements)
            {
                Panel.RemoveChild(view.Panel);
                view.Dismount();
            }

            Elements.Clear();

            Menu = null;
        }
    }
}