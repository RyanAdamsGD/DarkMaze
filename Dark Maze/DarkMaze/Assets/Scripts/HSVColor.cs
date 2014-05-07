using UnityEngine;
using System.Collections;

public class HSVColor
{
    public float hue { get; set; }
    public float saturation { get; set; }
    public float value { get; set; }

    public Color RGBValue
    {
        get
        {
            if (saturation == 0) { return new Color(0, 0, 0); }     // gray

            int i;
            float f, p, q, t;

            float h = hue;
            float s = saturation;
            float v = value;

            h /= 60.0f;
            i = (int)Mathf.Floor(h);
            f = h - i;
            p = v * (1 - s);
            q = v * (1 - s * f);
            t = v * (1 - s * (1 - f));

            switch (i)
            {
                case 0:
                    return new Color(v, t, p);
                case 1:
                    return new Color(q, v, p);
                case 2:
                    return new Color(p, v, t);
                case 3:
                    return new Color(p, q, v);
                case 4:
                    return new Color(t, p, v);
                default:
                    return new Color(v, p, q);
            }
        }
    }
}
