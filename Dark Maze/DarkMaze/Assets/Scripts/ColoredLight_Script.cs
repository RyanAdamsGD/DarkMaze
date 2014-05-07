using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ColoredLight_Script : MonoBehaviour {

    Colour colour;
    const float AMOUNT_OF_COLOR_ADDED = 0.009f;
    public PowerUpType type;


	// Use this for initialization
	void Start () 
    {
        colour = new Colour(type, AMOUNT_OF_COLOR_ADDED);
        gameObject.GetComponentInChildren<Light>().light.color = colour.color;
		gameObject.GetComponent<ParticleSystem>().startColor = colour.color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        Player_Script script = other.GetComponent<Player_Script>();
        if (script != null)
        {
            script.AddColor(new Colour(colour));            
        }
    }
}
