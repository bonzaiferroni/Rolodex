﻿using System;
using System.Collections.Generic;
using DivLib.Core;
using FusionLib.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexMenuView : MonoBehaviour
    {
        public Sprite DividerSprite;
        public Color PathColor;
        public Color DefaultColor;
        public Div PathParent;
        public Div ElementParent;
        
        public Div Div { get; private set; }
        public RolodexMenu Menu { get; private set; }
        public List<RolodexElementView> MenuPath { get; } = new List<RolodexElementView>();
        public List<RolodexElementView> Elements { get; } = new List<RolodexElementView>();

        public void Init()
        {
            Div = GetComponent<Div>();
            Div.Init();
        }
        
        public void Mount(RolodexMenu menu)
        {
            ResetView();
            Menu = menu;
            if (Menu == null) return;

            AddMenuPath(menu, true);

            foreach (var element in Menu.Elements)
            {
                AddElement(element);
            }
            Div.UpdatePosition(true);
        }

        private void AddMenuPath(RolodexMenu menu, bool isDisplayedMenu)
        {
            Sprite sprite = null;
            if (menu.Parent != null)
            {
                AddMenuPath(menu.Parent, false);
                sprite = DividerSprite;
            }
            
            Action action = null;
            if (!isDisplayedMenu)
            {
                action = () => Mount(menu);
            }

            var element = new RolodexElement(menu.Name, action, PathColor, sprite);
            var view = RolodexFactory.GetPathElement();
            PathParent.AddChild(view.Panel);
            view.Mount(element);
            MenuPath.Add(view);
        }

        private void AddElement(RolodexElement element)
        {
            if (element.Color == default(Color)) element.Color = DefaultColor;
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

            Menu = null;
        }
    }
}