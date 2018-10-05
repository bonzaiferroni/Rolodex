using DivLib.Core;
using FusionLib.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rolodex.Core
{
    public class RolodexElementView : MonoBehaviour
    {
        public DivText Label;
        public Image Icon;
        public DivVisibility IconVisibility;
        
        public Button Button { get; private set; }
        public Image Background { get; private set; }
        public RolodexElement Element { get; private set; }
        public Div Panel { get; private set; }
        public DivAnimatedVisibility Visibility { get; private set; }
        
        public void Init()
        {
            Panel = GetComponent<Div>();
            Background = GetComponent<Image>();
            Button = GetComponent<Button>();
            Visibility = GetComponent<DivAnimatedVisibility>();
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
            IconVisibility.SetVisibility(element.Sprite != null);
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