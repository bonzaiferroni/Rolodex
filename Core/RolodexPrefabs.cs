using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rolodex.Core
{
    public static class RolodexPrefabs
    {
        private static readonly Dictionary<Type, string> Subpaths = new Dictionary<Type, string>()
        {
            // class structures
            {typeof(RolodexElementView), "RolodexElementView"},
        };
        
        public static T GetView<T>() where T : MonoBehaviour, IRolodexPrefab
        {
            string subpath = Subpaths[typeof(T)];
            var path = $"Prefabs/{subpath}";
            var view = Object.Instantiate(Resources.Load<GameObject>(path)).GetComponent<T>();
            view.Init();
            return view;
        }
    }

    public interface IRolodexPrefab
    {
        void Init();
    }
}