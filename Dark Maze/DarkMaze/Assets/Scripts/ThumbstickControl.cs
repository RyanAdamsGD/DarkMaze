using UnityEngine;
using System.Collections;

public class ThumbstickControl : MonoBehaviour 
{
    public Rect Bounds;
    public Texture OuterRing;
    public Texture InnerRing;

    const float SCREEN_PADDING = 0.02f;

    public float MaxReturnedValue;
    public float MaxOffset;

    private Vector2 innerOffset;
    private Rect offsetBounds
    {
        get
        {
            return new Rect(Bounds.xMin + innerOffset.x, Bounds.yMin + innerOffset.y, Bounds.width, Bounds.height);
        }
    }

    private bool trackingInput;
    private int touchTrackingID;

    public Vector2 Displacement
    {
        get
        {
            return new Vector2(Mathf.Clamp(innerOffset.x, -MaxReturnedValue, MaxReturnedValue), Mathf.Clamp(innerOffset.y, -MaxReturnedValue, MaxReturnedValue));
        }
    }

	void Start () 
    {
        float screenArea = Screen.width * ((1 - LightBar_Script.SCREEN_WIDTH_MULTIPLYER) * 0.4f);

        Bounds = new Rect(Screen.width * SCREEN_PADDING, Screen.height -  (Screen.height * SCREEN_PADDING)- screenArea, screenArea, screenArea);
        trackingInput = false;
	}
	
	void Update () 
    {
        // is there a touch?
        //  y- am I following it?
        //          y- follow it.
        //          n- is it within my range?
        //              y- follow it.
        //              n- ignore it.
        //  n- reset tracking info.

        Vector2? inputPos = null;

        // mouse tracking
        if (Input.GetMouseButton(0))
        {
            inputPos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

            if (!trackingInput)
            {
                trackingInput = Bounds.Contains((Vector2)inputPos);  // if wasn't tracking input, see if it needs to
            }

            if (trackingInput)
            {
                innerOffset = (Vector2)inputPos - Bounds.center; // update the joystick's position

                if (innerOffset.magnitude > MaxOffset)
                    innerOffset = innerOffset.normalized * MaxOffset;
            }
        }

        if (inputPos != null) return;   // return if there was mouse input.

        foreach (Touch t in Input.touches)
        {
            // TODO: CHECK TO MAKE SURE THE POSITION of TOUCH DOESN'T HAVE THE Y-VALUE INVERTED.
            // IT WAS INVERTED FOR THE MOUSE, MIGHT BE FOR THIS.
            inputPos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

            if (!trackingInput)
            {
                if (Bounds.Contains((Vector2)inputPos))  // if wasn't tracking input, see if it needs to
                {
                    trackingInput = true;
                    touchTrackingID = t.fingerId;
                }
            }

            if (trackingInput && t.fingerId == touchTrackingID)
            {
                innerOffset = (Vector2)inputPos - Bounds.center; // update the joystick's position

                if (innerOffset.magnitude > MaxOffset)
                    innerOffset = innerOffset.normalized * MaxOffset;
            }
        }

        if (inputPos == null)    // no input == no touch. reset.
        {
            trackingInput = false;
            touchTrackingID = -1;
            innerOffset = Vector2.zero;
        }
	}

    void OnGUI()
    {
        GUI.DrawTexture(Bounds, OuterRing);
        GUI.DrawTexture(offsetBounds, InnerRing);
    }
}
