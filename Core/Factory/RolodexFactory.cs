using System;
using System.Collections.Generic;
using FusionLib.Core;
using UnityEngine;

namespace Rolodex.Core
{
    public class RolodexFactory : RecipeFactory
    {
        private static readonly RolodexFactory Instance = new RolodexFactory();
        private static Sprite _nodeIcon;

        protected override Dictionary<Type, Action<Fusion>> Recipes { get; } = new Dictionary<Type, Action<Fusion>>()
        {
            {typeof(RolodexPathElementView), new RolodexElementRecipe().PathElementParts},
            {typeof(RolodexElementView), new RolodexElementRecipe().ElementParts},
        };

        public static Sprite NodeSprite
        {
            get
            {
                if (!_nodeIcon) _nodeIcon = Fusion.LoadResource<Sprite>("RolodexNodeSprite");
                return _nodeIcon;
            }
        }

        public static RolodexMenuView GetMenu()
        {
            var view = Fusion.Create("RolodexMenu", new RolodexMenuRecipe().Parts).Get<RolodexMenuView>();
            view.Init();
            return view;
        }

        public static RolodexElementView GetElement()
        {
            var view = Instance.GetView<RolodexElementView>();
            view.Init();
            return view;
        }

        public static RolodexPathElementView GetPathElement()
        {
            var view = Instance.GetView<RolodexPathElementView>();
            view.Init();
            return view;
        }
    }
}