using UnityEngine;


namespace OkamelUtils.CustomWrappers {
    public abstract class Singleton<T> : MonoBehaviour where T : Component {
        public static T Instance => _instance ??= FindFirstObjectByType<T>();
        private static T _instance;


        public virtual void Dispose() => _instance = null;
        private void OnDestroy() => Dispose();
    }
}