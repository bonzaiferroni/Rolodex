using System;
using UnityEngine;

namespace Rolodex.Core
{
    public class RolodexElement : IRoloItem
    {
        private Action _action;
        
        public RolodexElement(string name, bool isNode, Action action, Color color = default(Color), Sprite sprite = null)
        {
            Name = name;
            IsNode = isNode;
            _action = action;
            Color = color;
            Sprite = sprite;
        }

        public string Name { get; set; }
        public bool IsNode { get; set; }
        public Color Color { get; set; }
        public Sprite Sprite { get; set; }

        public void Action()
        {
            _action();
        }
    }
}