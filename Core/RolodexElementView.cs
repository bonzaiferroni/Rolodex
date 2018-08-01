using Divvy.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexElementView : MonoBehaviour, IRolodexPrefab
    {
        [SerializeField] private DivvyText _label;
        [SerializeField] private Button _button;
        
        public RolodexElement Element { get; private set; }
        
        public void Init()
        {
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Element.Action();
        }

        public void Mount(RolodexElement element)
        {
            Element = element;
            _label.Text = element.Name;
        }
    }
}