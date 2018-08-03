using Rolodex.Tests;
using UnityEngine;

namespace Rolodex.Core
{
    public class RolodexTestManager : MonoBehaviour
    {
        private void Start()
        {
            var data = new RolodexData(transform);
            data.MountMenu();
            data.View.Panel.Rect.anchoredPosition = new Vector2(10, -10);
        }
    }
}