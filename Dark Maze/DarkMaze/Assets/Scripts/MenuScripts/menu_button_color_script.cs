using UnityEngine;
using System.Collections;

public class menu_button_color_script : MonoBehaviour 
{
    private static HSVColor currentColor = new HSVColor();
	// Use this for initialization
	void Start () 
    {
        currentColor.saturation = 1;
        currentColor.value = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        renderer.material.color = currentColor.RGBValue;

        currentColor.hue += Time.deltaTime * 5;

        if (currentColor.hue > 360)
            currentColor.hue = 0;
	}
}
