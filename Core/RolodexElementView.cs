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
        public DivvyPanel Panel { get; private set; }
        public DivvyAnimatedVisibility Visibility { get; private set; }
        
        public void Init()
        {
            Panel = GetComponent<DivvyPanel>();
            Panel.Init();
            Visibility = GetComponent<DivvyAnimatedVisibility>();
            Visibility.OnFinishedAnimation += Recycle;
            Visibility.Init();
        }

        private void Recycle(bool isVisible)
        {
            if (isVisible) return;
            RolodexPrefabs.Return(this);
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
            Visibility.Show();
        }

        public void Dismount()
        {
            Element = null;
            Visibility.Hide();
        }
    }
}