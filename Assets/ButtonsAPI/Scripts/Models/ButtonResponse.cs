using System;
using System.Collections.Generic;
using ButtonsAPI.Interfaces;
using ButtonsAPI.Utils;
using UnityEngine;

namespace ButtonsAPI.Models
{
    [Serializable]
    public class ButtonResponse : IButton
    {
        public int id;
        public List<float> color;
        public bool animationType;
        public string text;


        public int buttonId => id;
        public Color? buttonColor => color is { Count: 3 } ? ColorHelper.HSLToRGB(color[0], color[1], color[2]) : null;
        public bool isAnimation => animationType;
        public string buttonText => text;
    }
}