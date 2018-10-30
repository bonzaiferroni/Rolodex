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
        public IRoloItem Item { get; private set; }
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
            Item.Action();
        }

        public void Mount(IRoloItem item)
        {
            Item = item;
            Label.Text = item.Name;
            Icon.sprite = item.Sprite;
            IconVisibility.SetVisibility(item.Sprite != null);
            Background.color = item.Color;
            Visibility.Show();
        }

        public void Dismount()
        {
            Item = null;
            Visibility.Hide();
        }
    }
}