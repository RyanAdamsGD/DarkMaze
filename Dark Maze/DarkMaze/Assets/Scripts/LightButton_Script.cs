using UnityEngine;
using System.Collections;

public class LightButton_Script : MonoBehaviour 
{
    private Color buttonColor;
    public GameObject LightBarContainer;
    public GameObject LightBarBackground;
    bool callThisOnlyOnce = true;
    float spacingOffset;

    public Color color
    {
        get { return buttonColor; }
        set 
        { 
            buttonColor = value;
            LightBarBackground.GetComponent<GUITexture>().color = value;
        }
    }

    public const float LIGHT_BUTTON_HEIGHT_OFFSET = 0.01f;
    public const float LIGHT_BUTTON_WIDTH_PADDING = 0.035f;
    public const float LIGHT_BUTTON_WIDTH_OFFSET_ADJUSTMENT = 0.35f;
	// Use this for initialization
	void Start () 
    {
        float xPosition = (Screen.width * LightBar_Script.SCREEN_WIDTH_MULTIPLYER)/Screen.width + LIGHT_BUTTON_WIDTH_PADDING;
        gameObject.transform.position = new Vector3(xPosition,transform.position.y, 0);
        //gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, 0);
        //transform.localPosition -= new Vector3((((Screen.width * LightBar_Script.SCREEN_WIDTH_MULTIPLYER * 0.25f) / Screen.width - (LIGHT_BUTTON_WIDTH_OFFSET_ADJUSTMENT * LightBar_Script.SCREEN_WIDTH_MULTIPLYER))*0.5f) * LIGHT_BUTTON_WIDTH_OFFSET_ADJUSTMENT,0,0);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (callThisOnlyOnce)
        {
            Rect resizingRect = new Rect(LightBarContainer.GetComponent<GUITexture>().pixelInset);
            resizingRect.width *= LIGHT_BUTTON_WIDTH_OFFSET_ADJUSTMENT * LightBar_Script.SCREEN_WIDTH_MULTIPLYER;
            LightBarContainer.GetComponent<GUITexture>().pixelInset = resizingRect;
            LightBarBackground.GetComponent<LightBarBackground_Script>().resizeToContainer(resizingRect);
            callThisOnlyOnce = false;            
        }
	}

    public void ButtonHit(GameObject hitObject)
    {
        highlightButton();
        GameObject.Find("Player").GetComponent<Player_Script>().ActivateColor(gameObject);
    }

    void highlightButton()
    {
        LightBarContainer.GetComponent<GUITexture>().color = new Color(1, 1, 0, 1);
    }

    public void unhighlightButton()
    {
        LightBarContainer.GetComponent<GUITexture>().color = new Color(1, 1, 1, 1);
    }
}
