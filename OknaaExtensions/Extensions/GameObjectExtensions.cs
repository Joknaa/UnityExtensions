using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace OknaaEXTENSIONS {
    public static class GameObjectExtensions {
        public static List<Scene> GetAllLoadedScenes(this SceneManager sceneManager) {
            int countLoaded = SceneManager.sceneCount;
            List<Scene> loadedScenes = new List<Scene>();

            for (int i = 0; i < countLoaded; i++) {
                loadedScenes.Add(SceneManager.GetSceneAt(i));
            }

            return loadedScenes;
        }

        /// <summary>
        /// get all gameobject children recursively
        /// </summary>
        public static List<GameObject> GetALLChildren(this GameObject go) {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in go.transform) {
                children.Add(child.gameObject);
                children.AddRange(child.gameObject.GetALLChildren());
            }

            return children;
        }

        /// <summary>
        /// Shuffles the children of a gameobject
        /// </summary>
        /// <param name="parent"></param>
        public static void ShuffleChildren(this GameObject parent) {
            List<Transform> children = parent.GetDirectChildren<Transform>();
            children.Shuffle();
            var childCount = children.Count;
            for (int i = 0; i < childCount; i++) {
                children[i].transform.SetSiblingIndex(i);
            }
        }

        /// <summary>
        /// Shuffles the children of a transform
        /// </summary>
        /// <param name="parent"></param>
        public static void ShuffleChildren(this Transform parent) {
            List<Transform> children = parent.GetDirectChildren<Transform>();
            children.Shuffle();
            var childCount = children.Count;
            for (int i = 0; i < childCount; i++) {
                children[i].SetSiblingIndex(i);
            }
        }


        /// <summary>
        /// Gets all the direct children of a gameObject.
        /// </summary>
        public static List<GameObject> GetDirectChildren(this GameObject parent) {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in parent.transform) {
                children.Add(child.gameObject);
            }

            return children;
        }

        /// <summary>
        /// Gets all the direct children of a gameObject, as a list of the component type T.
        /// </summary>
        public static List<T> GetDirectChildren<T>(this GameObject parent) {
            List<T> children = new List<T>();
            foreach (Transform child in parent.transform) {
                children.Add(child.GetComponent<T>());
            }

            return children;
        }

        // GetChildByTag recursively searches for a child with the given tag
        public static Transform GetChildByTag(this Transform parent, string tag) {
            foreach (Transform child in parent.transform) {
                if (child.CompareTag(tag)) return child;
                
                Transform childOfChild = child.GetChildByTag(tag);
                if (childOfChild != null) return childOfChild;
            }

            return null;
        }


        /// <summary>
        /// Gets all the direct children of a transform, as a list of the component type T.
        /// </summary>
        public static List<T> GetDirectChildren<T>(this Transform parent) {
            return GetDirectChildren<T>(parent.gameObject);
        }

        // places rect transform to have the same dimensions as 'other', even if they don't have same parent.
        // Relatively non-expensive.
        // NOTICE - also modifies scale of your rectTransf to match the scale of other
        public static void MatchOther(this RectTransform rt, RectTransform other) {
            Vector2 myPrevPivot = rt.pivot;
            myPrevPivot = other.pivot;
            rt.position = other.position;

            rt.localScale = other.localScale;

            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, other.rect.width);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, other.rect.height);
            //rectTransf.ForceUpdateRectTransforms(); - needed before we adjust pivot a second time?
            rt.pivot = myPrevPivot;
        }

        /// <summary>
        /// Replaces the transform of a gameobject with a new one, and returns the new transform.
        /// </summary>
        /// <param name="t">the old transform</param>
        /// <param name="other">the new transform</param>
        public static void SetTransform(this Transform t, Transform other) {
            t.SetParent(other.parent);
            t.SetPositionAndRotation(other.position, other.rotation);
            t.SetSiblingIndex(other.GetSiblingIndex());
            t.localScale = other.localScale;
        }
        
        /// <summary>
        /// Gets all the game objects with the tag *tag* and gets the component *T* and adds them to the list *list*
        /// </summary>
        /// <param name="list">Output list</param>
        /// <param name="tag">Target Tag</param>
        /// <typeparam name="T">Target Component</typeparam>
        public static void GetAllTypesWithTag<T>(List<T> list, string tag) {
            var gameObjects = GameObject.FindGameObjectsWithTag(tag);
            list.AddRange(gameObjects.Select(aGameObject => aGameObject.GetComponent<T>()));
        }

    }
}