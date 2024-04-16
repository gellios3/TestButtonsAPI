using System.Collections.Generic;
using UnityEngine;

namespace ButtonsAPI.Utils
{
    public class ColorHelper
    {
        public static Color HSLToRGB(float hue, float saturation, float lightness)
        {
            hue = Mathf.Clamp01(hue);
            saturation = Mathf.Clamp01(saturation);
            lightness = Mathf.Clamp01(lightness);

            if (saturation == 0)
            {
                return new Color(lightness, lightness, lightness);
            }

            float q = lightness < 0.5f ? lightness * (1 + saturation) : lightness + saturation - lightness * saturation;
            float p = 2 * lightness - q;

            float h = hue * 360f;
            float r = HueToRgb(p, q, h / 360f + 1 / 3f);
            float g = HueToRgb(p, q, h / 360f);
            float b = HueToRgb(p, q, h / 360f - 1 / 3f);

            return new Color(r, g, b);

            float HueToRgb(float p, float q, float t)
            {
                if (t < 0)
                {
                    t += 1;
                }

                if (t > 1)
                {
                    t -= 1;
                }

                return t switch
                {
                    < 1 / 6f => p + (q - p) * 6 * t,
                    < 1 / 2f => q,
                    < 2 / 3f => p + (q - p) * (2 / 3f - t) * 6,
                    _ => p
                };
            }
        }

        public static List<float> RGBToHSL(Color rgbColor)
        {
            float r = rgbColor.r;
            float g = rgbColor.g;
            float b = rgbColor.b;

            float max = Mathf.Max(r, Mathf.Max(g, b));
            float min = Mathf.Min(r, Mathf.Min(g, b));
            float s, l;

            float h = s = l = (max + min) / 2f;

            if (Mathf.Approximately(max, min))
            {
                h = s = 0; 
            }
            else
            {
                float d = max - min;
                s = l > 0.5f ? d / (2f - max - min) : d / (max + min);
                if (Mathf.Approximately(max, r))
                {
                    h = (g - b) / d + (g < b ? 6f : 0f);
                }
                else if (Mathf.Approximately(max, g))
                {
                    h = (b - r) / d + 2f;
                }
                else if (Mathf.Approximately(max, b))
                {
                    h = (r - g) / d + 4f;
                }

                h /= 6f;
            }

            return new List<float>{h, s, l};
        }
    }
}