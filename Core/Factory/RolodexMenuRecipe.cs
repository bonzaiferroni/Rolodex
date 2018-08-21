using DivLib.Core;
using FusionLib.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexMenuRecipe : DivRecipe
    {
        public const string HeaderTag = "Header";
        
        public void Parts(Fusion obj)
        {
            obj.NewChild(HeaderTag, MenuHeaderParts);
            obj.NewChild(ScrollTag, ScrollParts);

            var div = obj.Add<Div>();
            div.Style = LayoutStyle.Vertical;
            div.ExpandChildren = true;
            div.LineHeight = 30;

            var divScroll = obj.Get<DivScroll>(ScrollTag);
            divScroll.MaxSize.Set(0, 150);

            var menu = obj.Add<RolodexMenuView>();
            menu.PathColor = DarkBackgroundColor;
            menu.DefaultColor = LightBackgroundColor;
            menu.DividerSprite = Fusion.LoadResource<Sprite>("RolodexDividerSprite");
            menu.PathParent = obj.Get<Div>(HeaderTag);
            menu.ElementParent = obj.Get<Div>(ContentTag);
        }

        private void MenuHeaderParts(Fusion obj)
        {
            obj.Add(LineParts);
            
            obj.Add<Image>(DarkBackground);
        }
    }
}