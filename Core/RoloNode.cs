using UnityEngine;

namespace Rolodex.Core
{
    public class RoloNode : IRoloItem
    {
        private IRoloMenu _menu;
        private IRoloView _view;

        public RoloNode(string name, IRoloMenu menu, IRoloView view, Color color = default(Color), Sprite sprite = null)
        {
            _menu = menu;
            _view = view;
            Name = name;
            Color = color;
            Sprite = sprite;
        }
        
        public string Name { get; }
        
        public bool IsNode => true;
        
        public Color Color { get; set; }
        
        public Sprite Sprite { get; set; }
        
        public void Action()
        {
            _view.Mount(_menu);
        }
    }
}