using UnityEngine;

namespace Rolodex.Core
{
    public class RoloClientItem : IRoloItem
    {
        private readonly IRoloClient _client;

        public RoloClientItem(string name, IRoloClient client, Color color = default(Color), Sprite sprite = null)
        {
            Name = name;
            _client = client;
            Color = color;
            Sprite = sprite;
        }
        
        public string Name { get; }
        public Color Color { get; set; }
        public Sprite Sprite { get; set; }

        public bool IsNode => false;

        public void Action()
        {
            _client.Action(Name);
        }
    }
}