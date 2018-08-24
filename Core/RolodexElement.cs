using System;
using System.Text;
using UnityEngine;

namespace Rolodex.Core
{
    public class RolodexElement
    {
        public RolodexElement(string name, bool isNode, Action action, Color color = default(Color), Sprite sprite = null)
        {
            Name = name;
            IsNode = isNode;
            Action = action;
            Color = color;
            Sprite = sprite;
        }

        public string Name { get; set; }
        public bool IsNode { get; set; }
        public Action Action { get; }
        public Color Color { get; set; }
        public Sprite Sprite { get; set; }
    }
}