using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ColorBar_Script : MonoBehaviour {

    public Colour colour;
	float originalWidth;

	// Use this for initialization
	void Start () 
    {
        Rect position = new Rect();
        position.width = Screen.width * (LightBar_Script.SCREEN_WIDTH_MULTIPLYER - 0.025f);
		originalWidth = position.width;
        position.height = Screen.height * LightBar_Script.SCREEN_HEIGHT_MULTIPLYER;
        position.x = (Screen.width / 2.0f) - (position.width / 2.0f);
        position.y = (Screen.height / LightBar_Script.OFFSET_FROM_BOTTOM_OF_SCREEN);

        guiTexture.pixelInset = position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (colour.amount == 0)
        {
            Destroy(this);
        }

        Rect position = guiTexture.pixelInset;
        //add 2 pixels for better quality
        position.width = colour.amount * originalWidth + 2;
		guiTexture.pixelInset = position;
	}
}
