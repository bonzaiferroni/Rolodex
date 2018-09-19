using DivLib.Core;
using FusionLib.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexMenuRecipe : DivRecipe
    {
        public const string HeaderTag = "Header";
        public const string CloseButtonTag = "CloseButton";
        
        public void Parts(Fusion obj)
        {
            obj.NewChild(HeaderTag, MenuHeaderParts);
            obj.NewChild(ScrollTag, ScrollParts);

            var div = obj.Add<Div>();
            div.Style = LayoutStyle.Vertical;
            div.ExpandChildren = true;

            var divScroll = obj.Get<DivScroll>(ScrollTag);
            divScroll.MaxSize.Set(0, 150);

            var menu = obj.Add<RolodexMenuView>();
            menu.HeaderColor = DarkBackgroundColor;
            menu.DefaultColor = LightBackgroundColor;
            menu.HeaderDiv = obj.Get<Div>(HeaderTag);
            menu.ElementParent = obj.Get<Div>(ContentTag);
            menu.Scroll = divScroll;
        }

        private void MenuHeaderParts(Fusion obj)
        {
            obj.Add(LineParts);

            obj.NewChild(CloseButtonTag, CloseButtonParts);
            
            obj.Add<Image>(DarkBackground);
        }

        private void CloseButtonParts(Fusion obj)
        {
            var div = obj.Add<Div>();
            div.Margin.Set(5, 0);
            div.MinSize.Set(20, 20);
            
            var image = obj.Add<Image>();
            image.sprite = Fusion.LoadResource<Sprite>("RolodexCloseSprite");

            obj.Add<Button>().targetGraphic = image;

            var visibility = obj.Add<DivEnable>();
            visibility.Add(image);
            visibility.IsVisible = false;
        }
    }
}