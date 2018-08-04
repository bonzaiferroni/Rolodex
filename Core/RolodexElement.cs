using System;
using System.Text;
using UnityEngine;

namespace Rolodex.Core
{
    public class RolodexElement
    {
        public RolodexElement(string name, Action action, Color color = default(Color), Sprite sprite = null)
        {
            Name = name;
            Action = action;
            Color = color;
            Sprite = sprite;
        }

        public string Name { get; }
        public Action Action { get; }
        public Color Color { get; set; }
        public Sprite Sprite { get; set; }
    }
}