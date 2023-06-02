using Sirenix.OdinInspector;
using UnityEngine;


namespace OknaaEXTENSIONS.CustomWrappers {
    public abstract class Singleton<T> : SerializedMonoBehaviour where T : Component {
        public static T Instance => _instance ??= FindObjectOfType<T>();
        private static T _instance;


        public virtual void Dispose() => _instance = null;
        private void OnDestroy() => Dispose();
    }
}