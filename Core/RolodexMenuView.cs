using System;
using System.Collections.Generic;
using DivLib.Core;
using FusionLib.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexMenuView : FusionView
    {
        public Color PathColor;
        public Color DefaultColor;
        public Div PathParent;
        public Div ElementParent;
        public DivScroll Scroll;
        
        public Div Div { get; private set; }

        public RolodexMenu Menu { get; private set; }
        public List<RolodexElementView> MenuPath { get; } = new List<RolodexElementView>();
        public List<RolodexElementView> Elements { get; } = new List<RolodexElementView>();
        public DivVisibility CloseVisibility { get; private set; }
        public Button CloseButton { get; private set; }
        
        public void Init()
        {
            Div = GetComponent<Div>();
            Div.Init();
            CloseVisibility = GetChild<DivVisibility>(RolodexMenuRecipe.CloseButtonTag);
            CloseButton = GetChild<Button>(RolodexMenuRecipe.CloseButtonTag);
        }

        private void Start()
        {
            if (CloseButton) CloseButton.onClick.AddListener(OnCloseClick);
        }

        private void OnCloseClick()
        {
            Close();
        }

        public void Mount(RolodexMenu menu)
        {
            ResetView();
            Menu = menu;
            if (Menu == null) return;

            CloseVisibility.SetVisibility(Menu.CanClose);
            AddMenuPath(menu, true);

            foreach (var element in Menu.Elements)
            {
                AddElement(element);
            }
            Div.UpdatePosition(true);
        }

        public void Close()
        {
            ResetView();
        }

        private void AddMenuPath(RolodexMenu menu, bool isDisplayedMenu)
        {
            Sprite sprite = null;
            if (menu.Parent != null)
            {
                AddMenuPath(menu.Parent, false);
                sprite = RolodexFactory.NodeSprite;
            }
            
            Action action = null;
            if (!isDisplayedMenu)
            {
                action = () => Mount(menu);
            }

            var element = new RolodexElement(menu.Name, true, action, PathColor, sprite);
            var view = RolodexFactory.GetPathElement();
            PathParent.AddChild(view.Panel);
            view.Mount(element);
            MenuPath.Add(view);
        }

        private void AddElement(RolodexElement element)
        {
            if (element.Color == default(Color)) element.Color = DefaultColor;
            if (element.IsNode && element.Sprite == null) element.Sprite = RolodexFactory.NodeSprite;
            var elementView = RolodexFactory.GetElement();
            ElementParent.AddChild(elementView.Panel); // must come before elementView.Mount(element)
            elementView.Mount(element);
            Elements.Add(elementView);
        }
        
        private void ResetView()
        {
            foreach (var view in MenuPath)
            {
                PathParent.RemoveChild(view.Panel);
                view.Dismount();
            }

            MenuPath.Clear();
            
            foreach (var view in Elements)
            {
                ElementParent.RemoveChild(view.Panel);
                view.Dismount();
            }

            Elements.Clear();
            CloseVisibility.Hide();

            Menu = null;
        }
    }
}