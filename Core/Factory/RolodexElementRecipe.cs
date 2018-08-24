using DivLib.Core;
using FusionLib.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexElementRecipe : DivRecipe
    {
        public const string IconTag = "Icon";
        
        public void ElementParts(Fusion obj)
        {
            obj.Add(BaseParts);

            var view = obj.Add<RolodexElementView>();
            view.Label = obj.Get<DivText>(LabelTag);
            view.Icon = obj.Get<Image>(IconTag);
            view.IconVisibility = obj.Get<DivVisibility>(IconTag);
        }

        public void PathElementParts(Fusion obj)
        {
            obj.Add(BaseParts);

            var div = obj.Get<Div>();
            div.Padding.Set(0);
            
            obj.Get<Image>(DarkBackground);
            
            var view = obj.Add<RolodexPathElementView>();
            view.Label = obj.Get<DivText>(LabelTag);
            view.Icon = obj.Get<Image>(IconTag);
            view.IconVisibility = obj.Get<DivVisibility>(IconTag);
        }

        private void BaseParts(Fusion obj)
        {
            obj.Add(LineParts);

            obj.NewChild(IconTag, IconParts);
            obj.NewChild(LabelTag, LabelParts);

            var image = obj.Add<Image>(LightBackground);
            var button = obj.Add<Button>();
            button.targetGraphic = image;
            var colorBlock = button.colors;
            colorBlock.disabledColor = Color.white;
            button.colors = colorBlock;
            obj.Add<DivFade>();
            obj.Add<PoolMember>();
        }

        private void IconParts(Fusion obj)
        {
            var div = obj.Add<Div>();
            div.Margin.Set(5, 0);
            div.MinSize.Set(20, 20);
            
            var image = obj.Add<Image>();
            obj.Add<DivEnable>().Graphic = image;
            image.sprite = Fusion.LoadResource<Sprite>("RolodexCircleSprite");
        }
    }
}