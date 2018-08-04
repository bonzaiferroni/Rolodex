using System;
using System.Collections.Generic;
using Divvy.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexView : MonoBehaviour, IRolodexPrefab
    {
        [SerializeField] private DivvyParent _pathParent;
        [SerializeField] private Sprite _dividerSprite;
        [SerializeField] private Color _pathColor;
        [SerializeField] private Color _defaultColor;

        public DivvyParent PathParent => _pathParent;
        
        public DivvyParent Panel { get; private set; }
        public RolodexMenu Menu { get; private set; }
        public List<RolodexElementView> MenuPath { get; } = new List<RolodexElementView>();
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

            AddMenuPath(menu, true);

            foreach (var element in Menu.Elements)
            {
                AddElement(element);
            }
        }

        private void AddMenuPath(RolodexMenu menu, bool isDisplayedMenu)
        {
            Sprite sprite = null;
            if (menu.Parent != null)
            {
                AddMenuPath(menu.Parent, false);
                sprite = _dividerSprite;
            }
            
            Action action = null;
            if (!isDisplayedMenu)
            {
                action = () => Mount(menu);
            }

            var element = new RolodexElement(menu.Name, action, _pathColor, sprite);
            var view = RolodexPrefabs.GetView<RolodexPathElementView>();
            PathParent.AddChild(view.Panel);
            view.Mount(element);
            MenuPath.Add(view);
        }

        private void AddElement(RolodexElement element)
        {
            if (element.Color == default(Color)) element.Color = _defaultColor;
            var elementView = RolodexPrefabs.GetView<RolodexElementView>();
            Panel.AddChild(elementView.Panel); // must come before elementView.Mount(element)
            elementView.Mount(element);
            Elements.Add(elementView);
        }
        
        private void Reset()
        {
            foreach (var view in MenuPath)
            {
                PathParent.RemoveChild(view.Panel);
                view.Dismount();
            }

            MenuPath.Clear();
            
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