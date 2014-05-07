using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class Player_Script : MonoBehaviour
{
    public GameObject trailPrefab;
    public GameObject lightBar;
    public GameObject lightButton;
    public GameObject SpotLight;

    public List<Object> buttons;
    List<Colour> colors = new List<Colour>();
    const float AMOUNT_TO_SUBTRACT = 0.045f;
    float BUTTON_Y_OFFSET;
    Vector3 lastNodePosition;
    Color averagedColor;
    GameObject currentActiveLightButton;
    public Colour currentPowerUp;
    bool useIncreasedDrainRate = false;
    //float INCREASED_DRAINRATE = 2.5f;

    // Use this for initialization
    void Start()
    {
        BUTTON_Y_OFFSET = (Screen.height * LightBar_Script.SCREEN_HEIGHT_MULTIPLYER * 1.25f) / Screen.height;
        ResetPlayerPowerUpChanges();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        lowerColor();
        calculateAveragedLightColor();
        trailPlacer();
        adjustLightBarPosition();
        adjustLightColors();
		fixDrag();
    }
	
	void fixDrag()
	{
		if (detectGround())
		{
			rigidbody.drag = 1;
		}else{
			rigidbody.drag = 0;	
		}
	}
	private bool detectGround(){
		Ray ray = new Ray(rigidbody.position, new Vector3(0,-1,0));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)){
			return hit.distance<=0.7;
		}
        return false;
	}
    void adjustLightColors()
    {
        GetComponent<Light>().color = averagedColor;
        Color color;
        if (averagedColor.r > 0 || averagedColor.g > 0 || averagedColor.b > 0)
        {
            color = new Color(averagedColor.r * 0.75f, averagedColor.g * 0.75f, averagedColor.b * 0.75f, 1);
        }
        else
        {
            color = new Color(0.05f, 0.05f, 0.05f, 1);
        }

        //SpotLight.GetComponent<Light>().color = color;
        adjustMaterialColor(color);
    }

    void adjustMaterialColor(Color color)
    {
        GetComponent<MeshRenderer>().material.SetColor("_TintColor", color);
    }

    private void trailPlacer()
    {
        if ((lastNodePosition - gameObject.transform.position).magnitude >= 4f)
        {
            Vector3 trailNodePosition = gameObject.transform.position;
            Object node = Instantiate(trailPrefab, trailNodePosition, Quaternion.AngleAxis(90, new Vector3(1, 0, 0)));
            lastNodePosition = ((GameObject)node).transform.position;
            ((GameObject)node).GetComponent<TrailNode>().setColor(averagedColor);
        }
    }

    public void AddColor(Colour ColorToAdd)
    {
        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].color == ColorToAdd.color)
            {
                calculateAddedColor(i, ColorToAdd.amount);
                return;
            }
        }
        createNewLightColor(ColorToAdd);
    }

    void createNewLightColor(Colour ColorToAdd)
    {
        colors.Add(ColorToAdd);
        lightBar.GetComponent<LightBar_Script>().AddColorBar(colors[colors.Count - 1]);
        buttons.Add(Instantiate(lightButton, new Vector3(0,(buttons.Count * BUTTON_Y_OFFSET), 0), new Quaternion()));
        ((GameObject)buttons[buttons.Count - 1]).GetComponent<LightButton_Script>().color = ColorToAdd.color;
    }

    void calculateAveragedLightColor()
    {
        averagedColor = new Color(0, 0, 0, 1);

        foreach (Colour colour in colors)
        {
            averagedColor.r += colour.color.r * colour.amount;
            averagedColor.g += colour.color.g * colour.amount;
            averagedColor.b += colour.color.b * colour.amount;
        }
    }

    void adjustLightBarPosition()
    {
        lightBar.gameObject.transform.position = new Vector3();
    }

    public void lowerColor()
    {
        for (int i = 0; i < colors.Count; i++)
        {
            colors[i].amount = Mathf.Clamp01(colors[i].amount - (AMOUNT_TO_SUBTRACT * Time.deltaTime));           

            if (colors[i].amount == 0)
            {
                removeButton(colors[i].color);
                colors.RemoveAt(i--);
                useIncreasedDrainRate = false;
            }
        }
        //turn this back on to drain faster when using an ability
        //if (useIncreasedDrainRate)
        //{
        //    currentPowerUp.amount = Mathf.Clamp01(currentPowerUp.amount - (AMOUNT_TO_SUBTRACT * INCREASED_DRAINRATE * Time.deltaTime));
        //}
    }

    void removeButton(Color color)
    {
        bool shiftRemaining = false;
        for (int j = 0; j < buttons.Count; j++)
        {
            if (shiftRemaining)
            {
                ((GameObject)buttons[j]).gameObject.transform.position = new Vector3(((GameObject)buttons[j]).gameObject.transform.position.x, ((GameObject)buttons[j]).gameObject.transform.position.y - BUTTON_Y_OFFSET, 0);
            }
            if (((GameObject)buttons[j]).GetComponent<LightButton_Script>().color == color)
            {
                if (currentActiveLightButton == (GameObject)buttons[j])
                {
                    currentActiveLightButton = null;
                    currentPowerUp = null;
                    ResetPlayerPowerUpChanges();
                }
                DestroyObject(buttons[j]);
                buttons.RemoveAt(j--);
                shiftRemaining = true;
            }

        }
    }

    void calculateAddedColor(int index, float amountToAdd)
    {
        if (colors.Count > 1)
        {
            float totalAmount = 0;
            foreach (Colour colour in colors)
            {
                totalAmount += colour.amount;
            }
            //check if our new total will be over 1
            //if so only add enough to take us to 1
            if (totalAmount + amountToAdd > 1)
            {
                colors[index].amount += 1 - totalAmount;
            }
            else
            {
                colors[index].amount += amountToAdd;
            }
        }
        else
        {
            colors[0].amount = Mathf.Clamp01(colors[0].amount + amountToAdd);
        }
    }

    public void UseCurrentPowerUp()
    {
        if (currentPowerUp != null)
        {
            currentPowerUp.UsePowerUp(this);
            useIncreasedDrainRate = true;
        }
    }

    public void ResetPlayerPowerUpChanges()
    {
        useIncreasedDrainRate = false;

        //white
        SpotLight.transform.localPosition = new Vector3(0, 1.2f, 0);
        SpotLight.GetComponent<Light>().spotAngle = 60;
        //blue
        GetComponent<Controls>().speed = 8.0f;
        //red
        GameObject[] redBlocks = GameObject.FindGameObjectsWithTag("RedBlock");
        for (int i = 0; i < redBlocks.Length; i++)
        {
            redBlocks[i].GetComponent<Collider>().enabled = true;
            redBlocks[i].GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 0, 0, 1));
        }
        //yellow
        GameObject[] yellowBlocks = GameObject.FindGameObjectsWithTag("YellowBlock");
        for (int i = 0; i < yellowBlocks.Length; i++)
        {
            Rigidbody blockRigidBody = yellowBlocks[i].GetComponent<Rigidbody>();
            if (rigidbody.velocity.sqrMagnitude == 0)
            {
                blockRigidBody.isKinematic = true;
            }
        }
    }

    public void ActivateColor(GameObject lightButtonHit)
    {
        if (lightButtonHit != currentActiveLightButton)
        {
            if (currentActiveLightButton != null)
            {
                currentActiveLightButton.GetComponent<LightButton_Script>().unhighlightButton();
            }

            currentActiveLightButton = lightButtonHit;

            for (int i = 0; i < colors.Count; i++)
            {
                if (colors[i].color == currentActiveLightButton.GetComponent<LightButton_Script>().color)
                {
                    currentPowerUp = colors[i];
                }
            }
        }
        ResetPlayerPowerUpChanges();
        UseCurrentPowerUp();
    }

    public float AbsorbColor(PowerUpType type)
    {
        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].Type == type)
            {
                float amountToReturn = colors[i].amount;
                colors[i].amount = 0;
                return amountToReturn;
            }
        }
        return 0;
    }
}