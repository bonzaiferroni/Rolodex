using System.Text;
using UnityEngine;

namespace Rolodex.Core
{
    public interface IRoloItem
    {
        string Name { get; }
        bool IsNode { get; }
        Color Color { get; set; }
        Sprite Sprite { get; set; }

        void Action();
    }

    public interface IRoloClient
    {
        void Action(string name);
    }
    
    public abstract class RoloItem : IRoloItem
    {
        public RoloItem(string name, Color color = default(Color), Sprite sprite = null)
        {
            Name = name;
            Color = color;
            Sprite = sprite;
        }
        
        public string Name { get; }
        public Color Color { get; set; }
        public Sprite Sprite { get; set; }

        public bool IsNode => false;

        public abstract void Action();
    }
}