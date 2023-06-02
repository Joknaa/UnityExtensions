using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OknaaEXTENSIONS {
    public static class GeneralExtensions {
        
        public static void SetText(this TMP_Text text, object value) => text.text = value.ToString();
        
        public static void SetSprite(this Image image, Sprite sprite) => image.sprite = sprite;
        
        
        
        /// <summary>
        /// Sets the min, max, and value of a slider.
        /// </summary>
        /// <param name="slider"></param>
        /// <param name="value"></param>
        /// <param name="min">defaults to 0</param>
        /// <param name="max">defaults to 1</param>
        public static void SetSliderValue(this Slider slider, float value, float min = 0, float max = 1) {
            slider.minValue = min;
            slider.maxValue = max;
            slider.value = value;
        }
        
        /// <summary>
        /// Sets the value of a slider to its minimum value.
        /// </summary>
        /// <param name="slider"></param>
        public static void MaxOut(this Slider slider) => slider.value = slider.maxValue;
        
    }
}