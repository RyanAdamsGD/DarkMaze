using UnityEngine;
using System.Collections;

public class PowerupButtonControl : MonoBehaviour 
{
    private Player_Script player;

    public Rect ButtonRegion;
    public Texture ButtonTexture;
    int fingerId = -1;
    const float SCREEN_PADDING = 0.02f;

    bool displayButton
    {
        get
        {
            return player.currentPowerUp != null;
        }
    }

	// Use this for initialization
	void Start () 
    {
        player = gameObject.GetComponent<Player_Script>();

        float screenArea = Screen.width * ((1 - LightBar_Script.SCREEN_WIDTH_MULTIPLYER) * 0.3f);

        ButtonRegion = new Rect(Screen.width - (Screen.width * SCREEN_PADDING) - screenArea, Screen.height - (Screen.height * SCREEN_PADDING) - screenArea, screenArea, screenArea);
        //comment this line out to bring back the button
        ButtonRegion = new Rect(0,0,0,0);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ButtonRegion.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
                player.UseCurrentPowerUp();
            }
        }
		else if (Input.GetMouseButtonUp(0))
        {
            if (ButtonRegion.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
                player.ResetPlayerPowerUpChanges();
            }
        }
        else
        {
            foreach (Touch t in Input.touches)
            {
                if (ButtonRegion.Contains(t.position) && t.phase == TouchPhase.Began)
                {
                    player.UseCurrentPowerUp();
                    fingerId = t.fingerId;
                    return;
                }
                if (t.phase == TouchPhase.Ended )
                {
                    player.ResetPlayerPowerUpChanges();
                    fingerId = -1;
                    return;
                }
            }
        }
	}

    void OnGUI()
    {
        if (!displayButton) return;

        Color c = GUI.color;
        if(player.currentPowerUp != null)
            GUI.color = player.currentPowerUp.color;
        GUI.DrawTexture(ButtonRegion, ButtonTexture);
        GUI.color = c;
    }
}
