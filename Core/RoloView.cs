using System;
using System.Collections.Generic;
using DivLib.Core;
using FusionLib.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public interface IRoloView
    {
        IRoloMenu Menu { get; }
        
        void Mount(IRoloMenu menu);
        void Close();
    }
    
    public class RoloView : FusionView, IRoloView
    {
        public Color HeaderColor;
        public Color DefaultColor;
        public Div HeaderDiv;
        public Div ElementParent;
        public DivScroll Scroll;
        
        public Div Div { get; private set; }

        public IRoloMenu Menu { get; private set; }
        public List<RolodexElementView> MenuPath { get; } = new List<RolodexElementView>();
        public List<RolodexElementView> Elements { get; } = new List<RolodexElementView>();
        public DivVisibility CloseVisibility { get; private set; }
        public Button CloseButton { get; private set; }
        
        public void Init()
        {
            Div = GetComponent<Div>();
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
        
        public void Mount(IRoloMenu menu)
        {
            ResetView();
            Menu = menu;
            if (Menu == null) return;

            CloseVisibility.SetVisibility(Menu.CanClose);
            AddMenuPath(menu);

            foreach (var element in Menu.Items)
            {
                AddElement(element);
            }
            
            Div.UpdatePosition(true);
        }

        public void Close()
        {
            ResetView();
        }

        public void SetHeaderColor(Color color)
        {
            HeaderDiv.GetComponent<Image>().color = color;
            HeaderColor = color;
        }

        public void SetDefaultColor(Color color)
        {
            DefaultColor = color;
        }

        private void AddMenuPath(IRoloMenu menu)
        {
            Sprite sprite = null;
            if (menu.Parent != null)
            {
                AddMenuPath(menu.Parent);
                sprite = RolodexFactory.NodeSprite;
            }

            var element = new RoloNode(menu.Name, menu, this, HeaderColor, sprite);
            var view = RolodexFactory.GetPathElement();
            HeaderDiv.AddChild(view.Panel);
            view.Mount(element);
            MenuPath.Add(view);
        }

        private void AddElement(IRoloItem item)
        {
            if (item.Color == default(Color)) item.Color = DefaultColor;
            if (item.IsNode && item.Sprite == null) item.Sprite = RolodexFactory.NodeSprite;
            var elementView = RolodexFactory.GetElement();
            ElementParent.AddChild(elementView.Panel); // must come before elementView.Mount(element)
            elementView.Mount(item);
            Elements.Add(elementView);
        }
        
        private void ResetView()
        {
            foreach (var view in MenuPath)
            {
                HeaderDiv.RemoveChild(view.Panel);
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