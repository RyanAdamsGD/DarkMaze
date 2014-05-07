using UnityEngine;
using System.Collections;

public class LightBarBackground_Script : MonoBehaviour 
{
    //cut off 12 pixels on each side of the container
    const float INSET_LEFT_RIGHT = 12;
    const float INSET_TOP_BOTTOM = 16;
    public GameObject LightButton;

    // Use this for initialization
	void Start () 
    {
        Rect position = new Rect();
        float containerWidth = Screen.width * LightBar_Script.SCREEN_WIDTH_MULTIPLYER;
        float containerHeight = Screen.height * LightBar_Script.SCREEN_HEIGHT_MULTIPLYER;
		float xOffset = INSET_LEFT_RIGHT * (containerWidth/256);
		float yOffset = INSET_TOP_BOTTOM * (containerHeight/256);
        position.width = containerWidth - (2.0f * xOffset);
        position.height = containerHeight - (2.0f * yOffset);
        position.x = (Screen.width / 2.0f) - (containerWidth / 2.0f) + (xOffset/2.0f);
        position.y = (Screen.height / LightBar_Script.OFFSET_FROM_BOTTOM_OF_SCREEN) + yOffset;

        guiTexture.pixelInset = position;	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (LightButton != null && Input.GetMouseButtonDown(0) && guiTexture.HitTest(Input.mousePosition))
        {
            LightButton.GetComponent<LightButton_Script>().ButtonHit(gameObject);
        }
	}

    public void resizeToContainer(Rect container)
    {
        Rect position = new Rect();
		float xOffset = INSET_LEFT_RIGHT * (container.width/256.0f);
		float yOffset = INSET_TOP_BOTTOM * (container.height/256.0f);
        position.width = container.width - (2.0f * xOffset);
        position.height = container.height - (2.0f * yOffset);
        position.x = container.x + xOffset;
        position.y = container.y + yOffset;

        guiTexture.pixelInset = position;
    }
}
