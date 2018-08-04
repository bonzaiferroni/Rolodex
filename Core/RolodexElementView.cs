using Divvy.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexElementView : MonoBehaviour, IRolodexPrefab
    {
        [SerializeField] private DivvyText _label;
        [SerializeField] private Image _icon;
        [SerializeField] private DivvyPanel _spritePanel;

        public DivvyText Label => _label;
        public Image Icon => _icon;
        public DivvyPanel SpritePanel => _spritePanel;
        public Button Button { get; private set; }
        public Image Background { get; private set; }
        public RolodexElement Element { get; private set; }
        public DivvyParent Panel { get; private set; }
        public DivvyAnimatedVisibility Visibility { get; private set; }
        
        public virtual void Init()
        {
            Panel = GetComponent<DivvyParent>();
            Panel.Init();
            Background = GetComponent<Image>();
            Button = GetComponent<Button>();
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
            if (Button) Button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Element.Action();
        }

        public void Mount(RolodexElement element)
        {
            Element = element;
            Label.Text = element.Name;
            Icon.sprite = element.Sprite;
            SpritePanel.Visibility.SetVisibility(element.Sprite != null);
            Background.color = element.Color;
            Button.interactable = element.Action != null;
            Visibility.Show();
        }

        public void Dismount()
        {
            Element = null;
            Visibility.Hide();
        }
    }
}