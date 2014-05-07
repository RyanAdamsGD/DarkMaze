using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class LightBar_Script : MonoBehaviour
{

    public const float OFFSET_FROM_BOTTOM_OF_SCREEN = 75;
    public const float SCREEN_HEIGHT_MULTIPLYER = 0.07f;
    public const float SCREEN_WIDTH_MULTIPLYER = 0.6f;

    public GameObject colorBar;
    public GameObject colorBarBackground;
    List<Object> colorBarList = new List<Object>();

    // Use this for initialization
    void Start()
    {
        
    }

    public void AddColorBar(Colour color)
    {
        colorBarList.Add(Instantiate(colorBar));
        ((GameObject)colorBarList[colorBarList.Count - 1]).GetComponent<ColorBar_Script>().colour = color;
        ((GameObject)colorBarList[colorBarList.Count - 1]).guiTexture.color = color.color;
    }

    // Update is called once per frame
    void Update()
    {
        Rect movementHolder = new Rect(colorBarBackground.guiTexture.pixelInset);
        movementHolder.width = 0;
        for (int i = 0; i < colorBarList.Count; i++)
        {
            if (((GameObject)colorBarList[i]).GetComponent<ColorBar_Script>() == null)
            {
                DestroyObject(colorBarList[i]);
                colorBarList.RemoveAt(i--);
            }
            else
            {
                //subtract 3 pixels for better quality
                float newXMin = movementHolder.xMin + movementHolder.width -3;

                movementHolder = ((GameObject)colorBarList[i]).guiTexture.pixelInset;
                movementHolder.xMin = newXMin;

                ((GameObject)colorBarList[i]).guiTexture.pixelInset = movementHolder;
            }

        }
    }
}
