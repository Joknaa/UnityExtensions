using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace OknaaEXTENSIONS {
    public static class MaterialsExtensions {
        public static Color SetColor(this Color originalColor, float r = -1, float g = -1, float b = -1, float a = -1) {
            // if the r,g,b,a values are not set, use the original color values
            r = Math.Abs(r - (-1)) < 0.01f ? originalColor.r : r;
            g = Math.Abs(g - (-1)) < 0.01f ? originalColor.g : g;
            b = Math.Abs(b - (-1)) < 0.01f ? originalColor.b : b;
            a = Math.Abs(a - (-1)) < 0.01f ? originalColor.a : a;

            originalColor = new Color(r, g, b, a);
            return originalColor;
        }

        /// <summary>
        ///  Turns the material's rendering mode into Opaque
        /// </summary>
        public static void ToOpaqueMode(this Material material) {
            material.SetOverrideTag("RenderType", "Opaque");
            material.SetInt("_SrcBlend", (int)BlendMode.One);
            material.SetInt("_DstBlend", (int)BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }

        /// <summary>
        ///  Turns the material's rendering mode into Fade
        /// </summary>
        public static void ToFadeMode(this Material material) {
            material.SetOverrideTag("RenderType", "Fade");
            material.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)RenderQueue.Transparent;
        }

        /// <summary>
        ///  Sets the Alpha (transparency) of a material, 0 means fully transparent, 1 means fully visible.
        /// </summary>
        public static void SetAlpha(this Material material, bool enable, float alpha = 1f) {
            if (enable) material.ToFadeMode();

            var newColor = material.color;
            newColor.a = alpha;
            material.color = newColor;
        }

        /// <summary>
        ///  Makes the material transparent, then plays a Fade Out animation.
        /// </summary>
        public static IEnumerator FadeOut(this Material material, bool enable, float duration = 1f, float initialValue = 0f, float endValue = 0f, bool instantly = false) {
            if (enable) material.ToFadeMode();

            var newColor = material.color;
            if (instantly) {
                newColor.a = endValue;
                material.color = newColor;
                yield break;
            }

            var timeElapsed = 0f;

            while (newColor.a > endValue) {
                newColor.a = Mathf.Lerp(newColor.a, endValue, (timeElapsed / duration));
                timeElapsed += Time.deltaTime;
                material.color = newColor;
                yield return new WaitForEndOfFrame();
            }
        }


        /// <summary>
        /// Plays a Fade In animation, then makes the Material opaque
        /// </summary>
        public static IEnumerator FadeIn(this Material material, float duration = 1f, float initialValue = 0f, float endValue = 1f, bool instantly = false) {
            var newColor = material.color;
            newColor.a = initialValue;

            if (instantly) {
                newColor.a = endValue;
                material.color = newColor;
                yield break;
            }

            var timeElapsed = 0f;
            while (newColor.a < endValue) {
                newColor.a = Mathf.Lerp(newColor.a, endValue, (timeElapsed / duration));
                timeElapsed += Time.deltaTime;
                material.color = newColor;
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// Plays a Fade animation, depending on the "endValue" parameter, the material will either fade in or fade out.
        /// It compares the current alpha value of the material with the "endValue" parameter, if the current alpha is lower than the "endValue" parameter, it will fade in, otherwise it will fade out.
        /// </summary>
        public static IEnumerator FadeTowardsAlpha(this Material material, float endValue, float duration = 1f) {
            material.ToFadeMode();

            var isFadeIn = material.color.a < endValue;
            var newColor = material.color;
            var timeElapsed = 0f;

            while (Mathf.Abs(newColor.a - endValue) > 0.01f) {
                newColor.a = Mathf.Lerp(newColor.a, endValue, (timeElapsed / duration));
                timeElapsed += Time.deltaTime;
                material.color = newColor;
                yield return new WaitForEndOfFrame();
            }


            if (isFadeIn && material.color.a >= 1f)
                material.ToFadeMode();
        }
    }
}