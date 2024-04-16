using System;
using System.Collections.Generic;
using ButtonsAPI.Utils;
using UnityEngine;

namespace ButtonsAPI.Models
{
    [Serializable]
    public class ButtonRequest
    {
        public List<float> color;
        public bool animationType;
        public string text;

        public ButtonRequest()
        {
        }

        public ButtonRequest(Color? buttonColor, bool isAnimation, string buttonText)
        {
            if (buttonColor != null)
            {
                color = ColorHelper.RGBToHSL((Color)buttonColor);
            }

            animationType = isAnimation;
            text = buttonText;
        }
    }
}