using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace OknaaEXTENSIONS {
    public static class CoordinatesExtensions {
        /// <summary>
        /// Changes the X value of a Vector3, while keeping the other values the same.
        /// </summary>
        /// <param name="v">the vector3</param>
        /// <param name="x">the New value of X</param>
        /// <returns></returns>
        public static Vector3 SetX(this ref Vector3 v, float x) {
            return new Vector3(x, v.y, v.z);
        }

        public static Vector3 SetXY(this ref Vector3 v, float x, float y) {
            v.x = x;
            v.y = y;
            return v;
        }

        public static Vector3 SetXZ(this ref Vector3 v, float x, float z) {
            v.x = x;
            v.z = z;
            return v;
        }

        /// <summary>
        /// Changes the Y value of a Vector3, while keeping the other values the same.
        /// </summary>
        /// <param name="v">the vector3</param>
        /// <param name="y">the New value of Y</param>
        /// <returns></returns>
        public static Vector3 SetY(this Vector3 v, float y) {
            return new Vector3(v.x, y, v.z);
        }

        /// <summary>
        /// Changes the Z value of a Vector3, while keeping the other values the same.
        /// </summary>
        /// <param name="v">the vector3</param>
        /// <param name="z">the New value of Z</param>
        /// <returns></returns>
        public static Vector3 SetZ(this Vector3 v, float z) {
            return new Vector3(v.x, v.y, z);
        }

        public static Vector3 LerpX(this Vector3 initial, Vector3 final, float t) {
            initial.x = Mathf.Lerp(initial.x, final.x, t);
            return initial;
        }

        public static Vector3 LerpY(this Vector3 initial, Vector3 final, float t) {
            initial.y = Mathf.Lerp(initial.y, final.y, t);
            return initial;
        }

        public static Vector3 LerpZ(this Vector3 initial, Vector3 final, float t) {
            initial.z = Mathf.Lerp(initial.z, final.z, t);
            return initial;
        }

        public static Vector3 LerpXY(this Vector3 initial, Vector3 final, float t) {
            initial.x = Mathf.Lerp(initial.x, final.x, t);
            initial.y = Mathf.Lerp(initial.y, final.y, t);
            return initial;
        }

        public static Vector3 LerpXZ(this Vector3 initial, Vector3 final, float t) {
            initial.x = Mathf.Lerp(initial.x, final.x, t);
            initial.z = Mathf.Lerp(initial.z, final.z, t);
            return initial;
        }

        public static Vector3 LerpYZ(this Vector3 initial, Vector3 final, float t) {
            initial.y = Mathf.Lerp(initial.y, final.y, t);
            initial.z = Mathf.Lerp(initial.z, final.z, t);
            return initial;
        }

        public static float RandomCoord(this Vector2 v) => UnityEngine.Random.value < 0.5f ? v.x : v.y;
        public static int RandomCoord(this Vector2Int v) => UnityEngine.Random.value < 0.5f ? v.x : v.y;
        
        
        public static Vector3 GetVector(this float angle) {
            float angleRad = angle * Mathf.PI / 180;
            return new Vector3(Mathf.Cos(angleRad), 0,Mathf.Sin(angleRad));
        }
    }
}