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
            {typeof(RolodexView), "RolodexView"},
        };

        private static readonly Dictionary<Type, Stack<GameObject>> Pool = new Dictionary<Type, Stack<GameObject>>();
        
        public static T GetView<T>() where T : MonoBehaviour, IRolodexPrefab
        {
            var type = typeof(T);
            var view = GetFromPool<T>(type);
            if (!view)
                view = LoadPrefab<T>(type);
            return view;
        }

        private static T LoadPrefab<T>(Type type) where T : MonoBehaviour, IRolodexPrefab
        {
            if (!Subpaths.ContainsKey(type)) throw new Exception("Type not referenced in prefab loader");
            string subpath = Subpaths[type];
            var path = $"Prefabs/{subpath}";
            var view = Object.Instantiate(Resources.Load<GameObject>(path)).GetComponent<T>();
            view.Init(); // <--- Point of initialization
            return view;
        }

        private static T GetFromPool<T>(Type type) where T : MonoBehaviour, IRolodexPrefab
        {
            var pool = GetPool(type);
            if (pool == null || pool.Count == 0) return null;
            return pool.Pop().GetComponent<T>();
        }

        public static Stack<GameObject> GetPool(Type type)
        {
            if (!Pool.ContainsKey(type)) Pool[type] = new Stack<GameObject>();
            return Pool[type];
        }

        public static void Return(MonoBehaviour item)
        {
            var type = item.GetType();
            if (!Subpaths.ContainsKey(type)) throw new Exception("Type not referenced in prefab loader");
            var pool = GetPool(type);
            pool.Push(item.gameObject);
        }
    }

    public interface IRolodexPrefab
    {
        void Init();
    }
}