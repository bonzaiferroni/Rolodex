using Divvy.Core;
using UnityEngine;

namespace Rolodex.Core
{
    public class RolodexView : MonoBehaviour
    {
        [SerializeField] private DivvyText _label;
        
        public DivvyText Label => _label;
        
        public RolodexMenu Parent { get; private set; }
        public DivvyParent Panel { get; private set; }
        
        public void Mount(RolodexMenu menu)
        {
            Label.Text = menu.ToString();
            
            if (menu.Parent != null)
            {
                Parent = menu.Parent;
                var element = new RolodexElement("back", Back);
                var elementView = RolodexPrefabs.GetView<RolodexElementView>();
                elementView.Mount(element);
            }
            
            
        }

        private void Back()
        {
            Mount(Parent);
        }
    }
}