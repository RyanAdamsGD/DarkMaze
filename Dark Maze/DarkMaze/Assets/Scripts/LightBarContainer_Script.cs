using UnityEngine;
using System.Collections;

public class LightBarContainer_Script : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Rect position = new Rect();
        position.width = Screen.width * LightBar_Script.SCREEN_WIDTH_MULTIPLYER;
        position.height = Screen.height * LightBar_Script.SCREEN_HEIGHT_MULTIPLYER;
        position.x = (Screen.width / 2.0f) - (position.width / 2.0f);
        position.y = (Screen.height / LightBar_Script.OFFSET_FROM_BOTTOM_OF_SCREEN);

        guiTexture.pixelInset = position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
